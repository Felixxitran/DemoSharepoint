using Demo_Sharepoint_API.Pages.REST;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Demo_Sharepoint_API.Pages
{
    public class SharepointInstructionModel : PageModel
    {
        public GetAccessToken tenantInfo = new GetAccessToken();
        public ActionResult OnPost()
        {
            tenantInfo.clientID = Request.Form["clientID"];
            tenantInfo.clientSecret = Request.Form["clientSecret"];
            tenantInfo.DomainSite = Request.Form["DomainSite"];
            tenantInfo.FolderName = Request.Form["FolderName"];
            Debug.WriteLine("get acc tok run");
            ConstructREST restClient = new ConstructREST();
            HTTPRest restHTTPClient = new HTTPRest();
            //restClient.getAccessToken(tenantInfo.clientID, tenantInfo.DomainSite, tenantInfo.clientSecret);
            string accessToken = restHTTPClient.getAccessToken(tenantInfo.clientID, tenantInfo.DomainSite, tenantInfo.clientSecret);
            //restClient.getAccessTokenWithHTTP(tenantInfo.clientID, tenantInfo.DomainSite, tenantInfo.clientSecret);
            //return RedirectToPage("Index");
            restHTTPClient.createNewFolder(accessToken, tenantInfo.DomainSite, tenantInfo.FolderName);
            return Redirect("Success");
        }
    }
    public class GetAccessToken
    {
        public string clientID = "";
        public string clientSecret = " ";
        public string DomainSite = "";
        public string FolderName = "";
    }
}
