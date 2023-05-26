namespace Casdoor.Client;

internal readonly struct QueryMapBuilder
{
    private readonly List<KeyValuePair<string, string?>> _map = new();

    public QueryMapBuilder()
    {
    }

    public QueryMapBuilder Add(string key, string? value = null)
    {
        _map.Add(new KeyValuePair<string, string?>(key, value));
        return this;
    }

    public IEnumerable<KeyValuePair<string, string?>> QueryMap => _map;
}
