using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.Book;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.Book;
using MBDigitalTestTask.Services.DataManager.Book;
using Microsoft.AspNetCore.Mvc;
namespace MBDigitalTestTask.Controllers.api.v1
{
    //Author access rights

    [Controller]
    [Route("api/v1")]
    public class BookController : ControllerBase
    {
        private readonly IBookDataManager _bookDataManager;
        public BookController(IBookDataManager bookDataManager)
        {
            _bookDataManager = bookDataManager;
        }

        [HttpGet]
        [Route("books")]
        public async Task<ActionResult<Response<BookResponse>>> GetBooks()
        {
            var filter = new BookFilter();
            var result = await _bookDataManager.GetData(filter);
            return result;
        }

        [HttpGet]
        [Route("books/{id}")]
        public async Task<ActionResult<Response<BookDetailsResponse>>> GetBookDetails(int id)
        {
            var result = new Response<BookDetailsResponse>();
            if (id <= 0)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors("Incorrect Id");
            }
            else
            {
                var filter = new BookFilter() { Id = id };
                result = await _bookDataManager.GetDataDetails(filter);
            }

            return result;
        }

        [HttpPost]
        [Route("books/filter")]
        public async Task<ActionResult<Response<BookResponse>>> GetBooks([FromBody] BookFilter filter)
        {
            var result = await _bookDataManager.GetData(filter);
            return result;
        }

        [HttpPost]
        [Route("books")]
        public async Task<ActionResult<Response>> UpdateBook([FromBody] UpdateBookRequest request)
        {
            var result = new Response();
            if (!ModelState.IsValid || (request.Id < 0 && request.LibraryId < 0))
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors(ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)).ToArray());
            }
            else
            {
                result = await _bookDataManager.UpdateData(request);
            }

            return result;
        }

        [HttpDelete]
        [Route("books/{id}")]
        public async Task<ActionResult<Response>> DeleteBook([FromRoute] int id)
        {
            var result = new Response();

            if (id <= 0)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors("Incorrect Id");
            }
            else
            {
                result = await _bookDataManager.DeleteData(id);
            }

            return result;
        }
    }
}
