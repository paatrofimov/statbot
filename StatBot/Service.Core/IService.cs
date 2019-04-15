using System.Threading.Tasks;

namespace StatBot.Service.Core
{
    public interface IService
    {
        Task StartAndWaitAsync();
    }
}