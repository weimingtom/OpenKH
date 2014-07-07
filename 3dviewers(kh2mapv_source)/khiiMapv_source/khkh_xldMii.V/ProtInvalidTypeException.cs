using System;

namespace khkh_xldMii.V
{
    public class ProtInvalidTypeException : ApplicationException
    {
        public ProtInvalidTypeException() : base("Has to be typ1 or typ2")
        {
        }
    }
}