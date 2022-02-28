using System.Net;

namespace Casdoor.Client.Utils.http
{
    public class HttpReqBox
    {
        private static HttpClient? _httpClient;

        public static string syncGet(string uri)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException("Uri parameter transferd by null value!");
            }
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            using (_httpClient = new HttpClient())
            {
                HttpResponseMessage messageGet = _httpClient.SendAsync(request).Result;
                int statusCodeGet = (int)messageGet.StatusCode;
                if (statusCodeGet == 200)
                {
                    return messageGet.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new WebException($"return code shows:{statusCodeGet}");
                }
            }
        }

        public static string postString(string uri, string objStr)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentNullException(nameof(uri));
            }
            if (string.IsNullOrEmpty(objStr))
            {
                throw new ArgumentNullException(nameof(objStr));
            }

            using (_httpClient = new HttpClient())
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, uri);
                    HttpContent content = new StringContent(objStr);
                    content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    HttpResponseMessage messagePost = _httpClient.SendAsync(request).Result;
                    messagePost.EnsureSuccessStatusCode();
                    return messagePost.Content.ReadAsStringAsync().Result;
                }
                catch (Exception exp)
                {
                    throw;
                }
            }
        }

        private static string postFile(string uri, string fileInputAddr)
        {
            if (string.IsNullOrEmpty(uri))
            {
                throw new ArgumentException(nameof(uri));
            }
            if (string.IsNullOrEmpty(fileInputAddr))
            {
                throw new ArgumentException(nameof(fileInputAddr));
            }
            using (_httpClient = new HttpClient())
            {
                try
                {
                    string fileName = fileInputAddr.Substring(fileInputAddr.LastIndexOf('/') + 1);
                    byte[] bytesIn = File.ReadAllBytes(fileInputAddr);
                    MultipartFormDataContent formdata = new MultipartFormDataContent();
                    formdata.Add(new ByteArrayContent(bytesIn, 0, bytesIn.Length), fileName, fileName);
                    HttpResponseMessage messageFilePost = _httpClient.PostAsync(uri, formdata).Result;
                    messageFilePost.EnsureSuccessStatusCode();
                    return messageFilePost.Content.ReadAsStringAsync().Result;
                }
                catch (Exception exp)
                {
                    throw;
                }
            }
        }

    }
}
