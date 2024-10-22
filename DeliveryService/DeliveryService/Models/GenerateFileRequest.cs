namespace DeliveryService.Models
{
    public class GenerateFileRequest
    {
        public DateTime StartDate { get; set; }
        public int DistrictId { get; set; }

        public string? PathToFile { get; set; }
        public string? PathToLog { get; set; }
    }
}
