using MBDigitalTestTask.Models.Filters;

namespace MBDigitalTestTask.Services.Repository.Book
{
    public interface IBookRepository : IRepository<BookFilter, Models.Entities.Book>
    {

    }
}
