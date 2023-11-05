namespace ESP.Models
{
    public class StatusHistory
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public int ProcessId { get; set; }

        public Process? Process { get; set; }
    }
}
