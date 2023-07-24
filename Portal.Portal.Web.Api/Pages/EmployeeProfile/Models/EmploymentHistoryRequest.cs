using Portal.Portal.Common;

namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EmploymentHistoryRequest
    {
        public int Id { get; set; }
    }

    public class EmploymentHistoryResponse
    {
        public EmploymentHistoryResponse(
            int id,
            string? contractType,
            DateTime? jobStartDate,
            EmployeeFileModel? contract,
            DateTime? contractExpirationDate,
            string? supervisor,
            string? supervisorName,
            List<FormerPositionModel> formerPositions,
            List<EmployeeRolesModel> roles)
        {
            Id = id;
            ContractType = contractType;
            JobStartDate = jobStartDate;
            Contract = contract;
            ContractExpirationDate = contractExpirationDate;
            Supervisor = supervisor;
            FormerPositions = formerPositions;
            Roles = roles;
            SupervisorName = supervisorName;
        }

        public int Id { get; set; }

        public string? ContractType { get; set; }

        public DateTime? JobStartDate { get; set; }

        public EmployeeFileModel? Contract { get; set; }

        public DateTime? ContractExpirationDate { get; set; }

        public string? Supervisor { get; set; }

        public string? SupervisorName { get; set; }

        public List<FormerPositionModel> FormerPositions { get; set; } = new List<FormerPositionModel>();

        public List<EmployeeRolesModel> Roles { get; set; } = new List<EmployeeRolesModel>();
    }

    public class EmployeeRolesModel
    {
        public EmployeeRolesModel(
            int roleId,
            string name)
        {
            RoleId = roleId;
            Name = name;
        }

        public int RoleId { get; set; }

        public string Name { get; set; }
    }

    public class FormerPositionModel
    {
        public FormerPositionModel(
            int id,
            string title,
            DateTime startDate,
            DateTime endDate)
        {
            Id = id;
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class EmployeeFileModel
    {
        public EmployeeFileModel(
            string name,
            string path,
            FileType? type)
        {
            Name = name;
            Path = path;
            Type = type;
        }

        public string Name { get; set; }

        public string Path { get; set; }

        public FileType? Type { get; set; }
    }
}
