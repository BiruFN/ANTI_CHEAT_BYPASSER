using Newtonsoft.Json;
using RestSharp;

namespace ANTI_CHEAT_BYPASSER
{
    public class Auth
    {
        public static string GetToken(string code)
        {
            var client = new RestClient("https://account-public-service-prod03.ol.epicgames.com");
            var request = new RestRequest("/account/api/oauth/token", Method.Post);
            request.AddHeader("Authorization", "Basic ZWM2ODRiOGM2ODdmNDc5ZmFkZWEzY2IyYWQ4M2Y1YzY6ZTFmMzFjMjExZjI4NDEzMTg2MjYyZDM3YTEzZmM4NGQ=");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", code);
            var response = client.Execute(request).Content;
            var token = JsonConvert.DeserializeObject<Token>(response);
            return token.access_token;
        }
        public static string GetExchange(string token)
        {
            var client = new RestClient("https://account-public-service-prod03.ol.epicgames.com");
            var request = new RestRequest("/account/api/oauth/exchange", Method.Get);
            request.AddHeader("Authorization", $"Bearer {token}");
            var response = client.Execute(request).Content;
            var exchange = JsonConvert.DeserializeObject<Exchange>(response);
            return exchange.code;
        }
    }
    public class Token
    {
        public string access_token { get; set; }
    }
    public class Exchange
    {
        public string code { get; set; }
    }
}
