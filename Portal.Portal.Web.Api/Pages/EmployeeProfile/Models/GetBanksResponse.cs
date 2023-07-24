namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class BanksResponse
    {
        public BanksResponse(List<BankModel> timeZones)
        {
            Banks = timeZones;
        }

        public List<BankModel> Banks { get; set; }
    }

    public class BankModel
    {
        public BankModel(string bank, string code)
        {
            Bank = bank;
            Code = code;
        }

        public string Bank { get; set; }

        public string Code { get; set; }
    }
}
