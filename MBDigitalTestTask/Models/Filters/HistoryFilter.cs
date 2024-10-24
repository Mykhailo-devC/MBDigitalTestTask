namespace MBDigitalTestTask.Models.Filters
{
    public class HistoryFilter : FilterBase
    {
        public string MemberName { get; set; } = "";
        public string BookTitle { get; set; } = "";
        public int LibraryId { get; set; } = 0;
    }
}
