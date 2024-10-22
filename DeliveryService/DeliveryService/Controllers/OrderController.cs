using DeliveryService.Interfaces;
using DeliveryService.Models;
using DeliveryService.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml.Linq;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
       
        [HttpPost("[action]")]
        public ActionResult GenerateFile([FromBody] GenerateFileRequest request)
        {
            var startDate = request.StartDate;
            var districtId = request.DistrictId;
            var path = request.PathToFile;
            var pathToLog = request.PathToLog;

            _orderService.GenerateFile(startDate, districtId, path!, pathToLog!);

            return Ok();
        }
        
    }
}
