using Microsoft.AspNetCore.Mvc.RazorPages;

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

        }
    }
    public class GetAccessToken
    {
        public string clientID = "";
        public string clientSecret = " ";
        public string DomainSite = "";
    }
}
