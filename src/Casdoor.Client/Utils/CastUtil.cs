using Newtonsoft.Json;

namespace Casdoor.Client.Utils
{
    public class CastUtil
    {
        public static T classify<T>(Dictionary<string, object> dict, T defineClass)
            where T : class
        {
            if (dict is null) throw new ArgumentNullException(nameof(dict));
            if (defineClass is null) throw new ArgumentNullException(nameof(defineClass));
            try
            {
                string jsonPassStr = JsonConvert.SerializeObject(dict);
                return JsonConvert.DeserializeObject<T>(jsonPassStr);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
