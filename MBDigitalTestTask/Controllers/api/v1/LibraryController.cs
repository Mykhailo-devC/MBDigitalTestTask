using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.Library;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.Library;
using MBDigitalTestTask.Services.DataManager.Library;
using Microsoft.AspNetCore.Mvc;
namespace MBDigitalTestTask.Controllers.api.v1
{
    //Admin access rights

    [ApiController]
    [Route("api/v1")]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryDataManager _libraryDataManager;
        public LibraryController(ILibraryDataManager libraryDataManager)
        {
            _libraryDataManager = libraryDataManager;
        }

        [HttpGet]
        [Route("libraries")]
        public async Task<ActionResult<Response<LibraryResponse>>> GetLibraries()
        {
            var filter = new LibraryFilter();
            var result = await _libraryDataManager.GetData(filter);
            return result;
        }

        [HttpPost]
        [Route("libraries/filter")]
        public async Task<ActionResult<Response<LibraryResponse>>> GetLibraries([FromBody] LibraryFilter filter)
        {
            var result = await _libraryDataManager.GetData(filter);
            return result;
        }

        [HttpPost]
        [Route("libraries")]
        public async Task<ActionResult<Response>> UpdateLibrary([FromBody] UpdateLibraryRequest request)
        {
            var result = new Response();
            if (!ModelState.IsValid || request.Id < 0)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors(ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)).ToArray());
            }
            else
            {
                result = await _libraryDataManager.UpdateData(request);
            }
            
            return result;
        }

        [HttpDelete]
        [Route("libraries/{id}")]
        public async Task<ActionResult<Response>> DeleteLibrary([FromRoute] int id)
        {
            var result = new Response();
            if (id <= 0)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors("Incorrect Id");
            }
            else
            {
                result = await _libraryDataManager.DeleteData(id);
            }

            return result;
        }
    }
}
