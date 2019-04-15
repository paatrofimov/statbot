using System.Linq;
using StatBot.VkApiClient.Extensions.External;
using StatBot.VkApiClient.Extensions.Internal;
using StatBot.VkApiClient.Logging;
using StatBot.VkApiClient.Models.Internal;
using VkNet;

namespace StatBot.VkApiClient
{
    public class VkClient : IVkClient
    {
        private readonly VkApi api;

        public VkClient()
        {
            api = new VkApi(new Log4NetVkApiLoggerAdapter());
        }

        public InternalVkApiAuthorizationResponse Authorize(InternalVkApiAuthorizationRequestParams requestParams)
        {
            var externalParams = requestParams.ToExternal();

            api.Authorize(externalParams);

            return new InternalVkApiAuthorizationResponse {Token = api.Token};
        }

        public InternalVkApiObject ResolveScreenName(InternalVkApiResolveScreenNameRequestParams requestParams)
        {
            var response = api.Utils.ResolveScreenName(requestParams.ScreenName);

            return response.ToInternal();
        }

        public InternalVkApiWallGetObject WallGet(InternalVkApiWallGetRequestParams requestParams)
        {
            var externalParams = requestParams.ToExternal();

            var response = api.Wall.Get(externalParams);

            return response.ToInternal();
        }

        public InternalVkApiWallPostObject WallPost(InternalVkApiWallPostRequestParams requestParams)
        {
            var externalParams = requestParams.ToExternal();

            var response = api.Wall.Post(externalParams);

            return new InternalVkApiWallPostObject {PostId = response};
        }

        public InternalVkApiUser[] GetUsers(InternalVkApiUserGetRequestParams requestParams)
        {
            var response = api.Users.Get(requestParams.Ids);

            return response.Select(u => u.ToInternal()).ToArray();
        }

        public InternalVkApiGroup GetGroupById(InternalVkApiGetGroupByIdRequestParams requestParams)
        {
            var response = api.Groups.GetById(new[] {requestParams.GroupId}, requestParams.GroupId, null);

            return response.Select(u => u.ToInternal()).ToArray().SingleOrDefault();
        }
    }
}