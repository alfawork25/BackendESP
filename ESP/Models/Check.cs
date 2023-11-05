namespace ESP.Models
{
    public class Check
    {
        public int Id { get; set; }
        public string BlockName { get; set; } = string.Empty;
        public string ComplianceCheck { get; set; } = string.Empty;

        public List<Subject> Subjects { get; set; } = new List<Subject>();

    }

    public class Subject
    {
        public string SubjectName { get; set; } = string.Empty;
        public ValidationCodeDto[] Value { get; set; } = null!;

    }
    public class ValidationCodeDto
    {
        public List<ValidationCode> validationCodes { get; set; } = new List<ValidationCode>(); 
    }
    public class ValidationCode : CheckCode
    {
        public List<ProhibitionCode> newProhibitionCodes { get; set; } = new List<ProhibitionCode>();
        
    }

}
