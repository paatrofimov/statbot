using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using StatBot.Vk.Models;

namespace StatBot.Vk.Requests
{
    public class FrequencyStatsBuilder : IStatsBuilder<StatsBuilderBuildArgs>
    {
        public string Build(StatsBuilderBuildArgs buildArgs)
        {
            var postsLetters = buildArgs.WallGetObject.WallApiPosts.SelectMany(p => p.Text).Where(char.IsLetter)
                                        .ToArray();

            var frequencies = postsLetters.Aggregate(
                new Dictionary<char, double>(),
                (acc, letter) =>
                    IncreaseFrequency(acc, letter, postsLetters.Length)
            );

            var formattedFrequencies = frequencies.ToDictionary(kv => kv.Key, kv => kv.Value.ToString("F"));

            return
                $"{buildArgs.Username}, статистика для последних {buildArgs.WallGetObject.WallApiPosts.Length} постов: " +
                $"{JsonConvert.SerializeObject(formattedFrequencies).Replace("\"", string.Empty)}";
        }

        private Dictionary<char, double> IncreaseFrequency(Dictionary<char, double> acc, char letter,
                                                           int totalLettersCount)
        {
            var strLetter = char.Parse(letter.ToString().ToLower(CultureInfo.CurrentCulture));
            if (!acc.TryGetValue(strLetter, out var count))
            {
                count = 0;
            }

            acc[strLetter] = count + 1.0 / totalLettersCount;

            return acc;
        }
    }
}