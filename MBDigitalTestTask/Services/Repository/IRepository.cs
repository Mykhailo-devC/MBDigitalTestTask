using MBDigitalTestTask.Models.Entities;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Response;

namespace MBDigitalTestTask.Services.Repository
{
    public interface IRepository<TFilter, TEntity> : IReadOnlyRepository<TFilter, TEntity>
        where TEntity : Entity
        where TFilter : FilterBase
    {
        Task<Response> Create(TEntity request);
        Task<Response> Update(TEntity request);
        Task<Response> Delete(TFilter filter);
    }
}
