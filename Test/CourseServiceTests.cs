using UniversitySystem.Models;
using UniversitySystem.Services;
using Xunit;

namespace UniversitySystem.Tests
{
    public class CourseServiceTests
    {
        [Fact]
        public void EnrollStudent_ShouldFail_WhenStudentIsAlreadyEnrolled()
        {
            // Arrange
            var userService = new UserService();
            var courseService = new CourseService(userService);

            var lecturer = new Lecturer(
                "U1",
                "L100",
                "Test Lecturer",
                "lecturer@test.no",
                "lecturer",
                "1234",
                "IT");

            var student = new Student(
                "U2",
                "S1001",
                "Test Student",
                "student@test.no",
                "student",
                "1234");

            userService.AddUser(lecturer);
            userService.AddUser(student);

            courseService.CreateCourse("IS110", "Programmering", 10, 30, lecturer.Id, out _);
            courseService.EnrollStudent(student.Id, "IS110", out _);

            // Act
            bool result = courseService.EnrollStudent(student.Id, "IS110", out string message);

            // Assert
            Assert.False(result);
            Assert.Equal("Studenten er allerede meldt på kurset.", message);
        }

        [Fact]
        public void CreateCourse_ShouldFail_WhenCourseCodeAlreadyExists()
        {
            // Arrange
            var userService = new UserService();
            var courseService = new CourseService(userService);

            var lecturer = new Lecturer(
                "U1",
                "L100",
                "Test Lecturer",
                "lecturer@test.no",
                "lecturer",
                "1234",
                "IT");

            userService.AddUser(lecturer);

            courseService.CreateCourse("IS110", "Programmering", 10, 30, lecturer.Id, out _);

            // Act
            bool result = courseService.CreateCourse(
                "IS110",
                "Objektorientert programmering",
                10,
                25,
                lecturer.Id,
                out string message);

            // Assert
            Assert.False(result);
            Assert.Equal("Et kurs med denne kurskoden finnes allerede.", message);
        }
    }
}
