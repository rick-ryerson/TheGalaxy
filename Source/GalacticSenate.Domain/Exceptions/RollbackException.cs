using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Domain.Exceptions
{
    public class RollbackException : GalacticSenateException
    {
        public RollbackException(List<string> messages, Exception innerException) : base(messages, innerException)
        {

        }

    }
}
