using MBDigitalTestTask.Models;

namespace MBDigitalTestTask.Services.Repository
{
    public abstract class RepositoryBase
    {
        protected readonly DbClientContext _context;
        public RepositoryBase(DbClientContext context)
        {
            _context = context;
        }
    }
}
