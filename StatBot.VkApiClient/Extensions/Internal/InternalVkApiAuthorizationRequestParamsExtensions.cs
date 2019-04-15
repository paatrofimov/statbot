using StatBot.VkApiClient.Models.Internal;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace StatBot.VkApiClient.Extensions.Internal
{
    public static class InternalVkApiAuthorizationRequestParamsExtensions
    {
        public static ApiAuthParams ToExternal(this InternalVkApiAuthorizationRequestParams requestParams)
        {
            return new ApiAuthParams
                   {
                       ApplicationId = requestParams.ApplicationId,
                       Login = requestParams.Login,
                       Password = requestParams.Password,
                       AccessToken = requestParams.AccessToken,
                       Settings = Settings.All | Settings.Offline
                   };
        }
    }
}