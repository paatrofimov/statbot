using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Models
{
    public class StatsBuilderBuildArgs
    {
        public string Username { get; set; }
        public InternalVkApiWallGetObject WallGetObject { get; set; }
    }
}