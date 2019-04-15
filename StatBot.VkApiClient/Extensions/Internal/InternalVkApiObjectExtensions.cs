using StatBot.VkApiClient.Models.Internal;

namespace StatBot.VkApiClient.Extensions.Internal
{
    public static class InternalVkApiObjectExtensions
    {
        public static long? BuildOwnerId(InternalVkApiObject internalObject)
        {
            return internalObject.Type == InternalVkObjectType.Group
                       ? -internalObject.Id
                       : internalObject.Id;
        }
    }
}