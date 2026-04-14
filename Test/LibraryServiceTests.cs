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

            var student = new Student("U1", "S1001", "Test Student", "student@test.no")
            {
                Username = "student",
                Password = "1234"
            };

            userService.AddUser(student);

            libraryService.RegisterBook("B1", "Clean Code", "Robert C. Martin", 1, out _);
            libraryService.BorrowBook(student.UserId, "B1", out _);

            // Act
            bool result = libraryService.BorrowBook(student.UserId, "B1", out string message);

            // Assert
            Assert.False(result);
            Assert.Equal("Boken er ikke tilgjengelig for utlån.", message);
        }

        [Fact]
        public void ReturnBook_ShouldSucceed_WhenActiveLoanExists()
        {
            // Arrange
            var userService = new UserService();
            var libraryService = new LibraryService(userService);

            var student = new Student("U1", "S1001", "Test Student", "student@test.no")
            {
                Username = "student",
                Password = "1234"
            };

            userService.AddUser(student);

            libraryService.RegisterBook("B1", "Clean Code", "Robert C. Martin", 1, out _);
            libraryService.BorrowBook(student.UserId, "B1", out _);

            // Act
            bool result = libraryService.ReturnBook(student.UserId, "B1", out string message);

            // Assert
            Assert.True(result);
            Assert.Equal("Boken ble levert tilbake.", message);
        }
    }
}
