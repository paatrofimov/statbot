using System.Threading.Tasks;

namespace StatBot.Service.Core
{
    public interface IExecutingRequestsCollection<TRequestParams> : IExecutingRequestsCollection
    {
    }

    public interface IExecutingRequestsCollection
    {
        void TryStartExecuteNew(string rawInput);
        bool IsNotEmpty();
        Task WaitEmptyAsync();
    }
}