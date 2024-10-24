using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.Library;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.Library;

namespace MBDigitalTestTask.Services.DataManager.Library
{
    public interface ILibraryDataManager
    {
        Task<Response<LibraryResponse>> GetData(LibraryFilter filter);
        Task<Response> UpdateData(UpdateLibraryRequest response);
        Task<Response> DeleteData(int id);
    }
}
