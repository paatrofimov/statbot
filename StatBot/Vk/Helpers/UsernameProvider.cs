using StatBot.Vk.Cache;
using StatBot.VkApiClient.Extensions.Internal;
using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Helpers
{
    public class UsernameProvider : IUsernameProvider
    {
        private readonly IdsToGroupsCache idsToGroupsCache;
        private readonly IdsToUsersCache idsToUsersCache;

        public UsernameProvider(IdsToUsersCache idsToUsersCache,
                                IdsToGroupsCache idsToGroupsCache)
        {
            this.idsToUsersCache = idsToUsersCache;
            this.idsToGroupsCache = idsToGroupsCache;
        }

        public string GetUsernameForObject(InternalVkApiObject vkApiObject)
        {
            var id = vkApiObject.Id.Value;

            if (vkApiObject.Type == InternalVkObjectType.Group)
            {
                return idsToGroupsCache.GetOrAdd(id).Name;
            }

            return idsToUsersCache.GetOrAdd(id).ToUsername();
        }
    }
}