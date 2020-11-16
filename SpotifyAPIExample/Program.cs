using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SpotifyAPIExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var webClientOption = GetDataFromConfiguration();

            var endpoint = webClientOption.BaseUri + "v1/albums/6stcO4iWKmEGDlDguT9ZRu/tracks";
            
            var webClient = new WebClient(webClientOption.AccessToken, endpoint);

            var request = webClient.CreateRequest();

            var response = webClient.GetResponse(request);

            Console.WriteLine(response);
        }

        private static WebClientOption GetDataFromConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, false)
                .Build();

            var webClientOption = configuration.GetSection(nameof(WebClientOption)).Get<WebClientOption>();

            return webClientOption;
        }
    }
    
}