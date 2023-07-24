using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Pages.Login.Models;
using System.Net.Mail;
using System.Reflection.Emit;
using Microsoft.Extensions.Options;
using Portal.Portal.Web.Api.Configurations;
using Microsoft.AspNetCore.Http.HttpResults;
using Portal.Portal.Web.Api.Resources;

namespace Portal.Portal.Web.Api.Pages.Login
{
    [Route("api/[controller]/[action]")]
    public class LogInController : ControllerBase
    {
        protected PortalDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
        private EmailConfiguration emailConfiguration;

        public LogInController(
            PortalDbContext dbContext,
            UserManager<User> userManager,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IOptions<EmailConfiguration> emailConfiguration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.configuration = configuration;
            this.emailConfiguration = emailConfiguration.Value;
        }

        [HttpPost]
        public Result<LoginResponse> Login([FromBody] LoginRequest request)
        {
            var user = userManager.FindByEmailAsync(request.Email).Result;
            if (user is not null && userManager.CheckPasswordAsync(user, request.Password).Result)
            {
                var employee = dbContext.Employees
                    .Include(e => e.PersonalInformation)
                    .Include(e => e.Photo)
                    .FirstOrDefault(e => e.PersonalInformation.Email == user.Email);

                var permissions = dbContext.Roles
                    .Where(r => r.Users.Any(u => u.UserId == employee.Id))
                    .SelectMany(u => u.Permissions).ToList();

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = GetToken(authClaims);

                return new Result<LoginResponse>(new LoginResponse(
                    user.Id,
                    employee.PersonalInformation.FullName,
                    user.Email,
                    new JwtSecurityTokenHandler().WriteToken(token),
                    token.ValidTo,
                    employee.Photo == null ? configuration["NoPhotoPath"] : employee.Photo.Path,
                    employee.Id,
                    new ActionPermissions(
                        permissions.Any(p => p.PermissionKey == "AddEmployee"),
                        permissions.Any(p => p.PermissionKey == "ViewEmployeesList"),
                        permissions.Any(p => p.PermissionKey == "ViewEmployeeProfile"),
                        permissions.Any(p => p.PermissionKey == "EditPersonalInformation"),
                        permissions.Any(p => p.PermissionKey == "EditJobInfo"),
                        permissions.Any(p => p.PermissionKey == "EditEducation"),
                        permissions.Any(p => p.PermissionKey == "EditPayrollBasic"),
                        permissions.Any(p => p.PermissionKey == "EditEmploymentHistory"),
                        permissions.Any(p => p.PermissionKey == "EditSkillsAndLanguages"),
                        permissions.Any(p => p.PermissionKey == "EditOtherInfo"),
                        permissions.Any(p => p.PermissionKey == "AllTimeOffRequests"),
                        permissions.Any(p => p.PermissionKey == "AllNonComliances"),
                        permissions.Any(p => p.PermissionKey == "ViewAdminPanel"),
                        permissions.Any(p => p.PermissionKey == "ViewRoles"))));
            }

            return new Result<LoginResponse>("Email or Password is not valid");
        }

        [HttpGet]
        public Result<string> GetUserPhoto()
        {
            var user = userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var employee = dbContext.Employees
                .Include(e => e.PersonalInformation)
                .Include(e => e.Photo)
                .FirstOrDefault(e => e.PersonalInformation.Email == user.Email);

            return new Result<string>(employee.Photo == null ? configuration["NoPhotoPath"] : employee.Photo.Path);
        }

        [HttpGet]
        public bool IsAuthenticated()
        {
            return User.Identity.IsAuthenticated;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddHours(4).AddDays(7),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        public async Task<Result<IEntity>> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user == null)
                return new Result<IEntity>(AuthorizationModuleResources.UserDoesNotExist);

            var token = userManager.GeneratePasswordResetTokenAsync(user).Result;
            var appUrl = configuration.GetSection("AppUrl");
            var callback = Url.Action(nameof(ResetPassword), "LogIn", new { token, email = user.Email }, Request.Scheme, appUrl.Value);

            callback = callback.Replace("api/login/", "");
            callback = callback.Replace("?token=", "/");
            callback = callback.Replace("&email=", "/");

            using (var smtpClient = new SmtpClient(emailConfiguration.SmtpServer, emailConfiguration.Port))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential
                {
                    UserName = emailConfiguration.Email,
                    Password = emailConfiguration.Password
                };

                smtpClient.EnableSsl = true;

                var mailMessage = new MailMessage(emailConfiguration.Email, request.Email, "Password Reset", callback)
                {
                    IsBodyHtml = true
                };

                mailMessage.BodyEncoding = Encoding.UTF8;
                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    return new Result<IEntity>();

                }
                catch (Exception ex)
                {
                    return new Result<IEntity>(ex.Message);
                }
            }
        }

        [HttpPost]
        public Result<IEntity> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = userManager.FindByEmailAsync(request.Email).Result;

            if (user == null)
                return new Result<IEntity>(AuthorizationModuleResources.UserDoesNotExist);

            if (userManager.CheckPasswordAsync(user, request.Password).Result)
                return new Result<IEntity>(AuthorizationModuleResources.YourNewPasswordMustBeDifferent);

            var resetPassResult = userManager.ResetPasswordAsync(user, request.Token, request.Password).Result;
            if (!resetPassResult.Succeeded)
            {
                return new Result<IEntity>(resetPassResult.Errors.Select(e => e.Description).ToArray());
            }

            dbContext.SaveChanges();

            return new Result<IEntity>();
        }
    }
}
