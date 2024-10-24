using AutoMapper;
using MBDigitalTestTask.Models;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.Book;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.Book;
using MBDigitalTestTask.Services.Repository.Book;

namespace MBDigitalTestTask.Services.DataManager.Book
{
    public class BookDataManager : DataManagerBase, IBookDataManager
    {
        private readonly IBookRepository _bookRepository;
        public BookDataManager(IMapper mapper, DbClientContext context) : base(mapper)
        {
            _bookRepository = new BookRepository(context);
        }

        public async Task<Response> UpdateData(UpdateBookRequest request)
        {
            var result = new Response();

            try
            {
                var book = _mapper.Map<Models.Entities.Book>(request);

                if (request.Id == 0)
                {
                    result = await _bookRepository.Create(book);
                }
                else
                {
                    result = await _bookRepository.Update(book);
                }

            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response> DeleteData(int id)
        {
            var result = new Response();

            try
            {
                var filter = new BookFilter() { Id = id };
                result = await _bookRepository.Delete(filter);
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response<BookResponse>> GetData(BookFilter filter)
        {
            var result = new Response<BookResponse>();

            try
            {
                var response = await _bookRepository.Get(filter);

                if (response.IsSuccess)
                {

                    var books = _mapper.Map<List<BookResponse>>(response.Data.ToList());

                    result.Data = books;
                }
                else
                {
                    result.Status = response.Status;
                    response.Errors = response.Errors;
                }
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response<BookDetailsResponse>> GetDataDetails(BookFilter filter)
        {
            var result = new Response<BookDetailsResponse>();

            try
            {
                var response = await _bookRepository.Get(filter, true);

                if (response.IsSuccess)
                {

                    var books = _mapper.Map<List<BookDetailsResponse>>(response.Data.ToList());

                    result.Data = books;
                }
                else
                {
                    result.Status = response.Status;
                    response.Errors = response.Errors;
                }
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }
    }
}
