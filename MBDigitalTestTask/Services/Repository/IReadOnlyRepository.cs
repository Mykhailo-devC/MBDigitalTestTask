using MBDigitalTestTask.Models.Entities;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Response;

namespace MBDigitalTestTask.Services.Repository
{
    public interface IReadOnlyRepository<TFilter, TEntity>
        where TEntity : Entity
        where TFilter : FilterBase
    {
        Task<Response<TEntity>> Get(TFilter filter, bool includeDetails = false);
    }
}
