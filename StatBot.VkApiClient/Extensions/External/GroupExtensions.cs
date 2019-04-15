using StatBot.VkApiClient.Models.Internal;
using VkNet.Model;

namespace StatBot.VkApiClient.Extensions.External
{
    public static class GroupExtensions
    {
        public static InternalVkApiGroup ToInternal(this Group group)
        {
            return new InternalVkApiGroup
                   {
                       Id = group.Id,
                       Name = group.Name
                   };
        }
    }
}