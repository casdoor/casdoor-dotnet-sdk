using System.Collections;
using System.Text.Json;

namespace Casdoor.Client.Utils
{
    public class CastUtil
    {
        private readonly static JsonSerializerOptions _serialOps =
            new JsonSerializerOptions { WriteIndented = true };

        public static T classify<T, X>(X loopResource, T defineClass)
            where T : class
            where X : IEnumerable
        {
            if (loopResource is null) throw new ArgumentNullException(nameof(loopResource));
            if (defineClass is null) throw new ArgumentNullException(nameof(defineClass));
            try
            {
                var loop = (loopResource as List<KeyValuePair<string, object>>);
                Dictionary<string, object> properties = new(loop!.Count);
                loop.ForEach(kv => properties[kv.Key] = kv.Value);
                string jsonPassStr = JsonSerializer.Serialize(properties, options: _serialOps);
                return JsonSerializer.Deserialize<T>(jsonPassStr);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
