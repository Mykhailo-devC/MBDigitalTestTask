using AutoMapper;
using MBDigitalTestTask.Models;
using MBDigitalTestTask.Models.Entities;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Request.History;
using MBDigitalTestTask.Models.Response;
using MBDigitalTestTask.Models.Response.History;
using MBDigitalTestTask.Services.Repository.History;

namespace MBDigitalTestTask.Services.DataManager.History
{
    public class HistoryDataManager : DataManagerBase, IHistoryDataManager
    {
        private readonly IHistoryRepository _historyRepository;
        public HistoryDataManager(IMapper mapper, DbClientContext context) : base(mapper)
        {
            _historyRepository = new HistoryRepository(context);
        }

        public async Task<Response> UpdateData(HistoryCreateRequest request)
        {
            var result = new Response();

            try
            {
                var record = _mapper.Map<BorrowingHistory>(request);

                if (request.Id == 0)
                {
                    result = await _historyRepository.Create(record);
                }
                else
                {
                    result = await _historyRepository.Update(record);
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
                var filter = new HistoryFilter() { Id = id };
                result = await _historyRepository.Delete(filter);
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response<HistoryResponse>> GetData(HistoryFilter filter)
        {
            var result = new Response<HistoryResponse>();

            try
            {
                var response = await _historyRepository.Get(filter, true);

                if (response.IsSuccess)
                {

                    var records = _mapper.Map<List<HistoryResponse>>(response.Data.ToList());

                    result.Data = records;
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

        public async Task<Response> ReturnBook(int id)
        {
            var result = new Response();

            try
            {
                var filter = new HistoryFilter() { Id = id };
                var response = await _historyRepository.Get(filter);

                if (response.IsSuccess)
                {
                    var record = response.Data.FirstOrDefault();
                    record.ReturnedDate = DateTime.Now;

                    result = await _historyRepository.Update(record);
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
