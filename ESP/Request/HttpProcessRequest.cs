using ESP.Models;

namespace ESP.Request
{
    public class HttpProcessRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Steps { get; set; } = string.Empty;
 
        public string BlockTechnologist {get;set; } = string.Empty;
        public string SystemCode { get; set; } = string.Empty;

        public string? StartDate { get; set; }

        public string? PrimaryConnectionStatus { get; set; }

        public string? PrimaryDate { get; set; }
        
        public int Count { get; set; }

        public string? StartDateLastRevision { get; set; }

        public string? LastRevisionStatus { get; set; }

        public string? StatusDate { get; set; }

        public string? LastDateRevision { get; set; }
        public string Type { get; set; } = string.Empty;

        public List<int> CheckBlockIds { get; set; } = new List<int>();
        public List<int> CheckCodeIds { get; set; } = new List<int>();
        public List<int> SubjectIds { get; set; } = new List<int>();
        public List<int> ProhibitionCodeIds { get; set; } = new List<int>();
        public List<ProcessSubjectState> ProcessSubjectStates { get; set; } = new List<ProcessSubjectState>();
        public int SystemBlockId { get; set; }
        public int ClientTypeId { get; set; }
        public int SystemTypeId { get; set; }

        public List<StatusHistory> StatusHistories { get; set; } = new List<StatusHistory>();

    }



}
