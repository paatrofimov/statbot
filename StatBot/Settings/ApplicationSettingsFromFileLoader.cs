using System;
using System.IO;

namespace StatBot.Settings
{
    public class ApplicationSettingsFromFileLoader : IApplicationSettingsLoader
    {
        public ApplicationSettings Load()
        {
            var settingsFilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "statBotSettings");

            if (File.Exists(settingsFilepath))
            {
                var keyValueSettings = KeyValueSettingsLoader.LoadFromFile(settingsFilepath);

                return new ApplicationSettings
                       {
                           VkLogin = keyValueSettings["VkLogin"],
                           VkPassword = keyValueSettings["VkPassword"],
                           VkAccessToken = keyValueSettings["VkAccessToken"],
                           VkGroupOwnerIdForWallPost = long.Parse(keyValueSettings["VkGroupOwnerIdForWallPost"]),
                           VkStandaloneAppId = ulong.Parse(keyValueSettings["VkStandaloneAppId"]),
                           VkWallGetPostsCountParameter = ulong.Parse(keyValueSettings["VkWallGetPostsCountParameter"])
                       };
            }

            throw new Exception($"Expected file to exist: {settingsFilepath}");
        }
    }
}