namespace ESP.Models
{
    public class Revision
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string RevisionType { get; set; } = string.Empty;
        public int ProcessId { get; set; }
        public Process Process { get; set; } = null!;
        public int? ProcessInfoBlockId { get; set; }
        public ProcessInfoBlock? InfoBlock { get; set; } = null!;
        public int? TechnologistBlockId { get; set; }
        public TechnologistBlock? TechnologistBlock { get; set; } = null!;
        public int? BlockTestId { get; set; }
        public BlockTest? BlockTest { get; set; } = null!;
        public int? IntegrationId { get; set; }
        public Integration? Integration { get; set; } = null!;
        public int? PersonId { get; set; }
        public Person? Person { get; set; } = null!;
    }   
    public enum RevisionType
	{
        Primary = 0,
        Last = 1,
        Standart = 2,
	}
}
