using System.Threading.Tasks;
using StatBot.Service.Core;
using StatBot.Service.Core.Models;

namespace StatBot.Cuckoo
{
    public class CuckooServiceRequestExecutor : IRequestExecutor<CuckooRequestParameters>
    {
        public void Execute(RequestProcessorProcessArgs<CuckooRequestParameters> args)
        {
            Task.Delay(1000).GetAwaiter().GetResult();

            args.UserMessagePrinter.UnderlyingPrinter.Print("Cuckoo!");
        }
    }
}