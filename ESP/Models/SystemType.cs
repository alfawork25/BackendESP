namespace ESP.Models
{
    public class SystemType
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public List<Process> Processes { get; set; } = new List<Process>();
    }
}
