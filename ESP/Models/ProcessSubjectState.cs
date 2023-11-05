namespace ESP.Models
{
    public class ProcessSubjectState
    {
        public int Id { get; set; }

        public int SubjectId { get; set; }
        
        public bool IsNewClient { get; set; }

        public int ProcessId { get; set; }
    }
}
