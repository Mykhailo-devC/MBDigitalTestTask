namespace MBDigitalTestTask.Models.Entities
{
    public class Author : Entity
    {     
        public string FirstName { get; set; }   
        public string LastName { get; set; }     
        public string Email {  get; set; }
        public DateTime DateOfBirth { get; set; } 
        public virtual ICollection<Book> Books { get; set; }
    }
}
