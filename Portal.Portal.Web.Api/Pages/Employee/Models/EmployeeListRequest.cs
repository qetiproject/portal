using Portal.Portal.Application.EmployeeServiceModule.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.Employee.Models
{
    public class EmployeeListRequest
    {
        public string? Search { get; set; }
    }

    public class EmployeeListResponse
    {
        [Required]
        public int Total { get; set; }

        [Required]
        public List<EmployeeListModel> Employees { get; set; }

        public EmployeeListResponse(int total, List<EmployeeListModel> employees)
        {
            Total = total;
            Employees = employees;
        }
    }

    public class EmployeeListModel
    {
        public EmployeeListModel(
            int id,
            string fullName,
            string photo,
            string position,
            string phoneNumber,
            string email,
            string department,
            string contractType,
            EmployeeStatus status)
        {
            Id = id;
            FullName = fullName;
            Photo = photo;
            Position = position;
            PhoneNumber = phoneNumber;
            Email = email;
            Department = department;
            ContractType = contractType;
            Status = status;
        }

        public int Id { get; set; }

        public string FullName { get; set; }

        public string Photo { get; set; }

        public string Position { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Department { get; set; }

        public string ContractType { get; set; }

        public EmployeeStatus Status { get; set; }
    }
}
