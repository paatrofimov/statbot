using StatBot.UserIO;

namespace StatBot.Service.Core.Models
{
    public class RequestProcessorProcessArgs<TRequestParams>
    {
        public TRequestParams RequestParams { get; set; }
        public IUserMessagePrinter UserMessagePrinter { get; set; }
    }
}