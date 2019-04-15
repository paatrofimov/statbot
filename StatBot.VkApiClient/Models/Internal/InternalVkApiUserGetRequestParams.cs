using System.Collections.Generic;

namespace StatBot.VkApiClient.Models.Internal
{
    public class InternalVkApiUserGetRequestParams
    {
        public IEnumerable<long> Ids { get; set; }
    }
}