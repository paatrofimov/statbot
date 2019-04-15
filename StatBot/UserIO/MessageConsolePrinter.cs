using System;

namespace StatBot.UserIO
{
    public class MessageConsolePrinter : IMessagePrinter
    {
        private readonly string prefix;

        public MessageConsolePrinter(string prefix)
        {
            this.prefix = prefix;
        }

        public void Print(string message)
        {
            Console.WriteLine($"[{prefix}] {message}");
        }
    }
}