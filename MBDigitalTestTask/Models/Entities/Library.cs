namespace MBDigitalTestTask.Models.Entities
{
    public class Library : Entity
    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? WebsiteUrl { get; set; }
        public int? MaxBorrowLimit { get; set; }
        public virtual ICollection<BookLibrary> LibraryBooks { get; set; }
    }


}
