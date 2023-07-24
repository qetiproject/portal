using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{

    public class TimeOffRequestHistoryRequest
    {
        [Required]
        public string Number { get; set; }
    }

    public class TimeOffRequestHistoryResponse
    {
        public TimeOffRequestHistoryResponse(
            TimeOffRequestType type,
            string number,
            string title,
            string sender,
            string receiver,
            DateTime date,
            DateTime? deadLine,
            DateTime? from,
            DateTime? including,
            TimeOffRequestFileModel? file,
            string? description,
            TimeOffRequestStatus status,
            string? statusChanger,
            string? statusComment,
            DateTime? statusChangeDate,
            TimeOffRequestFileModel? statusFile,
            List<RedirectModel> redirects)
        {
            Type = type;
            Number = number;
            Title = title;
            Sender = sender;
            Receiver = receiver;
            Date = date;
            DeadLine = deadLine;
            From = from;
            Including = including;
            File = file;
            Description = description;
            Status = status;
            StatusChanger = statusChanger;
            StatusComment = statusComment;
            StatusFile = statusFile;
            Redirects = redirects;
            StatusChangeDate = statusChangeDate;
        }

        public TimeOffRequestType Type { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public DateTime Date { get; set; }

        public DateTime? DeadLine { get; set; }

        public DateTime? From { get; set; }

        public DateTime? Including { get; set; }

        public TimeOffRequestFileModel? File { get; set; }

        public string? Description { get; set; }

        public TimeOffRequestStatus Status { get; set; }

        public string? StatusChanger { get; set; }

        public string? StatusComment { get; set; }

        public DateTime? StatusChangeDate { get; set; }

        public TimeOffRequestFileModel? StatusFile { get; set; }

        public List<RedirectModel> Redirects { get; set; }
    }
}
