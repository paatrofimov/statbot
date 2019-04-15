using System;
using StatBot.RequestParametersParser;

namespace StatBot.Cuckoo
{
    public class CuckooRequestsParametersParser : IRequestsParametersParser<CuckooRequestParameters>
    {
        public CuckooRequestParameters ParseParametersOrNull(string rawInput)
        {
            if (rawInput.Equals("say cuckoo", StringComparison.CurrentCultureIgnoreCase))
            {
                return new CuckooRequestParameters()
                       {
                           NeedSayCuckoo = true,
                       };
            }

            return null;
        }
    }
}