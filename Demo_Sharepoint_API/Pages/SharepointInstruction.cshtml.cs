using Microsoft.AspNetCore.Mvc.RazorPages;
using Demo_Sharepoint_API.Pages.REST;
using System.Diagnostics;

namespace Demo_Sharepoint_API.Pages
{
    public class SharepointInstructionModel : PageModel
    {
        public GetAccessToken tenantInfo = new GetAccessToken();
        public void getAccTok(FormCollection values)
        {
            tenantInfo.clientID = values["clientID"];
            tenantInfo.clientSecret = values["clientSecret"];
            tenantInfo.DomainSite = values["DomainSite"];
            Debug.WriteLine("get acc tok run");
            ConstructREST restClient = new ConstructREST();
            restClient.getAccessToken(tenantInfo.clientID, tenantInfo.DomainSite, tenantInfo.clientSecret);
        }
    }
    public class GetAccessToken
    {
        public string clientID = "";
        public string clientSecret = " ";
        public string DomainSite = "";
    }
}
