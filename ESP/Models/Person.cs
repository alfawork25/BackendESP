namespace ESP.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string? ContactName { get; set; }

        public string? ResponsibleOKBP { get; set; }

        public string? ResponsibleTechnoglogist { get; set; }
        
        public Revision? Revision { get; set; }
    }
}
