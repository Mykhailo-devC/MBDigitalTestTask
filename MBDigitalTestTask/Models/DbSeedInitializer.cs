using MBDigitalTestTask.Models.Entities;

namespace MBDigitalTestTask.Models
{
    public static class DbSeedInitializer
    {
        public static async Task CreateSafeSeedData(DbClientContext context)
        {
            await context.Database.BeginTransactionAsync();

            try
            {
                await SetSeedData(context);
            }
            catch
            {
                await context.Database.RollbackTransactionAsync();
            }

            await context.Database.CommitTransactionAsync();
        }

        public static async Task CreateSeedData(DbClientContext context)
        {
            await SetSeedData(context);
        }

        private static async Task SetSeedData(DbClientContext context)
        {
            if (!context.Libraries.Any())
            {
                await context.Libraries.AddRangeAsync(
                    new Library
                    {
                        Name = "Central Library",
                        Address = "123 Main St, Cityville",
                        Latitude = "34.0522",
                        Longitude = "-118.2437",
                        WebsiteUrl = "http://www.cityvillelibrary.com",
                        MaxBorrowLimit = 5
                    },
                    new Library
                    {
                        Name = "North Branch",
                        Address = "456 Elm St, Cityville",
                        Latitude = "34.0622",
                        Longitude = "-118.2537",
                        WebsiteUrl = "http://www.cityvillenorthlibrary.com",
                        MaxBorrowLimit = 4
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Authors.Any())
            {
                await context.Authors.AddRangeAsync(
                    new Author
                    {
                        FirstName = "George",
                        LastName = "Orwell",
                        Email = "george.orwell@example.com",
                        DateOfBirth = new DateTime(1903, 6, 25)
                    },
                    new Author
                    {
                        FirstName = "J.K.",
                        LastName = "Rowling",
                        Email = "jk.rowling@example.com",
                        DateOfBirth = new DateTime(1965, 7, 31)
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.Books.Any())
            {
                await context.Books.AddRangeAsync(
                    new Book
                    {
                        Title = "1984",
                        PublishedDate = new DateTime(1949, 6, 8),
                        ISBN = "1234567890123",
                        TotalPages = 328,
                        Language = "English",
                        AuthorId = 1
                    },
                    new Book
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        PublishedDate = new DateTime(1997, 6, 26),
                        ISBN = "1234567890987",
                        TotalPages = 223,
                        Language = "English",
                        AuthorId = 2
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.BooksLibrarys.Any())
            {
                await context.BooksLibrarys.AddRangeAsync(
                        new BookLibrary
                        {
                            BookId = 1,
                            LibraryId = 1,
                        },
                        new BookLibrary
                        {
                            BookId = 2,
                            LibraryId = 1,
                        },
                        new BookLibrary
                        {
                            BookId = 2,
                            LibraryId = 2,
                        }
                    );
            }

            if (!context.Members.Any())
            {
                await context.Members.AddRangeAsync(
                    new LibraryMember
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        MembershipStartDate = new DateTime(2023, 1, 10),
                        Email = "john.doe@example.com",
                        PhoneNumber = "555-1234",
                        IsActive = true
                    },
                    new LibraryMember
                    {
                        FirstName = "Jane",
                        LastName = "Smith",
                        MembershipStartDate = new DateTime(2022, 8, 20),
                        Email = "jane.smith@example.com",
                        PhoneNumber = "555-5678",
                        IsActive = true
                    }
                );
                await context.SaveChangesAsync();
            }

            if (!context.History.Any())
            {
                await context.History.AddRangeAsync(
                    new BorrowingHistory
                    {
                        UserId = 1,
                        BookId = 1,
                        BorrowedDate = new DateTime(2023, 1, 15),
                        DueDate = new DateTime(2023, 2, 15),
                        ReturnedDate = new DateTime(2023, 2, 10)
                    },
                    new BorrowingHistory
                    {
                        UserId = 2,
                        BookId = 2,
                        BorrowedDate = new DateTime(2023, 1, 20),
                        DueDate = new DateTime(2023, 2, 20),
                        ReturnedDate = null
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
