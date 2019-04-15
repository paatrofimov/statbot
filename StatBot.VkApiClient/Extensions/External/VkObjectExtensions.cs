using System;
using StatBot.VkApiClient.Models.Internal;
using VkNet.Enums;
using VkNet.Model;

namespace StatBot.VkApiClient.Extensions.External
{
    public static class VkObjectExtensions
    {
        public static InternalVkApiObject ToInternal(this VkObject vkObject)
        {
            return vkObject == null
                       ? null
                       : new InternalVkApiObject
                         {
                             Id = vkObject.Id,
                             Type = ToInternal(vkObject.Type)
                         };
        }

        private static InternalVkObjectType ToInternal(VkObjectType vkObjectType)
        {
            switch (vkObjectType)
            {
                case VkObjectType.User:
                {
                    return InternalVkObjectType.User;
                }
                case VkObjectType.Group:
                {
                    return InternalVkObjectType.Group;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}