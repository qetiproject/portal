using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.Portal.Application.ApplicationUsers.Models;
using Portal.Portal.Application.EmployeeServiceModule;
using Portal.Portal.Application.EmployeeServiceModule.AddComputerSkillFeature;
using Portal.Portal.Application.EmployeeServiceModule.AddEmployeeTrainingFeature;
using Portal.Portal.Application.EmployeeServiceModule.AddFormerPositionFeature;
using Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeProfilePhotoFeature;
using Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeTrainingFeature;
using Portal.Portal.Application.EmployeeServiceModule.DeleteComputerSkillFeature;
using Portal.Portal.Application.EmployeeServiceModule.DeleteFormerPositionFeature;
using Portal.Portal.Application.EmployeeServiceModule.DeleteEmployeeUniversityFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditEmployeeJobInfoFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditEmployeePayrollBasicFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditEmployeeTrainingFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditEmploymentHistoryFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditFormerPositionFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditOtherInformationFeature;
using Portal.Portal.Application.EmployeeServiceModule.Models;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Application.EmployeeServiceModule.UpdateEmployeeProfilePhotoFeature;
using Portal.Portal.Common;
using Portal.Portal.Persistence;
using Portal.Portal.Web.Api.Pages.Employee.Models;
using Portal.Portal.Web.Api.Pages.EmployeeProfile.Models;
using System.ComponentModel;
using System.Reflection;
using Portal.Portal.Application.EmployeeServiceModule.AddLanguageFeature;
using Portal.Portal.Application.EmployeeServiceModule.DeleteLanguageFeature;
using Portal.Portal.Application.EmployeeServiceModule.UploadBloodGroupAndRhesusFeature;
using Portal.Portal.Application.EmployeeServiceModule.UploadMedicalCertificateFeature;
using Portal.Portal.Application.EmployeeServiceModule.UploadAlergiesFeature;
using Portal.Portal.Application.EmployeeServiceModule.UploadDrivingLicenseFeature;
using Portal.Portal.Application.EmployeeServiceModule.AddEmployeeUniversityFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditEmployeePersonalInformationFeature;
using Portal.Portal.Application.EmployeeServiceModule.EditEmployeeUniversityFeature;
using Portal.Portal.Application.EmployeeServiceModule.UploadResumeFeature;
using Portal.Portal.Application.EmployeeServiceModule.UploadContractFeature;
using Newtonsoft.Json;
using Portal.Portal.Web.Api.Pages.Shared.Resource.Models;
using Portal.Portal.Web.Api.Resources;
using System.Collections;
using System.Globalization;
using System.Resources;
using Portal.Portal.Application.EmployeeServiceModule.UploadIdDocumentFeature;
using Portal.Portal.Web.Api.Pages.TimeOffRequests.Models;
using System.Security.Claims;
using Portal.Portal.Application.RolesModule.AddEmployeeFeature;
using Portal.Portal.Web.Api.Pages.Roles.Models;
using Portal.Portal.Application.EmployeeServiceModule.HideEmployeeProfilePhotoFeauter;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class EmployeeProfileController : ControllerBase
    {
        private readonly PortalDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IPhotoUploadService _photoUploadService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IConfiguration _configuration;

        public EmployeeProfileController(
            PortalDbContext dbContext,
            UserManager<User> userManager,
            IPhotoUploadService photoUploadService,
            IFileUploadService fileUploadService,
            IConfiguration configuration)
        {
            this._dbContext = dbContext;
            this._userManager = userManager;
            this._photoUploadService = photoUploadService;
            this._fileUploadService = fileUploadService;
            this._configuration = configuration;
        }

        [HttpGet]
        public Result<SupervisorEmployeesResponse> SupervisorEmployees([FromQuery] SupervisorEmployeesRequest request)
        {
            var employees = _dbContext.Employees
                .Where(e => e.Id != request.EmployeeId)
                .Select(e => new SupervisorEmployeesModel(e.Id, e.PersonalInformation.Email)).ToList();

            if (!string.IsNullOrEmpty(request.Search))
                employees = employees.Where(e => e.Email.ToLower().Contains(request.Search.ToLower())).ToList();

            return new Result<SupervisorEmployeesResponse>(new SupervisorEmployeesResponse(employees));
        }

        [HttpGet]
        public Result<EmployeePersonalInformationResponse> EmployeePersonalInformation([FromQuery] EmployeePersonalInformationRequest request)
        {
            var result = (from emp in _dbContext.Employees
                          where emp.Id == request.Id
                          select new EmployeePersonalInformationResponse(
                              emp.Id,
                              emp.Photo == null ? _configuration["NoPhotoPath"] : emp.Photo.Path,
                              emp.Photo == null ? false : emp.Photo.Hidden,
                              emp.PersonalInformation.FullName,
                              emp.PersonalInformation.Position,
                              emp.PersonalInformation.PhoneNumber,
                              emp.PersonalInformation.Email,
                              emp.PersonalInformation.DateOfBirth,
                              emp.PersonalInformation.PersonalId,
                              emp.PersonalInformation.Status,
                              emp.PersonalInformation.JobType,
                              emp.Id.ToString("0000")
                          )).FirstOrDefault();

            return new Result<EmployeePersonalInformationResponse>(result);
        }

        [HttpPost]
        public Result<IEntity[]> EditEmployeePersonalInformation([FromBody] EditEmployeePersonalInformationRequest request)
        {
            ////var user = _userManager.FindByEmailAsync(request.Email);
            //var user = _dbContext.Users.FirstOrDefault(u => u.Email == request.Email);

            //var action = new EditEmployeePersonalInformationBusinessProcessAction(
            //    request.Id,
            //    user.Id,
            //    request.FullName,
            //    request.Position,
            //    request.PhoneNumber,
            //    request.Email,
            //    request.DateOfBirth,
            //    request.PersonalId,
            //    request.Status,
            //    request.JobType
            //);

            //var entityStore = new EntityStore(_dbContext);

            //return new ActionHandler(
            //           new EditEmployeePersonalInformationBusinessProcess(_userManager)
            //       ).Save(entityStore, action);

            var action = new EditEmployeePersonalInformationAction(
                request.Id,
                request.FullName,
                request.Position,
                request.PhoneNumber,
                request.Email,
                request.DateOfBirth,
                request.PersonalId,
                request.Status,
                request.JobType
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditEmployeePersonalInformationBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<EmployeeJobInfoResponse> EmployeeJobInfo([FromQuery] EmployeeJobInfoRequest request)
        {
            var result = (from emp in _dbContext.Employees
                          where emp.Id == request.Id
                          select new EmployeeJobInfoResponse
                          {
                              Id = emp.Id,
                              Region = emp.JobInfo.Region,
                              WorkAddress = emp.JobInfo.WorkAddress,
                              IdDocument = new EmployeeFileModel(emp.JobInfo.IdDocument.Name, emp.JobInfo.IdDocument.Path, emp.JobInfo.IdDocument.Type),
                              IdExpirationDate = emp.JobInfo.IdExpirationDate,
                              Department = emp.JobInfo.Department,
                              TimeZone = new TimeZoneModel(emp.JobInfo.TimeZone.ToString(), GetDescription(emp.JobInfo.TimeZone))
                          }).FirstOrDefault();

            return new Result<EmployeeJobInfoResponse>(result);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UploadIdDocument([FromForm] UploadEmployeeFileRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new UploadIdDocumentAction(
                request.EmployeeId,
                file);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UploadIdDocumentBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public TimeZonesResponse TimeZones()
        {
            var timeZones = new List<TimeZoneModel>();

            foreach (TimeZoneEnum timezone in Enum.GetValues(typeof(TimeZoneEnum)))
            {

                timeZones.Add(new TimeZoneModel(
                    timezone.ToString(),
                    GetDescription(timezone))
                    );
            }

            return new TimeZonesResponse(
                timeZones,
                new TimeZoneModel(
                    TimeZoneEnum.Abu_Dhabi_Muscat_Baku_Tbilisi.ToString(),
                    GetDescription(TimeZoneEnum.Abu_Dhabi_Muscat_Baku_Tbilisi))
                );
        }

        [HttpGet]
        public Result<MonthsResponse> Months()
        {
            var monthResources = GetResourceDictionary(typeof(MonthResources));

            var response = JsonConvert.DeserializeObject<MonthsResponse>(JsonConvert.SerializeObject(monthResources));

            return new Result<MonthsResponse>(response);
        }

        private static Dictionary<string, string> GetResourceDictionary(Type resourceType)
        {
            var resourceManager = new ResourceManager(resourceType);
            var resourceSet = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            var resourceList = new Dictionary<string, string>();

            foreach (DictionaryEntry entry in resourceSet)
            {
                resourceList.Add(entry.Key.ToString(), entry.Value.ToString());
            }

            return resourceList;
        }

        public static string GetDescription(Enum value)
        {
            if (value is null)
                return string.Empty;

            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        [HttpPost]
        public Result<IEntity[]> EditEmployeeJobInfo([FromBody] EditEmployeeJobInfoRequest request)
        {
            var action = new EditEmployeeJobInfoAction(
                request.Id,
                request.Region,
                request.WorkAddress,
                request.IdExpirationDate,
                request.Department,
                request.TimeZone
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditEmployeeJobInfoBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UpdateEmployeeProfilePhoto([FromForm] UpdateEmployeeProfilePhotoRequest request)
        {
            var photo = await _photoUploadService.UploadPhotoAsync(request.Photo);

            var action = new UpdateEmployeeProfilePhotoAction(
                request.Id,
                photo.FileName,
                photo.FilePath
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UpdateEmployeeProfilePhotoBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> HideEmployeeProfilePhoto([FromBody] HideEmployeeProfilePhotoRequest request)
        {
            var action = new HideEmployeeProfilePhotoAction(
                request.Id,
                request.Hidden
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new HideEmployeeProfilePhotoBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteEmployeeProfilePhoto([FromForm] DeleteEmployeeProfilePhotoRequest request)
        {
            var action = new DeleteEmployeeProfilePhotoAction(
                request.Id
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteEmployeeProfilePhotoBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<EmployeeEducationResponse> EmployeeEducation([FromQuery] EmployeeEducationRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<EmployeeEducationResponse>(ErrorResources.Unauthorized);

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var result = (from emp in _dbContext.Employees.Include(e => e.PersonalInformation)
                          where emp.Id == request.Id
                          let trainings = from training in emp.Trainings
                                          select new EmployeeTrainingModel(
                                              training.Id,
                                              training.Training,
                                              new EmployeeFileModel(
                                                  training.Certificate.Name,
                                                  training.Certificate.Path,
                                                  training.Certificate.Type)
                                          )
                          let universities = from university in emp.Universities
                                             select new EmployeeUniversityModel(
                                                 university.Id,
                                                 university.University,
                                                 university.Faculty,
                                                 university.StartDate,
                                                 university.EndDate,
                                                 new EmployeeFileModel(
                                                     university.Certificate.Name, 
                                                     university.Certificate.Path, 
                                                     university.Certificate.Type)
                                             )
                          select new EmployeeEducationResponse(
                              emp.Id,
                              emp.PersonalInformation.Email == user.Email,
                              trainings.ToList(),
                              universities.ToList()
                          )).FirstOrDefault();

            return new Result<EmployeeEducationResponse>(result);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> AddEmployeeTraining([FromForm] AddEmployeeTrainingRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new AddEmployeeTrainingAction(
                request.EmployeeId,
                request.Training,
                file 
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddEmployeeTrainingBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> EditEmployeeTraining([FromForm] EditEmployeeTrainingRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new EditEmployeeTrainingAction(
                request.EmployeeId,
                request.TrainingId,
                request.Training,
                file
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditEmployeeTrainingBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteEmployeeTraining([FromBody] DeleteEmployeeTrainingyRequest request)
        {
            var action = new DeleteEmployeeTrainingAction(
                request.EmployeeId,
                request.TrainingId
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteEmployeeTrainingBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> AddEmployeeUniversity([FromForm] AddEmployeeUniversityRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new AddEmployeeUniversityAction(
                request.EmployeeId,
                request.University,
                request.Faculty,
                request.StartDate,
                request.EndDate,
                file
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddEmployeeUniversityBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> EditEmployeeUniversity([FromForm] EditEmployeeUniversityRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new EditEmployeeUniversityAction(
                request.EmployeeId,
                request.UniversityId,
                request.University,
                request.Faculty,
                request.StartDate,
                request.EndDate,
                file
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditEmployeeUniversityBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteEmployeeUniversity([FromBody] DeleteEmployeeUniversityRequest request)
        {
            var action = new DeleteEmployeeUniversityAction(
                request.EmployeeId,
                request.UniversityId
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteEmployeeUniversityBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<EmployeePayrollBasicResponse> EmployeePayrollBasic([FromQuery] EmployeePayrollBasicRequest request)
        {
            var result = (from emp in _dbContext.Employees
                          where emp.Id == request.Id
                          select new EmployeePayrollBasicResponse
                          {
                              Id = emp.Id,
                              Bank = new BankModel(emp.PayrollBasic.Bank.ToString(), GetDescription(emp.PayrollBasic.Bank)),
                              BankCode = emp.PayrollBasic.BankCode,
                              AccountNumber = emp.PayrollBasic.AccountNumber,
                              Residency = emp.PayrollBasic.Residency,
                              GrossSalary = emp.PayrollBasic.GrossSalary,
                              NetSalary = emp.PayrollBasic.NetSalary,
                              IncomeTax = emp.PayrollBasic.IncomeTax,
                              CompanyPension = emp.PayrollBasic.CompanyPension,
                              EmployeePension = emp.PayrollBasic.EmployeePension,
                              ParticipationInPension = emp.PayrollBasic.ParticipationInPension
                          }).FirstOrDefault();

            return new Result<EmployeePayrollBasicResponse>(result);
        }

        [HttpGet]
        public BanksResponse Banks()
        {
            var banks = new List<BankModel>();

            foreach (Bank bank in Enum.GetValues(typeof(Bank)))
            {

                banks.Add(new BankModel(
                    bank.ToString(),
                    GetDescription(bank))
                    );
            }

            return new BanksResponse(banks);
        }

        [HttpPost]
        public Result<IEntity[]> EditEmployeePayrollBasic([FromBody] EditEmployeePayrollBasicRequest request)
        {
            var action = new EditEmployeePayrollBasicAction(
                request.Id,
                request.Bank,
                request.BankCode,
                request.AccountNumber,
                request.Residency,
                request.ParticipationInPension,
                request.GrossSalary,
                request.NetSalary,
                request.IncomeTax,
                request.CompanyPension,
                request.EmployeePension
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditEmployeePayrollBasicBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<EmployeeSalaryCalculationResponse> EmployeeSalaryCalculation([FromQuery] EmployeeSalaryCalculationRequest request)
        {
            var calculation = new EmployeeSalaryCalculationResponse();

            if (request.Retirement == ParticipationInPension.Yes)
            {
                calculation.GrossSalary = Math.Round(request.NetSalary / 0.8m / 0.98m, 2, MidpointRounding.AwayFromZero);
                calculation.IncomeTax = Math.Round(request.NetSalary / 0.8m * 0.2m, 2, MidpointRounding.AwayFromZero);
                calculation.CompanyPension = Math.Round(request.NetSalary / 0.8m / 0.98m * 0.02m, 2, MidpointRounding.AwayFromZero);
                calculation.EmployeePension = Math.Round(request.NetSalary / 0.8m / 0.98m * 0.02m, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                calculation.GrossSalary = Math.Round(request.NetSalary / 0.8m, 2, MidpointRounding.AwayFromZero);
                calculation.IncomeTax = Math.Round(request.NetSalary / 0.8m * 0.2m, 2, MidpointRounding.AwayFromZero);
                calculation.CompanyPension = 0;
                calculation.EmployeePension = 0;
            }

            return new Result<EmployeeSalaryCalculationResponse>(calculation);
        }

        [HttpGet]
        public Result<SkillsAndLanguageResponse> SkillsAndLanguage([FromQuery] SkillsAndLanguageRequest request)
        {
            if (!User.Identity.IsAuthenticated)
                return new Result<SkillsAndLanguageResponse>(ErrorResources.Unauthorized);

            var user = _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value).Result;

            var result = (from emp in _dbContext.Employees.Include(e => e.PersonalInformation)
                          where emp.Id == request.Id
                          let skills = from skill in emp.ComputerSkills
                                       select new ComputerSkillModel(
                                           skill.Id,
                                           skill.Skill
                                       )
                          let languages = from language in emp.Languages
                                          select new LanguageModel(
                                              language.Id,
                                              language.Language
                                          )
                          select new SkillsAndLanguageResponse(
                              emp.Id,
                              emp.PersonalInformation.Email == user.Email,
                              new EmployeeFileModel(emp.EmploymentHistory.Resume.Name, emp.EmploymentHistory.Resume.Path, emp.EmploymentHistory.Resume.Type),
                              skills.ToList(),
                              languages.ToList()
                          )).FirstOrDefault();

            return new Result<SkillsAndLanguageResponse>(result);
        }

        [HttpPost]
        public Result<IEntity[]> AddComputerSkill([FromBody] AddComputerSkillRequest request)
        {
            var action = new AddComputerSkillAction(
                request.EmployeeId,
                request.Skill);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddComputerSkillBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteComputerSkill([FromBody] DeleteComputerSkillRequest request)
        {
            var action = new DeleteComputerSkillAction(
                request.EmployeeId,
                request.SkillId);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteComputerSkillBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> AddLanguage([FromBody] AddLanguageRequest request)
        {
            var action = new AddLanguageAction(
                request.EmployeeId,
                request.Language);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddLanguageBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteLanguage([FromBody] DeleteLanguageRequest request)
        {
            var action = new DeleteLanguageAction(
                request.EmployeeId,
                request.LanguageId);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteLanguageBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> AddEmployeeToRole([FromBody] AddEmployeeToRoleRequest request)
        {
            var employee = _dbContext.Employees.Include(e => e.PersonalInformation).FirstOrDefault(e => e.Id == request.UserId);

            var action = new AddEmployeeAction(
                request.RoleId,
                employee.Id,
                employee.PersonalInformation.FullName,
                employee.PersonalInformation.Position
            );

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddEmployeeBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<EmploymentHistoryResponse> EmploymentHistory([FromQuery] EmploymentHistoryRequest request)
        {
            var roles = _dbContext.Roles
                .Include(r => r.Users)
                .Where(r => r.Users.Any(u => u.UserId == request.Id))
                .Select(r => new EmployeeRolesModel(
                    r.Id,
                    r.Name
                    )
                ).ToList();

            var result = (from emp in _dbContext.Employees
                          where emp.Id == request.Id
                          let formerPositions = from formerPosition in emp.FormerPositions
                                                select new FormerPositionModel(
                                                    formerPosition.Id,
                                                    formerPosition.Title,
                                                    formerPosition.StartDate,
                                                    formerPosition.EndDate
                                                )
                          select new EmploymentHistoryResponse(
                              emp.Id,
                              emp.EmploymentHistory.ContractType,
                              emp.EmploymentHistory.JobStartDate,
                              new EmployeeFileModel(emp.EmploymentHistory.Contract.Name, emp.EmploymentHistory.Contract.Path, emp.EmploymentHistory.Contract.Type),
                              emp.EmploymentHistory.ContractExpirationDate,
                              emp.EmploymentHistory.Supervisor,
                              (from sup in _dbContext.Employees where sup.PersonalInformation.Email == emp.EmploymentHistory.Supervisor select sup.PersonalInformation.FullName).FirstOrDefault(),
                              formerPositions.ToList(),
                              roles
                          )).FirstOrDefault();

            return new Result<EmploymentHistoryResponse>(result);
        }

        [HttpGet]
        public Result<ContractTypesResponse> ContractTypes()
        {
            var result = _dbContext.ContractTypes.Select(c => c.Name).ToList();

            return new Result<ContractTypesResponse>(new ContractTypesResponse(result));
        }

        [HttpPost]
        public Result<IEntity[]> EditEmploymentHistory([FromBody] EditEmploymentHistoryRequest request)
        {
            var action = new EditEmploymentHistoryAction(
                request.EmployeeId,
                request.JobStartDate,
                request.ContractExpirationDate,
                request.Supervisor,
                request.ContractType
            );

            var entityStore = new EntityStore(_dbContext);

            return new ActionHandler(
                       new EditEmploymentHistoryBehavior()
                   ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UploadContract([FromForm] UploadEmployeeFileRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new UploadContractAction(
                request.EmployeeId,
                file);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UploadContractBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UploadResume([FromForm] UploadEmployeeFileRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new UploadResumeAction(
                request.EmployeeId,
                file);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UploadResumeBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> AddFormerPosition([FromBody] AddFormerPositionRequest request)
        {
            var action = new AddFormerPositionAction(
                request.EmployeeId,
                request.Title,
                request.StartDate,
                request.EndDate);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new AddFormerPositionBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> EditFormerPosition([FromBody] EditFormerPositionRequest request)
        {
            var action = new EditFormerPositionAction(
                request.EmployeeId,
                request.FormerPositionId,
                request.Title,
                request.StartDate,
                request.EndDate);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditFormerPositionBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public Result<IEntity[]> DeleteFormerPosition([FromBody] DeleteFormerPositionRequest request)
        {
            var action = new DeleteFormerPositionAction(
                request.EmployeeId,
                request.FormerPositionId);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new DeleteFormerPositionBehavior()
                        ).Save(entityStore, action);
        }

        [HttpGet]
        public Result<OtherInformationResponse> OtherInformation([FromQuery] OtherInformationRequest request)
        {
            var result = (from emp in _dbContext.Employees
                          where emp.Id == request.Id
                          select new OtherInformationResponse
                          {
                              Id = emp.Id,
                              OtherInformation = new OtherInformationModel(
                                  emp.OtherInformation.Gender,
                                  emp.OtherInformation.MaritalStatus,
                                  emp.OtherInformation.LegalAddress,
                                  emp.OtherInformation.ActualAddress,
                                  new EmployeeFileModel(emp.OtherInformation.BloodGroupAndRhesus.Name, emp.OtherInformation.BloodGroupAndRhesus.Path, emp.OtherInformation.BloodGroupAndRhesus.Type),
                                  new EmployeeFileModel(emp.OtherInformation.MedicalCertificate.Name, emp.OtherInformation.MedicalCertificate.Path, emp.OtherInformation.MedicalCertificate.Type),
                                  new EmployeeFileModel(emp.OtherInformation.Alergies.Name, emp.OtherInformation.Alergies.Path, emp.OtherInformation.Alergies.Type),
                                  new EmployeeFileModel(emp.OtherInformation.DrivingLicense.Name, emp.OtherInformation.DrivingLicense.Path, emp.OtherInformation.DrivingLicense.Type),
                                  emp.OtherInformation.Conviction
                                  ),
                              EmergencyContact = new EmergencyContactModel(
                                  emp.EmergencyContact.FullName,
                                  emp.EmergencyContact.Relationship,
                                  emp.EmergencyContact.PhoneNumber
                                  )
                          }).FirstOrDefault();

            return new Result<OtherInformationResponse>(result);
        }

        [HttpPost]
        public Result<IEntity[]> EditOtherInformation([FromBody] EditOtherInformationRequest request)
        {
            var action = new EditOtherInformationAction(
                request.EmployeeId,
                request.Gender,
                request.MaritalStatus,
                request.LegalAddress,
                request.ActualAddress,
                request.Conviction,
                request.FullName,
                request.Relationship,
                request.PhoneNumber);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new EditOtherInformationBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UploadBloodGroupAndRhesus([FromForm] UploadEmployeeFileRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new UploadBloodGroupAndRhesusAction(
                request.EmployeeId,
                file);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UploadBloodGroupAndRhesusBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UploadMedicalCertificate([FromForm] UploadEmployeeFileRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new UploadMedicalCertificateAction(
                request.EmployeeId,
                file);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UploadMedicalCertificateBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UploadAlergies([FromForm] UploadEmployeeFileRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new UploadAlergiesAction(
                request.EmployeeId,
                file);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UploadAlergiesBehavior()
                        ).Save(entityStore, action);
        }

        [HttpPost]
        public async Task<Result<IEntity[]>> UploadDrivingLicense([FromForm] UploadEmployeeFileRequest request)
        {
            var file = await _fileUploadService.UploadFileAsync(request.File);

            var action = new UploadDrivingLicenseAction(
                request.EmployeeId,
                file);

            var entityStore = new EntityStore(_dbContext);
            return new ActionHandler(
                            new UploadDrivingLicenseBehavior()
                        ).Save(entityStore, action);
        }
    }
}
