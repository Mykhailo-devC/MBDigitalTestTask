using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.Book;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.Book;

namespace MBDigitalTestTask.Services.DataManager.Book
{
    public interface IBookDataManager
    {
        Task<Response<BookDetailsResponse>> GetDataDetails(BookFilter filter);
        Task<Response<BookResponse>> GetData(BookFilter filter);
        Task<Response> UpdateData(UpdateBookRequest request);
        Task<Response> DeleteData(int id);
    }
}
