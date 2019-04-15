using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Helpers
{
    public interface IUsernameProvider
    {
        string GetUsernameForObject(InternalVkApiObject vkApiObject);
    }
}