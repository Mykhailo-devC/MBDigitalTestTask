namespace MBDigitalTestTask.Models.Entities
{
    public class BookLibrary : Entity
    {
        public int BookId { get; set; }
        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }
        public virtual Book Book { get; set; }
    }
}
