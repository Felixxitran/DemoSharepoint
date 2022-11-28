namespace Demo_Sharepoint_API.Pages.REST
{
    public class AccessToken
    {
        public string token_type { get; set; }
        public string access_token { get; set; }

        public string expires_in { get; set; }

        public string not_before { get; set; }

        public string expire_on { get; set; }

        public string resource { get; set; }
    }
}
