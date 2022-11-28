using Demo_Sharepoint_API.Pages.REST;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Demo_Sharepoint_API.Pages
{
    public class SharepointInstructionModel : PageModel
    {
        public GetAccessToken tenantInfo = new GetAccessToken();
        public void OnPost()
        {
            tenantInfo.clientID = Request.Form["clientID"];
            tenantInfo.clientSecret = Request.Form["clientSecret"];
            tenantInfo.DomainSite = Request.Form["DomainSite"];
            Debug.WriteLine("get acc tok run");
            ConstructREST restClient = new ConstructREST();

            restClient.getAccessToken(tenantInfo.clientID, tenantInfo.DomainSite, tenantInfo.clientSecret);
            //restClient.getAccessTokenWithHTTP(tenantInfo.clientID, tenantInfo.DomainSite, tenantInfo.clientSecret);
            //return RedirectToPage("Index");
        }
    }
    public class GetAccessToken
    {
        public string clientID = "";
        public string clientSecret = " ";
        public string DomainSite = "";
    }
}
