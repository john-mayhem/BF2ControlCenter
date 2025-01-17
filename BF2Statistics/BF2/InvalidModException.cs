﻿using System;

namespace BF2Statistics
{
    class InvalidModException : Exception
    {
        public InvalidModException() : base() { }

        public InvalidModException(string Message) : base(Message)  { }

        public InvalidModException(string Message, Exception InnerException) : base(Message, InnerException) { }
    }
}
