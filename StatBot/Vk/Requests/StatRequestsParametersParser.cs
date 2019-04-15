using StatBot.RequestParametersParser;
using StatBot.Vk.Models;

namespace StatBot.Vk.Requests
{
    public class StatRequestsParametersParser : IRequestsParametersParser<StatRequestParameters>
    {
        public StatRequestParameters ParseParametersOrNull(string rawInput)
        {
            return new StatRequestParameters {ScreenName = rawInput};
        }
    }
}