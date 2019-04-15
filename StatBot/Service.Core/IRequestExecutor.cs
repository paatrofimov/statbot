using StatBot.Service.Core.Models;

namespace StatBot.Service.Core
{
    public interface IRequestExecutor<TRequestParams>
    {
        void Execute(RequestProcessorProcessArgs<TRequestParams> args);
    }
}