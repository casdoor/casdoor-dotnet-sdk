using System.Data;
using System.Text;

namespace Casdoor.Client;

public static class Util
{
    public static string GetUrl(CasdoorUserClient casdoorClient, string action, Dictionary<string, string> queryMap)
    {
        StringBuilder queryBuilder = new StringBuilder();
        foreach ((string k, string v) in queryMap)
        {
            queryBuilder.Append($"{k}={v}&");
        }

        queryBuilder.Remove(queryBuilder.Length - 1, 1);  // remove the last (a.k.a. redundant) `&`
        string query = queryBuilder.ToString();

        string url = $"{casdoorClient._endpoint}/api/{action}?{query}";
        return url;
    }

    public static Tuple<string, IDataReader> CreateForm(Dictionary<string, byte[]> formData)
    {
        var body = new MemoryStream();
        var w = new StreamWriter(body);

        foreach ((string k, byte[] v) in formData)
        {
        }
    }
}
