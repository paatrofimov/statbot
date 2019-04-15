using StatBot.Settings;
using StatBot.VkApiClient;
using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Authentication
{
    public class VkAuthenticatorFromSettings : IVkAuthenticator
    {
        private readonly ApplicationSettings appSettings;
        private readonly IVkClient vkClient;

        public VkAuthenticatorFromSettings(ApplicationSettings appSettings,
                                           IVkClient vkClient)
        {
            this.appSettings = appSettings;
            this.vkClient = vkClient;
        }

        public void Authenticate()
        {
            vkClient.Authorize(new InternalVkApiAuthorizationRequestParams
                               {
                                   ApplicationId = appSettings.VkStandaloneAppId,
                                   Login = appSettings.VkLogin,
                                   Password = appSettings.VkPassword,
                                   AccessToken = appSettings.VkAccessToken
                               });
        }
    }
}