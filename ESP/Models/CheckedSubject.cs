namespace ESP.Models
{
    public class CheckedSubject
    {
        public string Name { get; set; } = string.Empty;

        public bool[] clientTypes { get; set; } = new bool[5];
    }
}
