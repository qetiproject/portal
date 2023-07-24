using Microsoft.AspNetCore.Http;
using Portal.Portal.Application.TimeOffRequestsModule.ChangeReceivedTimeOffRequestStatusFeature;
using Portal.Portal.Application.TimeOffRequestsModule.Models.Enums;
using Portal.Portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Portal.Application.TimeOffRequestsModule.CreateTimeOffRequestFeature
{
    [Validate(typeof(CreateTimeOffRequestActionBusinessRules))]
    public class CreateTimeOffRequestAction : Common.Action
    {
        public CreateTimeOffRequestAction(
            TimeOffRequestType type,
            string title,
            string sender,
            string receiver,
            DateTime? deadLine,
            DateTime? from,
            DateTime? including,
            FileUploadResponse? file,
            string? description)
        {
            Type = type;
            Title = title;
            Sender = sender;
            Receiver = receiver;
            DeadLine = deadLine;
            From = from;
            Including = including;
            File = file;
            Description = description;
        }

        public TimeOffRequestType Type { get; set; }

        public string Title { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }

        public DateTime? DeadLine { get; set; }

        public DateTime? From { get; set; }

        public DateTime? Including { get; set; }

        public FileUploadResponse? File { get; set; }

        public string? Description { get; set; }
    }
}
