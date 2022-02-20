namespace Casdoor.Client.Config
{
    /// <summary>
    ///  Casdoor 配置类
    /// </summary>
    [Serializable]
    public class CasdoorConfig
    {

        public CasdoorConfig(string _endPoint, string _clientId, string _clientSecret, string _jwtPublicKey, string _orginazationName)
        {
            endPoint = _endPoint
                ?? throw new ArgumentNullException(nameof(endPoint));
            clientId = _clientId
                ?? throw new ArgumentNullException(nameof(clientId));
            clientSecret = _clientSecret
                ?? throw new ArgumentNullException(nameof(clientSecret));
            jwtPublicKey = _jwtPublicKey
                ?? throw new ArgumentNullException(nameof(jwtPublicKey));
            orginazationName = _orginazationName
                ?? throw new ArgumentNullException(nameof(orginazationName));
        }

        public string endPoint { get; set; } = string.Empty;

        public string clientId { get; set; } = string.Empty;

        public string clientSecret { get; set; } = string.Empty;

        public string jwtPublicKey { get; set; } = string.Empty;

        public string orginazationName { get; set; } = string.Empty;

        public string applicationName { get; set; } = string.Empty;

    }

}
