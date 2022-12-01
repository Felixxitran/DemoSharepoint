using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
namespace Demo_Sharepoint_API.Pages.REST
{
    public class HTTPRest
    {
        public string getAccessToken(String client_id, String DomainSite, String clientSecret)
        {


            String[] tokens = { };
            if (client_id != null)
            {

                tokens = Regex.Split(client_id, "@");
            }
            String clientID = tokens[1];
            String urlAccessToken = "https://accounts.accesscontrol.windows.net/" + clientID + "/tokens/OAuth/2/";
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("resource", "00000003-0000-0ff1-ce00-000000000000/" + DomainSite + "@" + clientID),
            new KeyValuePair<string, string>("client_id", client_id),
            new KeyValuePair<string, string>("client_secret", clientSecret),
            };

            HttpContent content = new FormUrlEncodedContent(keyValues);
            var httpClient = new HttpClient();
            var response = httpClient.PostAsync(urlAccessToken, content).Result;
            var token = response.Content.ReadAsStringAsync().Result;
            string accessToken = (JsonConvert.DeserializeObject<AccessToken>(token)).access_token;

            Console.WriteLine(accessToken);
            return accessToken;
        }
        public void createNewFolder(string accessToken, string DomainSite, string documentLibrary)
        {
            String url = "https://" + DomainSite + "/sites/" + "Beta_ASP.NET" + "/_api/web/folders";
            var httpClient = new HttpClient();
            var body = new
            {
                __metadata = new
                {
                    type = "SP.Folder",
                },
                ServerRelativeUrl = documentLibrary + "/CargoList"
            };
            var header = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Content-Type", "application/json;odata=verbose"),
                new KeyValuePair<string, string>("Accept", "application/json;odata=verbose"),
                new KeyValuePair<string, string>("Authorization", "Bearer "+accessToken),
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);




            string jsonBody = JsonConvert.SerializeObject(body);
            HttpContent content = new StringContent(jsonBody);
            MediaTypeHeaderValue sharePointJsonMediaType = null;
            MediaTypeHeaderValue.TryParse("application/json;odata=verbose;charset=utf-8", out sharePointJsonMediaType);
            content.Headers.ContentType = sharePointJsonMediaType;
            Console.WriteLine(url);
            var response = httpClient.PostAsync(url, content).Result;
            Console.WriteLine(response);
        }
        public void sendFiles()
        {

        }
    }
}

