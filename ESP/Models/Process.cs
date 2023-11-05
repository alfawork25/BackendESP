namespace ESP.Models
{
    public class Process
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string BlockTechnologist { get; set; } = string.Empty;

        public string SystemCode { get; set; } = string.Empty;

        public string? StartDate { get; set; }

        public string? PrimaryDate { get; set; }

        public int? Count { get; set; }

        public string? StartDateLastRevision { get; set; } = string.Empty;

        public string? StatusDate { get; set; } = string.Empty;

        public string? LastDateRevision { get; set; } = string.Empty;

        public List<CheckBlock> CheckBlocks { get; set; } = new List<CheckBlock>();
        public List<CheckCode> CheckCodes { get; set; } = new List<CheckCode>();
        public List<ProhibitionCode> ProhibitionCodes { get; set; } = new List<ProhibitionCode>();
        public List<SubjectType> SubjectTypes { get; set; } = new List<SubjectType>();

        public int? ClientTypeId { get; set; }
        public ClientType? ClientType { get; set; } 
        public int? SystemTypeId { get; set; }
        public SystemType? SystemType { get; set; }

        public List<StatusHistory> StatusHistories { get; set; } = new List<StatusHistory>();

        public int ReferenceProcessId { get; set; }
        public ReferenceProcess ReferenceProcess { get; set; } = null!;

        public List<Revision> Revisions { get; set; } = null!;
        

    }
}
