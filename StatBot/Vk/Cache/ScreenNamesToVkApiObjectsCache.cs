using StatBot.Helpers;
using StatBot.VkApiClient;
using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Cache
{
    public class ScreenNamesToVkApiObjectsCache : Cache<string, InternalVkApiObject>
    {
        private readonly IVkClient vkClient;

        public ScreenNamesToVkApiObjectsCache(IVkClient vkClient)
        {
            this.vkClient = vkClient;
        }

        protected override InternalVkApiObject GetValue(string screenName)
        {
            var vkApiObject = vkClient.ResolveScreenName(new InternalVkApiResolveScreenNameRequestParams
                                                         {
                                                             ScreenName = screenName
                                                         });

            return vkApiObject;
        }
    }
}