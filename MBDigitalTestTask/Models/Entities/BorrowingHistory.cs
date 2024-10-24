namespace MBDigitalTestTask.Models.Entities
{
    public class BorrowingHistory : Entity
    {
        public int UserId { get; set; }
        public LibraryMember User { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
