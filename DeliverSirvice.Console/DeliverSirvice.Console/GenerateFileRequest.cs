using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliverSirvice.Console
{
    public class GenerateFileRequest
    {
        public DateTime StartDate { get; set; }
        public int DistrictId { get; set; }
        public string? PathToFile { get; set; }
        public string? PathToLog { get; set; }
    }
}
