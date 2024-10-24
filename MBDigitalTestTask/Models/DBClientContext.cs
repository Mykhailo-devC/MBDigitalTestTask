using MBDigitalTestTask.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBDigitalTestTask.Models
{
    public class DbClientContext : DbContext
    {
        public DbClientContext(DbContextOptions<DbClientContext> options) : base (options)
        {
        }

        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<LibraryMember> Members { get; set; }
        public virtual DbSet<BorrowingHistory> History { get; set; }
        public virtual DbSet<BookLibrary> BooksLibrarys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.AuthorId);

            modelBuilder.Entity<BookLibrary>()
                .HasOne(x => x.Book)
                .WithMany(x => x.BookLibraries)
                .HasForeignKey(x => x.BookId);

            modelBuilder.Entity<BookLibrary>()
                .HasOne(x => x.Library)
                .WithMany(x => x.LibraryBooks)
                .HasForeignKey(x => x.LibraryId);

            modelBuilder.Entity<BorrowingHistory>()
                .HasOne(x => x.Book)
                .WithMany(x => x.History)
                .HasForeignKey(x => x.BookId);

            modelBuilder.Entity<BorrowingHistory>()
                .HasOne(x => x.User)
                .WithMany(x => x.History)
                .HasForeignKey(x => x.UserId);
        }
    }
}
