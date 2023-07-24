using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.EmployeeServiceModule.Models.Enums
{
    public enum TimeZoneEnum
    {
        [Description("(GMT -12:00) Eniwetok, Kwajalein")]
        Eniwetok_Kwajalein,
        [Description("(GMT -11:00) Midway Island, Samoa")]
        Midway_Island_Samoa,
        [Description("(GMT -10:00) Hawaii")]
        Hawaii,
        [Description("(GMT -9:30) Taiohae")]
        Taiohae,
        [Description("(GMT -9:00) Alaska")]
        Alaska,
        [Description("(GMT -8:00) Pacific Time (US & Canada)")]
        Pacific_Time_US_Canada,
        [Description("(GMT -7:00) Mountain Time (US & Canada)")]
        Mountain_Time_US_Canada,
        [Description("(GMT -6:00) Central Time (US & Canada), Mexico City")]
        Central_Time_US_Canada_Mexico_City,
        [Description("(GMT -5:00) Eastern Time (US & Canada), Bogota, Lima")]
        Eastern_Time_US_Canada_Bogota_Lima,
        [Description("(GMT -4:30) Caracas")]
        Caracas,
        [Description("(GMT -4:00) Atlantic Time (Canada), Caracas, La Paz")]
        Atlantic_Time_Canada_Caracas_La_Paz,
        [Description("(GMT -3:30) Newfoundland")]
        Newfoundland,
        [Description("(GMT -3:00) Brazil, Buenos Aires, Georgetown")]
        Brazil_Buenos_Aires_Georgetown,
        [Description("(GMT -2:00) Mid-Atlantic")]
        Mid_Atlantic,
        [Description("(GMT -1:00) Azores, Cape Verde Islands")]
        Azores_Cape_Verde_Islands,
        [Description("(GMT) Western Europe Time, London, Lisbon, Casablanca")]
        Western_Europe_Time_London_Lisbon_Casablanca,
        [Description("(GMT +1:00) Brussels, Copenhagen, Madrid, Paris")]
        Brussels_Copenhagen_Madrid_Paris,
        [Description("(GMT +2:00) Kaliningrad, South Africa")]
        Kaliningrad_South_Africa,
        [Description("(GMT +3:00) Baghdad, Riyadh, Moscow, St. Petersburg")]
        Baghdad_Riyadh_Moscow_St_Petersburg,
        [Description("(GMT +3:30) Tehran")]
        Tehran,
        [Description("(GMT +4:00) Abu Dhabi, Muscat, Baku, Tbilisi")]
        Abu_Dhabi_Muscat_Baku_Tbilisi,
        [Description("(GMT +4:30) Kabul")]
        Kabul,
        [Description("(GMT +5:00) Ekaterinburg, Islamabad, Karachi, Tashkent")]
        Ekaterinburg_Islamabad_Karachi_Tashkent,
        [Description("(GMT +5:30) Bombay, Calcutta, Madras, New Delhi")]
        Bombay_Calcutta_Madras_New_Delhi,
        [Description("(GMT +5:45) Kathmandu, Pokhara")]
        Kathmandu_Pokhara,
        [Description("(GMT +6:00) Almaty, Dhaka, Colombo")]
        Almaty_Dhaka_Colombo,
        [Description("(GMT +6:30) Yangon, Mandalay")]
        Yangon_Mandalay,
        [Description("(GMT +7:00) Bangkok, Hanoi, Jakarta")]
        Bangkok,
        [Description("(GMT +8:00) Beijing, Perth, Singapore, Hong Kong")]
        Beijing,
        [Description("(GMT +8:45) Eucla")]
        Eucla,
        [Description("(GMT +9:00) Tokyo, Seoul, Osaka, Sapporo, Yakutsk")]
        Tokyo,
        [Description("(GMT +9:30) Adelaide, Darwin")]
        Adelaide,
        [Description("(GMT +10:00) Eastern Australia, Guam, Vladivostok")]
        EasternAustralia,
        [Description("(GMT +10:30) Lord Howe Island")]
        LordHoweIsland,
        [Description("(GMT +11:00) Magadan, Solomon Islands, New Caledonia")]
        Magadan,
        [Description("(GMT +11:30) Norfolk Island")]
        NorfolkIsland,
        [Description("(GMT +12:00) Auckland, Wellington, Fiji, Kamchatka")]
        Auckland,
        [Description("(GMT +12:45) Chatham Islands")]
        ChathamIslands,
        [Description("(GMT +13:00) Apia, Nukualofa")]
        Apia,
        [Description("(GMT +14:00) Line Islands, Tokelau")]
        LineIslands
    }
}
