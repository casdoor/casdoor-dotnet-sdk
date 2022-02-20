using System.Runtime.Serialization;

namespace Casdoor.Client.Exception;

public class CasdoorException : System.Exception
{
    public CasdoorException()
    {
    }

    public CasdoorException(string? msg) : base(msg)
    {
    }

    public CasdoorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CasdoorException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
