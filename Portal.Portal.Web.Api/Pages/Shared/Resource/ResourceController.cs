using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portal.Portal.Application.EmployeeServiceModule.Resources;
using Portal.Portal.Application.NonComplianceModule.Resources;
using Portal.Portal.Application.ParametresModule.Resources;
using Portal.Portal.Application.RolesModule.Resources;
using Portal.Portal.Application.TimeOffRequestsModule.Resources;
using Portal.Portal.Common;
using Portal.Portal.Web.Api.Pages.Employee.Models;
using Portal.Portal.Web.Api.Pages.Shared.Resource.Models;
using Portal.Portal.Web.Api.Resources;
using System.Collections;
using System.Globalization;
using System.Resources;

namespace Portal.Portal.Web.Api.Pages.Shared.Resource
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResourceController
    {
        [HttpGet]
        public ResourceResponse GetResources()
        {
            var errorResources = GetResourceDictionary(typeof(ErrorResources));
            var employmentModuleResources = GetResourceDictionary(typeof(EmployementModuleResources));
            var authorizationModuleResources = GetResourceDictionary(typeof(AuthorizationModuleResources));
            var timeOffRequestResources = GetResourceDictionary(typeof(TimeOffRequestResources));
            var layoutResources = GetResourceDictionary(typeof(LayoutResources));
            var monthResources = GetResourceDictionary(typeof(MonthResources));
            var nonComplianceResources = GetResourceDictionary(typeof(NonComplianceResources));
            var parametresResources = GetResourceDictionary(typeof(ParametresResources));
            var rolesModuleResources = GetResourceDictionary(typeof(RolesModuleResources));

            var resourceResponse = new ResourceResponse(
                JsonConvert.DeserializeObject<ErrorResourceModel>(JsonConvert.SerializeObject(errorResources)),
                JsonConvert.DeserializeObject<EmploymentModuleResourceModel>(JsonConvert.SerializeObject(employmentModuleResources)),
                JsonConvert.DeserializeObject<AuthorizationModuleResourceModel>(JsonConvert.SerializeObject(authorizationModuleResources)),
                JsonConvert.DeserializeObject<TimeOffRequestResourcesModel>(JsonConvert.SerializeObject(timeOffRequestResources)),
                JsonConvert.DeserializeObject<LayoutResourcesModel>(JsonConvert.SerializeObject(layoutResources)),
                JsonConvert.DeserializeObject<MonthResourcesModel>(JsonConvert.SerializeObject(monthResources)),
                JsonConvert.DeserializeObject<NonComplianceResourcesModel>(JsonConvert.SerializeObject(nonComplianceResources)),
                JsonConvert.DeserializeObject<ParametresResourceModel>(JsonConvert.SerializeObject(parametresResources)),
                JsonConvert.DeserializeObject<RolesModuleResourcesModel>(JsonConvert.SerializeObject(rolesModuleResources))
            );

            return resourceResponse;
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
    }
}
