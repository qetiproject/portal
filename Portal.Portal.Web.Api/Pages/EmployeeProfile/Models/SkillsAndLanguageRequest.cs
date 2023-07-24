namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class SkillsAndLanguageRequest
    {
        public int Id { get; set; }
    }

    public class SkillsAndLanguageResponse
    {
        public SkillsAndLanguageResponse(
            int id,
            bool isMyProfile,
            EmployeeFileModel? resume,
            List<ComputerSkillModel> skills,
            List<LanguageModel> languages)
        {
            Id = id;
            IsMyProfile = isMyProfile;
            Resume = resume;
            Skills = skills;
            Languages = languages;
        }

        public int Id { get; set; }

        public bool IsMyProfile { get; set; }

        public EmployeeFileModel? Resume { get; set; }

        public List<ComputerSkillModel> Skills { get; set; } = new List<ComputerSkillModel>();

        public List<LanguageModel> Languages { get; set; } = new List<LanguageModel>();
    }

    public class ComputerSkillModel
    {
        public ComputerSkillModel(
            int id,
            string skill)
        {
            Id = id;
            Skill = skill;
        }

        public int Id { get; set; }

        public string Skill { get; set; }
    }

    public class LanguageModel
    {
        public LanguageModel(
            int id,
            string language)
        {
            Id = id;
            Language = language;
        }

        public int Id { get; set; }

        public string Language { get; set; }
    }
}
