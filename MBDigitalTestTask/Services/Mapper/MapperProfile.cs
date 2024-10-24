using AutoMapper;
using MBDigitalTestTask.Models.Entities;
using MBDigitalTestTask.Models.Request.Book;
using MBDigitalTestTask.Models.Request.History;
using MBDigitalTestTask.Models.Request.Library;
using MBDigitalTestTask.Models.Response.Book;
using MBDigitalTestTask.Models.Response.History;
using MBDigitalTestTask.Models.Response.Library;

namespace MBDigitalTestTask.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateLibraryMapping();
            CreateBookMapping();
            CreateHistoryMapping();
        }

        private void CreateLibraryMapping()
        {
            CreateMap<UpdateLibraryRequest, Library>();
            CreateMap<Library, LibraryResponse>();
        }

        private void CreateBookMapping()
        {
            CreateMap<UpdateBookRequest, Book>()
                .ForMember(x => x.LibraryId, x => x.MapFrom(c => c.LibraryId)); 

            CreateMap<Book, BookResponse>();
            CreateMap<Book, BookDetailsResponse>()
                .ForMember(x => x.AuthorName, x => x.MapFrom(c => c.Author.FirstName + " " + c.Author.LastName))
                .ForMember(x => x.AuthorName, x => x.MapFrom(c => c.Author.Email));

            CreateMap<(int bookId, int libraryId), BookLibrary>()
                .ForMember(x => x.BookId, x => x.MapFrom(c => c.bookId))
                .ForMember(x => x.LibraryId, x => x.MapFrom(c => c.libraryId));

        }

        private void CreateHistoryMapping()
        {
            CreateMap<HistoryCreateRequest, BorrowingHistory>();
            CreateMap<BorrowingHistory, HistoryResponse>()
                .ForMember(x => x.MemberName, x => x.MapFrom(c => c.User.FirstName + " " + c.User.LastName))
                .ForMember(x => x.BookTitle, x => x.MapFrom(c => c.Book.Title));
        }
    }
}
