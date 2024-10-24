using MBDigitalTestTask.Models;
using MBDigitalTestTask.Models.Entities;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace MBDigitalTestTask.Services.Repository.History
{
    public class HistoryRepository : RepositoryBase, IHistoryRepository
    {
        public HistoryRepository(DbClientContext context) : base(context)
        {
        }

        public async Task<Response> Create(BorrowingHistory record)
        {
            var result = new Response();

            try
            {
                await _context.History.AddAsync(record);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response> Delete(HistoryFilter filter)
        {
            var result = new Response();

            try
            {
                var records = FilterRecords(filter);
                _context.History.RemoveRange(records);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response<BorrowingHistory>> Get(HistoryFilter filter, bool includeDetails = false)
        {
            var result = new Response<BorrowingHistory>();

            try
            {
                var records = FilterRecords(filter, includeDetails);
                result.Data = await records.ToListAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response> Update(BorrowingHistory record)
        {
            var result = new Response();

            try
            {
                _context.History.Update(record);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        private IQueryable<BorrowingHistory> FilterRecords(HistoryFilter filter, bool includeDetails = false)
        {
            var records = _context.History.AsQueryable();

            if (includeDetails)
            {
                records = records
                    .Include(x => x.Book)
                    .Include(x => x.User);
            }

            if (filter.Id != 0)
            {
                records = records.Where(x => x.Id == filter.Id);
            }

            if (filter.LibraryId != 0)
            {
                records = records
                    .Include(x => x.Book)
                    .ThenInclude(x => x.BookLibraries)
                    .Where(x => x.Book.BookLibraries.Any(c => c.LibraryId == filter.LibraryId));
            }

            if (!string.IsNullOrEmpty(filter.BookTitle))
            {
                records = records.Include(x => x.Book).Where(x => x.Book.Title.Contains(filter.BookTitle));
            }

            if (!string.IsNullOrEmpty(filter.MemberName))
            {
                records = records.Include(x => x.User).Where(x => x.User.FirstName.Contains(filter.MemberName) ||
                    x.User.LastName.Contains(filter.MemberName));
            }

            return records;

        }
    }
}
