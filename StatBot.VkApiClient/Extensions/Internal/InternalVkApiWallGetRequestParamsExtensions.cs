using System;
using StatBot.VkApiClient.Models.Internal;
using VkNet.Model.RequestParams;

namespace StatBot.VkApiClient.Extensions.Internal
{
    public static class InternalVkApiWallGetRequestParamsExtensions
    {
        public static WallGetParams ToExternal(this InternalVkApiWallGetRequestParams requestParams)
        {
            return new WallGetParams
                   {
                       OwnerId = requestParams.OwnerId,
                       Count = Math.Min(requestParams.Count, 100)
                   };
        }
    }
}