namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class EmployeeEducationRequest
    {
        public int Id { get; set; }
    }

    public class EmployeeEducationResponse
    {
        public EmployeeEducationResponse(
            int id,
            bool isMyProfile,
            List<EmployeeTrainingModel> trainings,
            List<EmployeeUniversityModel> universities)
        {
            Id = id;
            IsMyProfile = isMyProfile;
            Trainings = trainings;
            Universities = universities;
        }

        public int Id { get; set; }

        public bool IsMyProfile { get; set; }

        public List<EmployeeTrainingModel> Trainings { get; set; } = new List<EmployeeTrainingModel>();

        public List<EmployeeUniversityModel> Universities { get; set; } = new List<EmployeeUniversityModel>();
    }

    public class EmployeeUniversityModel
    {
        public EmployeeUniversityModel(
            int id,
            string university,
            string faculty,
            DateTime startDate,
            DateTime endDate,
            EmployeeFileModel? cerificate)
        {
            Id = id;
            University = university;
            Faculty = faculty;
            StartDate = startDate;
            EndDate = endDate;
            Cerificate = cerificate;
        }

        public int Id { get; set; }

        public string University { get; set; }

        public string Faculty { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public EmployeeFileModel? Cerificate { get; set; }
    }

    public class EmployeeTrainingModel
    {
        public EmployeeTrainingModel(
            int id,
            string training,
            EmployeeFileModel? cerificate)
        {
            Id = id;
            Training = training;
            Cerificate = cerificate;
        }

        public int Id { get; set; }

        public string Training { get; set; }

        public EmployeeFileModel? Cerificate { get; set; }
    }
}
