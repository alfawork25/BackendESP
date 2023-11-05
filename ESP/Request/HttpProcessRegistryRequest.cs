namespace ESP.Request
{
    public class HttpProcessRegistryRequest
    {
        public int Id { get; set; }

        public int SystemCode { get; set; }

        public int ProcessFirstLevel { get; set; } 
        public int ProcessSecondLevel { get; set; }

        public string ProcessCodeThirdLevel { get; set; } = string.Empty;

        public string ProcessNameThirdLevel { get; set; } = string.Empty;

        public string ProcessReferenceUniqueName { get; set; } = string.Empty;

        public string? ProcessName { get; set; }

    }
}
