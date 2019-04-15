namespace StatBot.Settings
{
    public class ApplicationSettings
    {
        public string VkLogin { get; set; }
        public string VkPassword { get; set; }
        public string VkAccessToken { get; set; }
        public long VkGroupOwnerIdForWallPost { get; set; }
        public ulong VkStandaloneAppId { get; set; }
        public ulong VkWallGetPostsCountParameter { get; set; }
    }
}