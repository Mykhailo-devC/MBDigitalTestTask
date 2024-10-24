using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.History;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.History;
using MBDigitalTestTask.Services.DataManager.History;
using Microsoft.AspNetCore.Mvc;
namespace MBDigitalTestTask.Controllers.api.v1
{
    // User access rights

    [Controller]
    [Route("api/v1")]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryDataManager _historyDataManager;
        public HistoryController(IHistoryDataManager historyDataManager)
        {
            _historyDataManager = historyDataManager;
        }

        [HttpGet]
        [Route("history")]
        public async Task<ActionResult<Response<HistoryResponse>>> GetHistory()
        {
            var filter = new HistoryFilter();
            var result = await _historyDataManager.GetData(filter);
            return result;
        }

        [HttpPost]
        [Route("history/filter")]
        public async Task<ActionResult<Response<HistoryResponse>>> GetHistory([FromBody] HistoryFilter filter)
        {
            var result = await _historyDataManager.GetData(filter);
            return result;
        }

        [HttpPost]
        [Route("history")]
        public async Task<ActionResult<Response>> CreateRecord([FromBody] HistoryCreateRequest request)
        {
            var result = new Response();
            if (!ModelState.IsValid || request.Id < 0)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors(ModelState.Values.SelectMany(x => x.Errors.Select(x => x.ErrorMessage)).ToArray());
            }
            else
            {
                result = await _historyDataManager.UpdateData(request);
            }

            return result;
        }

        [HttpPut]
        [Route("history/{id}/return")]
        public async Task<ActionResult<Response>> ReturnBook(int id)
        {
            var result = new Response();

            if (id <= 0)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors("Incorrect Id");
            }
            else
            {
                result = await _historyDataManager.ReturnBook(id);
            }
            
            return result;
        }

        [HttpDelete]
        [Route("history/{id}")]
        public async Task<ActionResult<Response>> DeleteRecord([FromRoute] int id)
        {
            var result = new Response();

            if (id <= 0)
            {
                result.Status = StatusCodes.Status400BadRequest;
                result.SetErrors("Incorrect Id");
            }
            else
            {
                result = await _historyDataManager.DeleteData(id);
            }

            return result;
        }
    }
}
