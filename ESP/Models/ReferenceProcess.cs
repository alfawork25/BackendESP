namespace ESP.Models
{
    public class ReferenceProcess
    {
        public int Id { get; set; }

        public int? SystemBlockId { get; set; }

        public SystemBlock? SystemBlock { get; set; }

        public ProcessOneLevel? ProcessOneLevel { get; set; }

        public ProcessTwoLevel? ProcessTwoLevel { get; set; }

        public string? ProcessCodeThirdLevel { get; set; }

        public string? ProcessNameThirdLevel { get; set; }

        public string? ProcessReferenceUniqueName { get; set; }

        public string? ProcessName { get; set; }

        public Process Process { get; set; } = null!;

    }
}
