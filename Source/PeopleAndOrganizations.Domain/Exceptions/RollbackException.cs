using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Domain.Exceptions
{
    public class RollbackException : PeopleAndOrganizationsException
    {
        public RollbackException(List<string> messages, Exception innerException) : base(messages, innerException)
        {

        }

    }
}
