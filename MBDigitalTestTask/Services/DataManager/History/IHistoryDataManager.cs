using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.History;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.History;

namespace MBDigitalTestTask.Services.DataManager.History
{
    public interface IHistoryDataManager
    {
        Task<Response<HistoryResponse>> GetData(HistoryFilter filter);
        Task<Response> ReturnBook(int id);
        Task<Response> UpdateData(HistoryCreateRequest request);
        Task<Response> DeleteData(int id);
    }
}
