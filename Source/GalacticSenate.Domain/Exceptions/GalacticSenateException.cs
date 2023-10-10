using System;
using System.Collections.Generic;

namespace GalacticSenate.Domain.Exceptions {
   public class GalacticSenateException : ApplicationException
    {
        public GalacticSenateException()
        {
        }

        public GalacticSenateException(string message) : base(message)
        {
            Messages.Add(message);
        }
        public GalacticSenateException(List<string> messages) : base("See Messages")
        {
            Messages = messages;
        }

        public GalacticSenateException(List<string> messages, Exception innerException) : base("See Messages", innerException)
        {
            Messages = messages;
        }

        public GalacticSenateException(string message, Exception innerException) : base(message, innerException)
        {
            Messages.Add(message);
        }

        public List<string> Messages { get; } = new List<string>();
    }
}
