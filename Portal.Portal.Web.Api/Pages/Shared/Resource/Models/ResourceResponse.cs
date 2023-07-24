using Newtonsoft.Json.Linq;
using Portal.Portal.Application.EmployeeServiceModule.Models.ChildModels;
using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using Portal.Portal.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Portal.Portal.Web.Api.Pages.Shared.Resource.Models
{
    public class ResourceResponse
    {
        public ErrorResourceModel ErrorResources { get; set; }

        public EmploymentModuleResourceModel EmploymentModuleResources { get; set; }

        public AuthorizationModuleResourceModel AuthorizationModuleResources { get; set; }

        public TimeOffRequestResourcesModel TimeOffRequestResources { get; set; }

        public LayoutResourcesModel LayoutResources { get; set; }

        public MonthResourcesModel MonthResources { get; set; }

        public NonComplianceResourcesModel NonComplianceResources { get; set; }

        public ParametresResourceModel ParametresResources { get; set; }

        public RolesModuleResourcesModel RolesResources { get; set; }

        public ResourceResponse(
            ErrorResourceModel errorResources,
            EmploymentModuleResourceModel employmentModuleResources,
            AuthorizationModuleResourceModel authorizationModuleResources,
            TimeOffRequestResourcesModel timeOffRequestResources,
            LayoutResourcesModel layoutResources,
            MonthResourcesModel monthResources,
            NonComplianceResourcesModel nonComplianceResources,
            ParametresResourceModel parametresResources,
            RolesModuleResourcesModel rolesResources)
        {
            ErrorResources = errorResources;
            EmploymentModuleResources = employmentModuleResources;
            AuthorizationModuleResources = authorizationModuleResources;
            TimeOffRequestResources = timeOffRequestResources;
            LayoutResources = layoutResources;
            MonthResources = monthResources;
            NonComplianceResources = nonComplianceResources;
            ParametresResources = parametresResources;
            RolesResources = rolesResources;
        }
    }
}
