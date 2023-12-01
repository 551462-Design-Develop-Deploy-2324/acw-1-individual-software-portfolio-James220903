using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Staff_Monitor_Engagement;
using Moq;
using NUnit.Framework;


namespace Staff_Monitor_Engagement_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Teststudentconstructor()
        {
            Student student = new Student();
        }
    }
    
    [TestClass]
    public class LoginTests
    {
        private Mock<Database> mockDatabase;

        [SetUp]
        public void Setup()
        {
            mockDatabase = new Mock<Database>();
        }

        [TestMethod]
        public void Login_ValidStudentCredentials_ReturnsStudentRole()
        {
            // Arrange
            string validStudentUsername = "studentUser";
            string validStudentPassword = "studentPass";
            mockDatabase.Setup(db => db.IsUserValid(validStudentUsername, validStudentPassword, "STUDENT")).Returns(true);

            // Act
            var result = mockDatabase.Object.Login(validStudentUsername, validStudentPassword);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(UserRole.Student, result);
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsNoneRole()
        {
            // Arrange
            string invalidUsername = "invalidUser";
            string invalidPassword = "invalidPass";
            mockDatabase.Setup(db => db.IsUserValid(invalidUsername, invalidPassword, It.IsAny<string>())).Returns(false);

            // Act
            var result = mockDatabase.Object.Login(invalidUsername, invalidPassword);

            // Assert
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(UserRole.None, result);
        }

        // Additional tests for PersonalSupervisor and SeniorTutor roles
    }
}