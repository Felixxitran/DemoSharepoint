using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

using System.Linq;
using System.Xml.Linq;

namespace Demo_Sharepoint_API.Pages.REST
{
    public class ConstructREST
    {
        
        public String getAccessToken(String client_id, String DomainSite, String clientSecret)
        {
            String urlAccessToken = "https://accounts.accesscontrol.windows.net/" ;
            using (RestClient restClient = new RestClient(urlAccessToken))
            {
                var requestBody = new JObject();
                var parameters = new JObject();
                //get client string 
                String[] tokens = { };
                if (client_id!=null) { 

                    tokens = Regex.Split(client_id, "@");
                }
                
                String clientID = tokens[0];

                requestBody.Add("grant_type", "client_credentials");
                requestBody.Add("resource", "00000003-0000-0ff1-ce00-000000000000/" + DomainSite +"@"+ clientID);
                requestBody.Add("client_id", client_id);
                requestBody.Add("client_secret", clientSecret);

                parameters.Add("grant_type", "client_credentials");
                Console.WriteLine(requestBody.ToString());
                var request = new RestRequest(client_id + "/tokens/OAuth/2/", Method.Get);
                
                
                //request.AddJsonBody(new
                //{
                //    grant_type = "client_credentials",
                //    client_id = client_id,
                //    client_secret = clientSecret,
                //    resource= "00000003-0000-0ff1-ce00-000000000000/" + DomainSite + "@" + clientID
                //}) ;

                JArray requestArray = new JArray();
                requestArray.Add(requestBody);
                request.AddParameter("application/json", requestArray, ParameterType.RequestBody);
                Console.WriteLine(request.Parameters);
                RestResponse response = restClient.Execute(request);
                var content = response.Content;
                Console.WriteLine(content);
                Console.ReadKey(true);
                return "done";
            }

        }
        public void sendingRequest (RestRequest request,RestClient restClient) {
            
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
