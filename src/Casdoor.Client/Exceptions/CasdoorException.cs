namespace Casdoor.Client.Exceptions
{
    public class CasdoorException : Exception
    {
        public CasdoorException() : base() { }

        public CasdoorException(string message) : base(message) { }

        public CasdoorException(string message, Exception innerException) : base(message, innerException) { }

    }
}
