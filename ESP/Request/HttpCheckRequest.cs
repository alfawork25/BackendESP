namespace ESP.Request
{
    public class HttpCheckRequest
    {
        public List<SubjectDto> subjects = new List<SubjectDto>();

        public int ClientType { get; set; }
    }


    public class SubjectDto
    {
        public int SubjectId { get; set; }
        public bool IsNewClient { get; set; }
    }
}
