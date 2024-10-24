using MBDigitalTestTask.Models;
using MBDigitalTestTask.Models.Entities;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace MBDigitalTestTask.Services.Repository.Book
{
    public class BookRepository : RepositoryBase, IBookRepository
    {
        public BookRepository(DbClientContext context) : base(context)
        {
        }

        public async Task<Response> Create(Models.Entities.Book book)
        {
            var result = new Response();

            try
            {
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                var bookLibrary = new BookLibrary
                {
                    BookId = book.Id,
                    LibraryId = book.LibraryId,
                };

                await _context.BooksLibrarys.AddAsync(bookLibrary);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response> Delete(BookFilter filter)
        {
            var result = new Response();

            try
            {
                var books = FilterBooks(filter);
                _context.Books.RemoveRange(books);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response<Models.Entities.Book>> Get(BookFilter filter, bool includeDetails = false)
        {
            var result = new Response<Models.Entities.Book>();

            try
            {
                var books = FilterBooks(filter, includeDetails);
                result.Data = await books.ToListAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response> Update(Models.Entities.Book book)
        {
            var result = new Response();

            try
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        private IQueryable<Models.Entities.Book> FilterBooks(BookFilter filter, bool includeDetails = false)
        {
            var books = _context.Books.AsQueryable();

            if (includeDetails)
            {
                books = books
                    .Include(x => x.Author)
                    .Include(x => x.BookLibraries)
                        .ThenInclude(x => x.Library);
            }

            if (filter.Id != 0)
            {
                books = books.Where(x => x.Id == filter.Id);
            }

            if (filter.LibraryId != 0)
            {
                books = books.Where(x => x.BookLibraries.Any(c => c.LibraryId == filter.LibraryId));
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                books = books.Where(x => x.Title.Contains(filter.Title));
            }

            if (!string.IsNullOrEmpty(filter.AuthorName))
            {
                books = books.Include(x => x.Author).Where(x => x.Author.FirstName.Contains(filter.AuthorName) || 
                    x.Author.LastName.Contains(filter.AuthorName));
            }

            if (!string.IsNullOrEmpty(filter.ISBN))
            {
                books = books.Where(x => x.ISBN.Contains(filter.ISBN));
            }

            return books;

        }
    }
}
