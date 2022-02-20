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

        private string endPoint { get; set; }

        private string clientId { get; set; }

        private string clientSecret { get; set; }

        private string jwtPublicKey { get; set; }

        private string orginazationName { get; set; }

    }

}
