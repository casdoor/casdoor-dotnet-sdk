using System.Runtime.Serialization;

namespace Casdoor.Client.Exception;

public class CasdoorAuthException : CasdoorException
{
    public CasdoorAuthException()
    {
    }

    public CasdoorAuthException(string? msg) : base(msg)
    {
    }

    public CasdoorAuthException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CasdoorAuthException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
