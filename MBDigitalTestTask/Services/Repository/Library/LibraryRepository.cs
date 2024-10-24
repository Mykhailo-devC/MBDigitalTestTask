using MBDigitalTestTask.Models;
using MBDigitalTestTask.Models.Filters;
using MBDigitalTestTask.Models.Response;
using Microsoft.EntityFrameworkCore;

namespace MBDigitalTestTask.Services.Repository.Library
{
    public class LibraryRepository : RepositoryBase, ILibraryRepository
    {
        public LibraryRepository(DbClientContext context) : base(context)
        {
        }

        public async Task<Response> Create(Models.Entities.Library library)
        {
            var result = new Response();

            try
            {
                await _context.Libraries.AddAsync(library);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response> Delete(LibraryFilter filter)
        {
            var result = new Response();

            try
            {
                var libraries = FilterLibraries(filter);
                _context.Libraries.RemoveRange(libraries);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response<Models.Entities.Library>> Get(LibraryFilter filter, bool includeDetails = false)
        {
            var result = new Response<Models.Entities.Library>();

            try
            {
                var libraries = FilterLibraries(filter, includeDetails);
                result.Data = await libraries.ToListAsync();
            }
            catch(Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        public async Task<Response> Update(Models.Entities.Library library)
        {
            var result = new Response();

            try
            {
                _context.Libraries.Update(library);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.SetErrors(ex.Message);
                result.Status = StatusCodes.Status500InternalServerError;
            }

            return result;
        }

        private IQueryable<Models.Entities.Library> FilterLibraries(LibraryFilter filter, bool includeDetails = false)
        {
            var libraries = _context.Libraries.AsQueryable();

            if (includeDetails)
            {
                libraries = libraries
                    .Include(x => x.LibraryBooks)
                        .ThenInclude(x => x.Book);
            }

            if(filter.Id != 0)
            {
                libraries = libraries.Where(x => x.Id == filter.Id);
            }

            if (!string.IsNullOrEmpty(filter.Name))
            {
                libraries = libraries.Where(x => x.Name.Contains(filter.Name));
            }

            return libraries;

        }
    }
}
