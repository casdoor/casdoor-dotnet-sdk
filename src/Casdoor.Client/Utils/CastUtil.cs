using System.Text.Json;

namespace Casdoor.Client.Utils
{
    public class CastUtil
    {
        private readonly static JsonSerializerOptions _serialOps = new JsonSerializerOptions { WriteIndented = true };

        public static T classify<T>(Dictionary<string, object> dict, T defineClass)
            where T : class
        {
            if (dict is null) throw new ArgumentNullException(nameof(dict));
            if (defineClass is null) throw new ArgumentNullException(nameof(defineClass));
            try
            {
                string jsonPassStr = JsonSerializer.Serialize(dict, options: _serialOps);
                return JsonSerializer.Deserialize<T>(jsonPassStr);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
