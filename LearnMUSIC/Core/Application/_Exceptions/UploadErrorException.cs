using System;

namespace LearnMUSIC.Core.Application._Exceptions
{
    public class UploadErrorException : Exception
    {
        public UploadErrorException(string message)
            : base(message)
        {
        }
    }
}
