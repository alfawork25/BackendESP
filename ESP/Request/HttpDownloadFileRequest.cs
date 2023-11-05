using ESP.Models;

namespace ESP.Request
{
    public class HttpDownloadFileRequest
    {
        public List<Check> Checks { get; set; } = new List<Check>();

        public List<CheckedSubject> checkedSubjects { get; set; } = new List<CheckedSubject>();

    }
}
