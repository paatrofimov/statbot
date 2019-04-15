using StatBot.VkApiClient.Models.Internal;
using VkNet.Model;

namespace StatBot.VkApiClient.Extensions.External
{
    public static class UserExtensions
    {
        public static InternalVkApiUser ToInternal(this User user)
        {
            return new InternalVkApiUser
                   {
                       Id = user.Id,
                       FirstName = user.FirstName,
                       LastName = user.LastName
                   };
        }
    }
}