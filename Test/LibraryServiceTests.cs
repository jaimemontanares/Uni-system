using UniversitySystem.Models;
using UniversitySystem.Services;
using Xunit;

namespace UniversitySystem.Tests
{
    public class LibraryServiceTests
    {
        [Fact]
        public void BorrowBook_ShouldFail_WhenNoCopiesAreAvailable()
        {
            // Arrange
            var userService = new UserService();
            var libraryService = new LibraryService(userService);

            var librarian = new Librarian(
                "U10",
                "LB100",
                "Test Librarian",
                "librarian@test.no",
                "librarian",
                "1234",
                "Bibliotek");

            var student = new Student(
                "U1",
                "S1001",
                "Test Student",
                "student@test.no",
                "student",
                "1234");

            userService.AddUser(librarian);
            userService.AddUser(student);

            libraryService.RegisterBook("B1", "Clean Code", "Robert C. Martin", 1, librarian.Id, out _);
            libraryService.BorrowBook(student.Id, "B1", out _);

            // Act
            bool result = libraryService.BorrowBook(student.Id, "B1", out string message);

            // Assert
            Assert.False(result);
            Assert.Equal("Ingen tilgjengelige eksemplarer.", message);
        }

        [Fact]
        public void ReturnBook_ShouldSucceed_WhenActiveLoanExists()
        {
            // Arrange
            var userService = new UserService();
            var libraryService = new LibraryService(userService);

            var librarian = new Librarian(
                "U10",
                "LB100",
                "Test Librarian",
                "librarian@test.no",
                "librarian",
                "1234",
                "Bibliotek");

            var student = new Student(
                "U1",
                "S1001",
                "Test Student",
                "student@test.no",
                "student",
                "1234");

            userService.AddUser(librarian);
            userService.AddUser(student);

            libraryService.RegisterBook("B1", "Clean Code", "Robert C. Martin", 1, librarian.Id, out _);
            libraryService.BorrowBook(student.Id, "B1", out _);

            // Act
            bool result = libraryService.ReturnBook(student.Id, "B1", out string message);

            // Assert
            Assert.True(result);
            Assert.Equal("Boken ble levert tilbake.", message);
        }
    }
}
