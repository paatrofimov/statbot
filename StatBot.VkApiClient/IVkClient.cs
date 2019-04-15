using StatBot.VkApiClient.Models.Internal;

namespace StatBot.VkApiClient
{
    public interface IVkClient
    {
        InternalVkApiAuthorizationResponse Authorize(InternalVkApiAuthorizationRequestParams requestParams);
        InternalVkApiObject ResolveScreenName(InternalVkApiResolveScreenNameRequestParams requestParams);
        InternalVkApiWallGetObject WallGet(InternalVkApiWallGetRequestParams requestParams);
        InternalVkApiWallPostObject WallPost(InternalVkApiWallPostRequestParams requestParams);
        InternalVkApiUser[] GetUsers(InternalVkApiUserGetRequestParams requestParams);
        InternalVkApiGroup GetGroupById(InternalVkApiGetGroupByIdRequestParams requestParams);
    }
}