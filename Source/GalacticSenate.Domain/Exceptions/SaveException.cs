using System;
using System.Collections.Generic;

namespace GalacticSenate.Domain.Exceptions {
   public class SaveException : GalacticSenateException
    {
        public SaveException(List<string> messages, List<string> entries, Exception innerException) : base(messages, innerException)
        {
            Entries = entries ?? throw new ArgumentNullException(nameof(entries));
        }

        public List<string> Entries { get; private set; } = new List<string>();
    }
}
