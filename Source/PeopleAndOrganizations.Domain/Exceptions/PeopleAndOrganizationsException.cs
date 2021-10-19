using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Domain.Exceptions
{
    public class PeopleAndOrganizationsException : ApplicationException
    {
        public PeopleAndOrganizationsException()
        {
        }

        public PeopleAndOrganizationsException(string message) : base(message)
        {
            Messages.Add(message);
        }
        public PeopleAndOrganizationsException(List<string> messages) : base("See Messages")
        {
            Messages = messages;
        }

        public PeopleAndOrganizationsException(List<string> messages, Exception innerException) : base("See Messages", innerException)
        {
            Messages = messages;
        }

        public PeopleAndOrganizationsException(string message, Exception innerException) : base(message, innerException)
        {
            Messages.Add(message);
        }

        public List<string> Messages { get; } = new List<string>();
    }
}
