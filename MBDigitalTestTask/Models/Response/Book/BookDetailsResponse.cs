namespace MBDigitalTestTask.Models.Response.Book
{
    public class BookDetailsResponse : BookResponse
    {
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string ISBN { get; set; }
        public string Language { get; set; }
        public int TotalPages { get; set; }
    }
}
