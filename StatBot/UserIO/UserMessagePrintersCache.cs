using System;
using StatBot.Helpers;

namespace StatBot.UserIO
{
    public class UserMessagePrintersCache : Cache<string, IUserMessagePrinter>
    {
        private readonly Func<string, IUserMessagePrinter> buildPrinter;

        public UserMessagePrintersCache(Func<string, IUserMessagePrinter> buildPrinter)
        {
            this.buildPrinter = buildPrinter;
        }

        protected override IUserMessagePrinter GetValue(string prefix)
        {
            return buildPrinter(prefix);
        }
    }
}