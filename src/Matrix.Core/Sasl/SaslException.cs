using System;

namespace Matrix.Sasl
{
    public class SaslException : Exception
    {
        public SaslException(string message) : base(message)
        {
        }
    }
}
