using System.Linq;
using StatBot.VkApiClient.Models.Internal;

namespace StatBot.VkApiClient
{
    public static class VkClientExtensions
    {
        public static InternalVkApiAuthorizationResponse AuthorizeByLoginPassword(this IVkClient client,
                                                                                  string login,
                                                                                  string password,
                                                                                  ulong appId)
        {
            return client.Authorize(new InternalVkApiAuthorizationRequestParams
                                    {
                                        ApplicationId = appId,
                                        Login = login,
                                        Password = password
                                    });
        }

        public static InternalVkApiAuthorizationResponse AuthorizeByAccessToken(this IVkClient client,
                                                                                string accessToken,
                                                                                ulong appId)
        {
            return client.Authorize(new InternalVkApiAuthorizationRequestParams
                                    {
                                        ApplicationId = appId,
                                        AccessToken = accessToken
                                    });
        }

        public static InternalVkApiUser GetUserOrDie(this IVkClient client, long id)
        {
            return client.GetUsers(new InternalVkApiUserGetRequestParams {Ids = new[] {id}}).Single();
        }
    }
}