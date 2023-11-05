namespace ESP.Models
{
    public class BlockTest
    {
        public int Id { get; set; }

        public string? RequestDate { get; set; }

        public string? DirectionDate { get; set; }

        public string? ResultDate { get; set; }

        public bool? Note { get; set; }

        public string? EndDate { get; set; }
        public Revision? Revision { get; set; }
    }
}
