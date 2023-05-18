namespace Casdoor.Client;

internal class QueryMapBuilder
{
    private readonly Dictionary<string, string?> _map = new();

    public QueryMapBuilder Add(string key, string? value = null)
    {
        _map.Add(key, value);
        return this;
    }

    public IEnumerable<KeyValuePair<string, string?>> GetMap() => _map;
}
