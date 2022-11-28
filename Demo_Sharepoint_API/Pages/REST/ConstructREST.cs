using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace Demo_Sharepoint_API.Pages.REST
{
    public class ConstructREST
    {

        public String getAccessToken(String client_id, String DomainSite, String clientSecret)
        {
            String urlAccessToken = "https://accounts.accesscontrol.windows.net/";
            var restClient = new RestClient(urlAccessToken);
            var requestBody = new JObject();
            var parameters = new JObject();
            //get client string 
            String[] tokens = { };
            if (client_id != null)
            {

                tokens = Regex.Split(client_id, "@");
            }

            String clientID = tokens[0];

            requestBody.Add("grant_type", "client_credentials");
            requestBody.Add("resource", "00000003-0000-0ff1-ce00-000000000000/" + DomainSite + "@" + clientID);
            requestBody.Add("client_id", client_id);
            requestBody.Add("client_secret", clientSecret);

            parameters.Add("grant_type", "client_credentials");
            Console.WriteLine(requestBody.ToString());
            var request = new RestRequest(clientID + "/tokens/OAuth/2/", Method.Get);


            //request.AddStringBody(requestBody.ToString(), DataFormat.Json);
            request.RequestFormat = DataFormat.Json;
            var body = new Posts { grant_type = "client_credentials", resource = "00000003-0000-0ff1-ce00-000000000000/" + DomainSite + "@" + clientID, client_id = client_id, client_secret = clientSecret };
            request.AddJsonBody(body);

            var bodysent = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            Console.WriteLine(bodysent);

            //make request and get response 
            //RestResponse response = restClient.Execute(request);
            //var content = response.Content;

            var response = restClient.Execute(request);

            //print result 
            Console.WriteLine(response.Content.ToString());
            Console.ReadKey(true);

            return "done";

        }
        public String getAccessTokenWithHTTP(String client_id, String DomainSite, String clientSecret)
        {
            using (var client = new HttpClient())
            {
                //get client string 
                var requestBody = new JObject();
                var parameters = new JObject();
                //get client string 
                String[] tokens = { };
                if (client_id != null)
                {

                    tokens = Regex.Split(client_id, "@");
                }

                String clientID = tokens[0];

                requestBody.Add("grant_type", "client_credentials");
                requestBody.Add("resource", "00000003-0000-0ff1-ce00-000000000000/" + DomainSite + "@" + clientID);
                requestBody.Add("client_id", client_id);
                requestBody.Add("client_secret", clientSecret);

                var endpoint = new Uri("https://accounts.accesscontrol.windows.net/" + client_id + "/tokens/OAuth/2/");
                var newPost = new Posts()
                {
                    grant_type = "client_credentials",
                    resource = "00000003-0000-0ff1-ce00-000000000000/" + DomainSite + "@" + clientID,
                    client_id = client_id,
                    client_secret = clientSecret
                };
                var newPostJson = JsonConvert.SerializeObject(newPost);

                //var res = client.PostAsync(endpoint, requestBody).Result.Content.ReadAsStringAsync().Result;
                //Console.WriteLine(res);
            }
            return "done";
        }
        public void testRESTSharp()
        {
        }
        public void sendingRequest(RestRequest request, RestClient restClient)
        {

            try
            {
                var res = restClient.GetAsync(request).Result;
                Debug.WriteLine(res);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
        public String getItems(String accessToken, String DomainSite)
        {
            String url = DomainSite + "_api/Web/Lists/getbytitle('Beta_Folder')/Items";
            using (RestClient restClient = new RestClient(url))
            {
                var headers = new JObject();
                headers.Add("Accept", "application/json;odata=verbose");
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
