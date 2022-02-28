namespace Casdoor.Client.Exceptions
{
    public class CasdoorAuthException : CasdoorException
    {
        public CasdoorAuthException(string message, Exception exp) : base(message, exp) { }

        public CasdoorAuthException(string message) : base(message) { }

    }
}
