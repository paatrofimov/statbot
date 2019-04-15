using StatBot.UserIO;

namespace StatBot.Service.Core.Models
{
    public class StartNewRequestArgs<TRequestParams>
    {
        public string TraceId { get; set; }
        public TRequestParams RequestParams { get; set; }
        public IMessagePrinter UserMessagePrinter { get; set; }
    }
}