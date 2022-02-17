using System.Runtime.Serialization;

namespace Casdoor.Client.Exception;

public class CasdoorApiException : System.Exception
{
    public CasdoorApiException()
    {
    }

    public CasdoorApiException(string? msg) : base(msg)
    {
    }

    public CasdoorApiException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public CasdoorApiException(string? message, System.Exception? innerException) : base(message, innerException)
    {
    }
}
