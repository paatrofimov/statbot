using System.Web;
using StatBot.VkApiClient.Models.Internal;
using VkNet.Model.RequestParams;

namespace StatBot.VkApiClient.Extensions.Internal
{
    public static class InternalVkApiWallPostRequestParamsExtensions
    {
        public static WallPostParams ToExternal(this InternalVkApiWallPostRequestParams requestParams)
        {
            return new WallPostParams
                   {
                       OwnerId = requestParams.OwnerId,
                       Message = HttpUtility.HtmlEncode(requestParams.Message)
                   };
        }
    }
}