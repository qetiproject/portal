using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class SentTimeOffRequestDetailsRequest
    {
        [Required]
        public string Number { get; set; }
    }

    public class SentTimeOffRequestDetailsResponse
    {
        public SentTimeOffRequestDetailsResponse(
            TimeOffRequestType type,
            string number,
            string title,
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
            TimeOffRequestFileModel? statusFile)
        {
            Type = type;
            Number = number;
            Title = title;
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
            StatusChangeDate = statusChangeDate;
        }

        public TimeOffRequestType Type { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

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
    }
}
