namespace ESP.Models
{
    public class Integration
    {
        public int Id { get; set; }

        public string? ApprovedProfile { get; set; }

        public string? ApprovedProm { get; set; }

        public string? ApprovedWithNote { get; set; }

        public string? Integrated { get; set; }
        public Revision? Revision { get; set; }
    }
}
