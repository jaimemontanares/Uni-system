using UniversitySystem.Models;
using UniversitySystem.Services;
using Xunit;

namespace UniversitySystem.Tests
{
    public class AuthorizationServiceTests
    {
        [Fact]
        public void RegisterStudent_ShouldFail_WhenUsernameAlreadyExists()
        {
            // Arrange
            var userService = new UserService();
            var authorizationService = new AuthorizationService(userService);

            var existingStudent = new Student(
                "U1",
                "S1001",
                "Eksisterende Student",
                "existing@student.no",
                "student1",
                "1234");

            userService.AddUser(existingStudent);

            // Act
            bool result = authorizationService.RegisterStudent(
                "U2",
                "S1002",
                "Ny Student",
                "new@student.no",
                "student1",
                "5678",
                out string message);

            // Assert
            Assert.False(result);
            Assert.Equal("Brukernavnet er allerede i bruk.", message);
        }
    }
}
