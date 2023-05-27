using System.Text.Json;

namespace Casdoor.Client;

public static class CasdoorResponseExtension
{
    internal static T? DeserializeData<T>(this CasdoorResponse? response)
    {
        if (response?.Status != "ok")
        {
            throw new CasdoorApiException(response?.Msg);
        }
        if (response.Data is JsonElement element)
        {
            return element.Deserialize<T>();
        }

        return default;
    }
}
