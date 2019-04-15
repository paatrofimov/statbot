using StatBot.Helpers;
using StatBot.VkApiClient;
using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Cache
{
    public class IdsToGroupsCache : Cache<long, InternalVkApiGroup>
    {
        private readonly IVkClient vkClient;

        public IdsToGroupsCache(IVkClient vkClient)
        {
            this.vkClient = vkClient;
        }

        protected override InternalVkApiGroup GetValue(long key)
        {
            return vkClient.GetGroupById(new InternalVkApiGetGroupByIdRequestParams
                                         {
                                             GroupId = key.ToString()
                                         });
        }
    }
}