using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PeopleAndOrganizations.Domain.Exceptions
{
    public class SaveException : PeopleAndOrganizationsException
    {
        public SaveException(List<string> messages, List<string> entries, Exception innerException) : base(messages, innerException)
        {
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        public List<string> Entries { get; private set; } = new List<string>();
    }
}
