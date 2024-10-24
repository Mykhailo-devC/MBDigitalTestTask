using MBDigitalTestTask.Models.Entities;

namespace MBDigitalTestTask.Models.Response.Library
{
    public class LibraryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string WebsiteUrl { get; set; }
        public int MaxBorrowLimit { get; set; }
    }
}
