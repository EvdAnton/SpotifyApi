using System.IO;
using System.Net;

namespace SpotifyAPIExample
{
    public class WebClient
    {
        private readonly string _accessToken;
        private readonly string _endEndpoint;

        public WebClient(string accessToken, string endEndpoint)
        {
            _accessToken = accessToken;
            _endEndpoint = endEndpoint;
        }

        public HttpWebRequest CreateRequest()
        {
            var request = (HttpWebRequest) WebRequest.Create(_endEndpoint);
            request.Method = WebRequestMethods.Http.Get;
            request.PreAuthenticate = true;
            request.Headers.Add("Accept","application/json");
            request.Headers.Add("Content-Type", "application/json");
            request.Headers.Add("Authorization",$"Bearer {_accessToken}");

            return request;
        }

        public string GetResponse(HttpWebRequest request)
        {
            using var response = (HttpWebResponse) request.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new InvalidDataException($"Incorrect request endpoint: {request.RequestUri}");
            }

            using var responseStream = response.GetResponseStream();

            if (responseStream == null)
            {
                return string.Empty;
            }
            
            using var reader = new StreamReader(responseStream);
            
            return reader.ReadToEnd();
        }

    }
}