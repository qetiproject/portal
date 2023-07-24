using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.EmployeeServiceModule;
using Portal.Portal.Application.EmployeeServiceModule.ImportEmployeesFeature;
using Portal.Portal.Application.RolesModule.AddEmployeeFeature;
using Portal.Portal.Common;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Configurations;
using Portal.Portal.Web.Api.Pages.Employee.Models;
using System;
using System.Net.Mail;
using System.Text;

namespace Portal.Portal.Web.Api.Pages.Employee
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly PortalDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly EmailConfiguration _emailConfiguration;

        public EmployeeController(
            PortalDbContext dbContext,
            UserManager<User> userManager,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IOptions<EmailConfiguration> emailConfiguration)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _configuration = configuration;
            _emailConfiguration = emailConfiguration.Value;
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            var action = new CreateEmployeeBusinessProcessAction(
                request.PersonalInformation.FullName,
                request.PersonalInformation.Position,
                request.PersonalInformation.PhoneNumber,
                request.PersonalInformation.Email,
                request.PersonalInformation.DateOfBirth,
                request.PersonalInformation.PersonalId,
                request.PersonalInformation.JobType,
                request.EmployeeRole.RoleId
            );

            var entityStore = new EntityStore(_dbContext);

            var result = new ActionHandler(
                                new CreateEmployeeBusinessProcess(_userManager)
                            ).Save(entityStore, action);

            if (result.Ok)
            {
                var employee = _dbContext.Employees.Include(e => e.PersonalInformation).FirstOrDefault(e => e.PersonalInformation.Email == request.PersonalInformation.Email);

                var addEmployeeToRoleAction = new AddEmployeeAction(
                    request.EmployeeRole.RoleId,
                    employee.Id,
                    employee.PersonalInformation.FullName,
                    employee.PersonalInformation.Position
                );

                result = new ActionHandler(
                                    new AddEmployeeBehavior()
                                ).Save(entityStore, addEmployeeToRoleAction);
            }

            if (result.Ok)
            {
                var user = await _userManager.FindByEmailAsync(request.PersonalInformation.Email);

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var appUrl = _configuration.GetSection("AppUrl");
                var callback = Url.Action("ResetPassword", "LogIn", new { token, email = user.Email }, Request.Scheme, appUrl.Value);

                callback = callback.Replace("api/login/", "");
                callback = callback.Replace("?token=", "/");
                callback = callback.Replace("&email=", "/");

                using (var smtpClient = new SmtpClient(_emailConfiguration.SmtpServer, _emailConfiguration.Port))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential
                    {
                        UserName = _emailConfiguration.Email,
                        Password = _emailConfiguration.Password
                    };
                    smtpClient.EnableSsl = true;

                    var mailMessage = new MailMessage(_emailConfiguration.Email, request.PersonalInformation.Email.Trim(), "Password Reset", callback)
                    {
                        IsBodyHtml = true
                    };

                    mailMessage.BodyEncoding = Encoding.UTF8;
                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        return new Result<IEntity[]>(ex.Message);
                    }
                }
            }

            return result;
        }

        [HttpGet]
        public Result<EmployeeRolesListResponse> EmployeeRolesList([FromQuery] EmployeeRolesListRequest request)
        {
            var roles = _dbContext.Roles
                .OrderByDescending(r => r.Id)
                .Select(r => new EmployeeRolesListModel(
                    r.Id,
                    r.Name))
                .ToList();

            var defaultRole = roles.FirstOrDefault(r => r.Name == "Employee");

            roles.Remove(defaultRole);

            if (!string.IsNullOrEmpty(request.Search))
                roles = roles.Where(r => r.Name.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<EmployeeRolesListResponse>(new EmployeeRolesListResponse(roles, defaultRole));
        }

        [HttpGet]
        public Result<EmployeeListResponse> EmployeeList([FromQuery] EmployeeListRequest request)
        {
            var employees = _dbContext.Employees.Include(e => e.PersonalInformation)
                .OrderByDescending(emp => emp.Id)
                .Select(emp => new EmployeeListModel(
                    emp.Id,
                    emp.PersonalInformation.FullName,
                    emp.Photo == null || emp.Photo.Hidden ? _configuration["NoPhotoPath"] : emp.Photo.Path,
                    emp.PersonalInformation.Position,
                    emp.PersonalInformation.PhoneNumber,
                    emp.PersonalInformation.Email,
                    emp.JobInfo.Department,
                    emp.EmploymentHistory.ContractType,
                    emp.PersonalInformation.Status
                )).ToList();

            if (!string.IsNullOrEmpty(request.Search))
            {
                employees = employees
                    .Where(e => e.FullName.ToUpper().Contains(request.Search.ToUpper())
                                || e.Position.ToUpper().Contains(request.Search.ToUpper())
                                || e.ContractType.ToUpper().Contains(request.Search.ToUpper())
                                || e.PhoneNumber.ToUpper().Contains(request.Search.ToUpper())
                                || e.Email.ToUpper().Contains(request.Search.ToUpper())).ToList();
            }

            return new Result<EmployeeListResponse>(new EmployeeListResponse(employees.Count(), employees));
        }

        [HttpPost]
        public Result<IEntity[]> ImportEmployees([FromForm] ImportEmployeesRequest request)
        {
            var fileParser = new ImportEmployeesFileParser();
            var businessProcesses = fileParser.Parse(request.File);

            var errorMessages = new List<string>();

            var entityStore = new EntityStore(_dbContext);
            businessProcesses.ForEach(action =>
            {
                var result = new ActionHandler(
                                       new ImportEmployeeBusinessProcess(_userManager)
                                   ).Save(entityStore, action);

                if (!result.Ok)
                    errorMessages.AddRange(result.Errors.Messages);
            });

            return errorMessages.Any() ? new Result<IEntity[]>(errorMessages.ToArray()) : new Result<IEntity[]>();
        }
    }
}
