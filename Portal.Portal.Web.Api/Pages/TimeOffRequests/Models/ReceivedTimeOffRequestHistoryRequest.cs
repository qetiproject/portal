using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Application.TimeOffRequestsModule.Resources;
using System.ComponentModel.DataAnnotations;

namespace Portal.Portal.Web.Api.Pages.TimeOffRequests.Models
{
    public class ReceivedTimeOffRequestHistoryRequest
    {
        [Required]
        public string Number { get; set; }
    }

    public class ReceivedTimeOffRequestHistoryResponse
    {
        public ReceivedTimeOffRequestHistoryResponse(
            TimeOffRequestType type,
            string number,
            string title,
            string sender,
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
            List<RedirectModel> redirects,
            bool isStatusChanger)
        {
            Type = type;
            Number = number;
            Title = title;
            Sender = sender;
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
            IsStatusChanger = isStatusChanger;
            StatusChangeDate = statusChangeDate;
        }

        public TimeOffRequestType Type { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        public string Sender { get; set; }

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

        public bool IsStatusChanger { get; set; }
    }

    public class RedirectModel
    {
        public RedirectModel(
            string redirecter,
            string participant,
            string? comment,
            TimeOffRequestFileModel? file,
            bool hasFile)
        {
            Redirecter = redirecter;
            Participant = participant;
            Comment = comment;
            File = file;
            HasFile = hasFile;
        }

        public string Redirecter { get; set; }

        public string Participant { get; set; }

        public string? Comment { get; set; }

        public TimeOffRequestFileModel? File { get; set; }

        public bool HasFile { get; set; }

        public string FileIconName { get; set; }

        public string Title
        {
            get
            {
                return string.Format("{0} | {1} {2} {3}", string.IsNullOrEmpty(this.Comment) ? string.Empty : TimeOffRequestResources.Comment, this.Redirecter, TimeOffRequestResources.SentTo, this.Participant);
            }
        }
    }
}
