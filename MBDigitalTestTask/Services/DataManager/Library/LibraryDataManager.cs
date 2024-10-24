using AutoMapper;
using MBDigitalTestTask.Models;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.Library;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.Library;
using MBDigitalTestTask.Services.Repository.Library;

namespace MBDigitalTestTask.Services.DataManager.Library
{
    public class LibraryDataManager : DataManagerBase, ILibraryDataManager
    {
        private readonly ILibraryRepository _libraryRepository;
        public LibraryDataManager(IMapper mapper, DbClientContext context) : base(mapper)
        {
            _libraryRepository = new LibraryRepository(context);
        }

        public async Task<Response> UpdateData(UpdateLibraryRequest request)
        {
            var result = new Response();

            try
            {
                var library = _mapper.Map<Models.Entities.Library>(request);

                if(request.Id == 0)
                {
                    result = await _libraryRepository.Create(library);
                }
                else
                {
                    result = await _libraryRepository.Update(library);
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
                var filter = new LibraryFilter() { Id = id };
                result = await _libraryRepository.Delete(filter);
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response<LibraryResponse>> GetData(LibraryFilter filter)
        {
            var result = new Response<LibraryResponse>();

            try
            {
                var response = await _libraryRepository.Get(filter);

                if (response.IsSuccess)
                {

                    var libraries = _mapper.Map<List<LibraryResponse>>(response.Data.ToList());

                    result.Data = libraries;
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
