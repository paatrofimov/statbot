using StatBot.VkApiClient.Models.Internal;

namespace StatBot.VkApiClient.Extensions.Internal
{
    public static class InternalVkApiUserExtensions
    {
        public static string ToUsername(this InternalVkApiUser user)
        {
            return $"{user.FirstName} {user.LastName}";
        }
    }
}