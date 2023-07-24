namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class ContractTypesResponse
    {
        public ContractTypesResponse(List<string> contractTypes)
        {
            ContractTypes = contractTypes;
        }

        public List<string> ContractTypes { get; set; }
    }
}
