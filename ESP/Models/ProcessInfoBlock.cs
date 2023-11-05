namespace ESP.Models
{
    public class ProcessInfoBlock
    {
        public int Id { get; set; }

        public string? Modification { get; set; }

        public bool? Current { get; set; }

        public string? StartDate { get; set; }

        public Status? Status { get; set; }

        public string? ProcessInfo { get; set; }

        public Revision? Revision { get; set; }

        
    }
}
