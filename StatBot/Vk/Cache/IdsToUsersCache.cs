using StatBot.Helpers;
using StatBot.VkApiClient;
using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Cache
{
    public class IdsToUsersCache : Cache<long, InternalVkApiUser>
    {
        private readonly IVkClient vkClient;

        public IdsToUsersCache(IVkClient vkClient)
        {
            this.vkClient = vkClient;
        }

        protected override InternalVkApiUser GetValue(long id)
        {
            return vkClient.GetUserOrDie(id);
        }
    }
}