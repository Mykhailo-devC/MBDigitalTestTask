using System.ComponentModel.DataAnnotations;

namespace MBDigitalTestTask.Models.Request.Library
{
    public class UpdateLibraryRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string WebsiteUrl { get; set; }
        [Required]
        public int MaxBorrowLimit { get; set; }
    }
}
