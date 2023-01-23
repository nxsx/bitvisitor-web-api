namespace HTCBitVisitorWebApi.Models
{
    public class BitvisitorData
    {
        public int Id { get; set; }
        public string VisitorType { get; set; } = string.Empty;
        public string VisitorTime { get; set; } = string.Empty;
        public int VisitorId { get; set; }
        public string VisitorName { get; set; } = string.Empty;
        public string VisitorComp { get; set; } = string.Empty;
        public string VisitorMobile { get; set; } = string.Empty;
        public string VisitorVehicle { get; set; } = string.Empty;
        public string VisitorHostDeparture { get; set; } = string.Empty;
        public string VisitorHostName { get; set; } = string.Empty;
    }
}
