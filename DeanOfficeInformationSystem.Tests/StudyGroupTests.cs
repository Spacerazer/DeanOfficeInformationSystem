using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DeanOfficeInformationSystem.Tests
{
    [TestClass]
    public class StudyGroupTests
    {
        private DatabaseService _dbService;
        private Mock<SqlConnection> _mockConnection;
        private Mock<SqlCommand> _mockCommand;
        private Mock<SqlDataReader> _mockReader;

        [TestInitialize]
        public void Setup()
        {
            _mockConnection = new Mock<SqlConnection>();
            _mockCommand = new Mock<SqlCommand>();
            _mockReader = new Mock<SqlDataReader>();

            _mockCommand.Setup(c => c.ExecuteReader()).Returns(_mockReader.Object);
            _mockConnection.Setup(c => c.CreateCommand()).Returns(_mockCommand.Object);
        }

        [TestMethod]
        public void GetAllStudyGroups_ShouldReturnListOfGroups()
        {
            // Arrange
            var expectedGroups = new List<DatabaseService.StudyGroup>
            {
                new DatabaseService.StudyGroup
                {
                    Id = 1,
                    GroupName = "ИС-20",
                    Course = 3,
                    Speciality = "Информационные системы",
                    FormationYear = 2020,
                    HeadmanId = 1,
                    HeadmanFullName = "Иванов Иван Иванович"
                }
            };

            _mockReader.Setup(r => r.Read())
                .Returns(() => expectedGroups.Count > 0)
                .Callback(() => expectedGroups.RemoveAt(0));

            _mockReader.Setup(r => r["Id"]).Returns(1);
            _mockReader.Setup(r => r["GroupName"]).Returns("ИС-20");
            _mockReader.Setup(r => r["Course"]).Returns(3);
            _mockReader.Setup(r => r["Speciality"]).Returns("Информационные системы");
            _mockReader.Setup(r => r["FormationYear"]).Returns(2020);
            _mockReader.Setup(r => r["HeadmanId"]).Returns(1);
            _mockReader.Setup(r => r["HeadmanFullName"]).Returns("Иванов Иван Иванович");

            // Act
            var result = _dbService.GetAllStudyGroups();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("ИС-20", result[0].GroupName);
            Assert.AreEqual(3, result[0].Course);
        }

        [TestMethod]
        public void AddStudyGroup_ShouldExecuteCorrectQuery()
        {
            // Arrange
            var newGroup = new DatabaseService.StudyGroup
            {
                GroupName = "ПИ-23",
                Course = 1,
                Speciality = "Программная инженерия",
                FormationYear = 2023,
                HeadmanId = 2
            };

            string expectedQuery = @"INSERT INTO StudyGroup (GroupName, Course, Speciality, FormationYear, HeadmanId) 
                               VALUES (@GroupName, @Course, @Speciality, @FormationYear, @HeadmanId)";

            // Act
            _dbService.AddStudyGroup(newGroup);

            // Assert
            _mockCommand.Verify(c => c.ExecuteNonQuery(), Times.Once);
            _mockCommand.Verify(c => c.Parameters.AddWithValue("@GroupName", newGroup.GroupName), Times.Once);
            _mockCommand.Verify(c => c.Parameters.AddWithValue("@Course", newGroup.Course), Times.Once);
        }

        [TestMethod]
        public void UpdateStudyGroup_ShouldExecuteCorrectQuery()
        {
            // Arrange
            var group = new DatabaseService.StudyGroup
            {
                Id = 1,
                GroupName = "ИС-20",
                Course = 4,
                Speciality = "Информационные системы",
                FormationYear = 2020,
                HeadmanId = 1
            };

            // Act
            _dbService.UpdateStudyGroup(group);

            // Assert
            _mockCommand.Verify(c => c.ExecuteNonQuery(), Times.Once);
            _mockCommand.Verify(c => c.Parameters.AddWithValue("@Id", group.Id), Times.Once);
            _mockCommand.Verify(c => c.Parameters.AddWithValue("@GroupName", group.GroupName), Times.Once);
        }

        [TestMethod]
        public void DeleteStudyGroup_ShouldExecuteCorrectQuery()
        {
            // Arrange
            int groupId = 1;
            string expectedQuery = "DELETE FROM StudyGroup WHERE Id = @Id";

            // Act
            _dbService.DeleteStudyGroup(groupId);

            // Assert
            _mockCommand.Verify(c => c.ExecuteNonQuery(), Times.Once);
            _mockCommand.Verify(c => c.Parameters.AddWithValue("@Id", groupId), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddStudyGroup_WithInvalidCourse_ShouldThrowException()
        {
            // Arrange
            var invalidGroup = new DatabaseService.StudyGroup
            {
                GroupName = "ТЕ-23",
                Course = 7, // Курс не может быть больше 6
                Speciality = "Тестовая специальность",
                FormationYear = 2023
            };

            // Act
            _dbService.AddStudyGroup(invalidGroup);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddStudyGroup_WithInvalidFormationYear_ShouldThrowException()
        {
            // Arrange
            var invalidGroup = new DatabaseService.StudyGroup
            {
                GroupName = "ТЕ-23",
                Course = 1,
                Speciality = "Тестовая специальность",
                FormationYear = 1800 // Год не может быть меньше 1900
            };

            // Act
            _dbService.AddStudyGroup(invalidGroup);
        }
    }
} 