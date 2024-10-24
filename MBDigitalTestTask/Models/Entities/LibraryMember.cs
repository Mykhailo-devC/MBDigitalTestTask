namespace MBDigitalTestTask.Models.Entities
{
    public class LibraryMember : Entity
    {       
        public string FirstName { get; set; }       
        public string LastName { get; set; }    
        public DateTime MembershipStartDate { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<BorrowingHistory> History { get; set; }
    }
}
