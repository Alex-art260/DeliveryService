using DeliveryService.Data;
using DeliveryService.Models;

namespace DeliveryService.Interfaces
{
    public interface IOrderService
    {
        void GenerateFile(DateTime startDate, int districtId, string path, string pathToLog);
    }
}
