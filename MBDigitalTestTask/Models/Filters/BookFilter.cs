namespace MBDigitalTestTask.Models.Filters
{
    public class BookFilter : FilterBase
    {
        public string Title { get; set; } = "";
        public string AuthorName { get; set; } = "";
        public string ISBN { get; set; } = "";
        public int LibraryId { get; set; } = 0;
    }
}
