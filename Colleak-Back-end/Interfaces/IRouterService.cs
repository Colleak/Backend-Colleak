using Colleak_Back_end.Models;

namespace Colleak_Back_end.Interfaces
{
    public interface IRouterService
    {
        Task<List<DeviceInfo>> GetAllRouterInfo();
    }
}
