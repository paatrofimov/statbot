using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StatBot.Settings
{
    public class KeyValueSettingsLoader
    {
        public static IDictionary<string, string> LoadFromFile(string settingsFilepath)
        {
            return File.ReadAllLines(settingsFilepath)
                       .Select(line => line.Split('=').Select(keyOrValue => keyOrValue.Trim()).ToArray())
                       .Where(keyValue => keyValue.Length == 2)
                       .ToDictionary(keyValue => keyValue.First(), keyValue => keyValue.Last());
        }
    }
}