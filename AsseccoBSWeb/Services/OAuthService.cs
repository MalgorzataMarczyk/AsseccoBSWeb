using AsseccoBSWeb.Models;
using AsseccoBSWeb.Services.Abstraction;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace AsseccoBSWeb.Services
{
    public class OAuthService : IOAuthService
    {
        public Token GetAccessToken(string username, string password)
        {
            var client = new RestClient("https://oauth2.assecobs.pl/api/oauth2/token")
            {
                Authenticator = new HttpBasicAuthenticator(username, password),
            };

            var request = new RestRequest();

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", "USERAPI");

            var response = client.Post(request);

            Console.WriteLine(response.Content);

            return JsonConvert.DeserializeObject<Token>(response.Content);
        }
    }
}
