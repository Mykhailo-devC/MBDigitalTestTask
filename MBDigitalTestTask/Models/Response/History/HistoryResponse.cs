namespace MBDigitalTestTask.Models.Response.History
{
    public class HistoryResponse
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
