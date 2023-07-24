namespace Portal.Portal.Web.Api.Pages.EmployeeProfile.Models
{
    public class TimeZonesResponse
    {
        public TimeZonesResponse(List<TimeZoneModel> timeZones, TimeZoneModel defaultTimeZone)
        {
            TimeZones = timeZones;
            DefaultTimeZone = defaultTimeZone;
        }

        public TimeZoneModel DefaultTimeZone { get; set; }

        public List<TimeZoneModel> TimeZones { get; set; }
    }

    public class TimeZoneModel
    {
        public TimeZoneModel(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
