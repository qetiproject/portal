using Portal.Portal.Application.TimeOffRequestsModule.Models.ChildModels;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.Models
{
    public record TimeOffRequest : Record, IEntity
    {
        public TimeOffRequest()
        {
            Redirects = new HashSet<Redirect>();
        }

        public TimeOffRequest(
            TimeOffRequestType type,
            string title,
            string sender,
            string receiver,
            DateTime? deadline,
            DateTime? from,
            DateTime? including,
            string? description,
            TimeOffRequestFile? file
            ) : this()
        {
            Type = type;
            Title = title;
            Sender = sender;
            Receiver = receiver;
            Deadline = deadline;
            Description = description;
            File = file;
            Status = TimeOffRequestStatus.InProgress;
            CreateDate = DateTime.Now;
            From = from;
            Including = including;
        }

        public TimeOffRequestType Type { get; set; }

        public string Title { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public DateTime? Deadline { get; set; }

        public DateTime? From { get; set; }

        public DateTime? Including { get; set; }

        public string? Description { get; set; }

        public TimeOffRequestStatus Status { get; set; }

        public string? StatusChanger { get; set; }

        public string? StatusComment { get; set; }

        public DateTime? StatusChangeDate { get; set; }

        public TimeOffRequestFile? StatusFile { get; set; }

        public DateTime CreateDate { get; set; }

        public HashSet<Redirect> Redirects { get; set; }

        public TimeOffRequestFile? File { get; set; }
    }
}
