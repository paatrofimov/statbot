namespace StatBot.VkApiClient.Models.Internal
{
    public class InternalVkApiAuthorizationRequestParams
    {
        public ulong ApplicationId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string AccessToken { get; set; }
    }
}