using DeliveryService.Data;
using DeliveryService.Interfaces;
using DeliveryService.Models;
using System.Data;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace DeliveryService.Services
{
    public class OrderService : IOrderService
    {
        private List<Order> GetOrders(string pathToLog)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "order.json");
                var jsonString = File.ReadAllText(filePath);

                var data = JsonSerializer.Deserialize<List<Order>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new DateTimeConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
                });

                if (data != null)
                    return data;
            }
            catch (Exception ex)
            {
                LogException(ex, "GetOrders", pathToLog);
            }
            return new List<Order>();

        }

        private List<IpDateTime> GetIp(DateTime startDate, int districtId, string pathToLog)
        {
            var endDate = startDate.AddMinutes(30);
            var orders = GetOrders(pathToLog);

            var ipDateTime = orders
                .Where(o => o.District == districtId)
                .Where(o => o.DeliveryTime >= startDate && o.DeliveryTime <= endDate).
                Select(o => new IpDateTime {IpAddress = o.IpAddress, DeliveryTime = o.DeliveryTime });


            return ipDateTime.ToList();
        }

        public void GenerateFile(DateTime startDate, int districtId, string path, string pathToLog)
        {
            try
            {
                var ipCounts = GetIp(startDate, districtId, pathToLog);

                string filePath = Path.Combine(path!, "ip.txt");
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (var ipCount in ipCounts)
                    {
                        writer.WriteLine($"{ipCount.IpAddress}: {ipCount.DeliveryTime}");
                    }
                }

                Console.WriteLine($"Результаты записаны в файл: {filePath}");
            }
            catch(Exception ex)
            {
                LogException(ex, "GenerateFile", pathToLog);
            }

        }

        private void LogException(Exception ex, string methodName, string pathToLog)
        {
            string logPath = Path.Combine(pathToLog!, "log.txt");
            using (StreamWriter writer = new StreamWriter(pathToLog))
            {
                writer.WriteLine($"[{DateTime.Now}] Метод: {methodName}");
                writer.WriteLine($"Ошибка: {ex.Message}");
                writer.WriteLine($"StackTrace: {ex.StackTrace}");
                writer.WriteLine("----------------------------------");
            }
        }
    }
}

