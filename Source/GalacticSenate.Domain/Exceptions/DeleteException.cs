﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GalacticSenate.Domain.Exceptions
{
    public class DeleteException : GalacticSenateException
    {
        public DeleteException(string message) : base(message)
        {
        }
    }
}
