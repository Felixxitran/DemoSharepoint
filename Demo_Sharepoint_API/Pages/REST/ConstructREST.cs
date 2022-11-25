using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Demo_Sharepoint_API.Pages.REST
{
    public class ConstructREST
    {
        static void Main(String[] args)
        {
            var client = new RestClient();

        }
        public String getAccessToken(String client_id, String DomainSite, String clientSecret)
        {
            String urlAccessToken = "https://accounts.accesscontrol.windows.net/" + client_id + "/tokens/OAuth/2/";
            using (RestClient restClient = new RestClient(urlAccessToken))
            {
                var requestBody = new JObject();
                var parameters = new JObject();

                String[] tokens = Regex.Split(client_id, "@");
                String clientID = tokens[0];

                requestBody.Add("grant_type", "client_credentials");
                requestBody.Add("resource", "00000003-0000-0ff1-ce00-000000000000/" + DomainSite + clientID);
                requestBody.Add("client_id", client_id);
                requestBody.Add("client_secret", clientSecret);

                parameters.Add("grant_type", "client_credentials");
                var request = new RestRequest();
                request.AddStringBody(requestBody.ToString(), DataFormat.Json);
                request.AddParameter(parameters.ToString(), DataFormat.Json);
                var res = restClient.GetAsync(request).Result;
                Debug.WriteLine(res);
                Console.WriteLine(res.ToString());
                Debug.WriteLine("done accessing token");
                return "done";
            }

        }
        public String getItems(String accessToken, String DomainSite)
        {
            String url = DomainSite + "_api/Web/Lists/getbytitle('Beta_Folder')/Items";
            using (RestClient restClient = new RestClient(url))
            {
                var headers = new JObject();
                headers.Add("Accept", "?Accept=application/json;odata=verbose");
                headers.Add("content-type", "application/json;odata=verbose");
                headers.Add("Authorization", "Bearer " + accessToken);
                var request = new RestRequest();
                request.AddHeader(headers.ToString(), DataFormat.Json);
                var res = restClient.GetAsync(request).Result;
                return "done";
            }
        }

    }
}
