using MBDigitalTestTask.Models.Entities;
using MBDigitalTestTask.Models.Filters;

namespace MBDigitalTestTask.Services.Repository.History
{
    public interface IHistoryRepository : IRepository<HistoryFilter, BorrowingHistory>
    {

    }
}
