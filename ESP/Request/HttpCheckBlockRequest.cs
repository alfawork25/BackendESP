
namespace ESP.Request
{
    public class HttpCheckBlockRequest
    {
        public int Id { get; set; }
        public int BlockId { get; set; }
        public string ShortName { get; set; } = null!;
        public int SequenceNumber { get; set; }
        public List<string> Subjects { get; set; } = null!;
        public List<int> ClientTypes { get; set; } =  new List<int>();
        public List<CheckCodeDto> CheckCodes { get; set; } = new List<CheckCodeDto>();
    }

    public class CheckCodeDto
    {
        public int Id { get; set; }
        public List<SubjectsToCheckCode> subjectsToCheckCode { get; set; } = new List<SubjectsToCheckCode>();

    }

    public class SubjectsToCheckCode
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
