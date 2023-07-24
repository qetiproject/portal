namespace Portal.Portal.Web.Api.Pages.AdminPanel.Models
{
    public class ContractTypesListRequest
    {
        public string? Search { get; set; }
    }

    public class ContractTypesListResponse
    {
        public ContractTypesListResponse(List<ContractTypesListModel> cotractTypes)
        {
            ContractTypes = cotractTypes;
        }

        public List<ContractTypesListModel> ContractTypes { get; set; }
    }

    public class ContractTypesListModel
    {
        public ContractTypesListModel(int id, string cotractType)
        {
            Id = id;
            ContractType = cotractType;
        }

        public int Id { get; set; }

        public string ContractType { get; set; }
    }
}
