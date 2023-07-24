using System.Reflection;
using FluentValidation;
using FluentValidation.Results;

namespace Portal.Portal.Common
{
    public abstract class Action
    {
        public Action()
        {

        }

        public Action(object id)
        {
            Id = id;
        }

        public object Id { get; set; }
    }
}