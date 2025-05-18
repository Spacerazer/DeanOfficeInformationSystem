using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DeanOfficeInformationSystem
{
    public class DatabaseService
    {
        private readonly string connectionString = @"Data Source=DESKTOP-6VIECO7;Initial Catalog=DeanOfficeDB;Integrated Security=True";

        public class Student
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string Group { get; set; }
            public int Course { get; set; }
            public string Speciality { get; set; }
        }

        public class Employee
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string Position { get; set; }
            public string Department { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }

        public class StudyGroup
        {
            public int Id { get; set; }
            public string GroupName { get; set; }
            public int Course { get; set; }
            public string Speciality { get; set; }
            public int FormationYear { get; set; }
            public int HeadmanId { get; set; }
            public string HeadmanFullName { get; set; }
        }

        public List<Student> GetAllStudents()
        {
            var student = new List<Student>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Student", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                LastName = reader["LastName"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"].ToString(),
                                Group = reader["Group"].ToString(),
                                Course = Convert.ToInt32(reader["Course"]),
                                Speciality = reader["Speciality"].ToString()
                            });
                        }
                    }
                }
            }

            return student;
        }

        public List<Employee> GetAllEmployees()
        {
            var employee = new List<Employee>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Employee", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee.Add(new Employee
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                LastName = reader["LastName"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"].ToString(),
                                Position = reader["Position"].ToString(),
                                Department = reader["Department"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }

            return employee;
        }

        public void AddStudent(Student student)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Student (LastName, FirstName, MiddleName, [Group], Course, Speciality) 
                                 VALUES (@LastName, @FirstName, @MiddleName, @Group, @Course, @Speciality)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", student.MiddleName);
                    command.Parameters.AddWithValue("@Group", student.Group);
                    command.Parameters.AddWithValue("@Course", student.Course);
                    command.Parameters.AddWithValue("@Speciality", student.Speciality);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE Student 
                                 SET LastName = @LastName, 
                                     FirstName = @FirstName, 
                                     MiddleName = @MiddleName, 
                                     [Group] = @Group, 
                                     Course = @Course, 
                                     Speciality = @Speciality 
                                 WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", student.Id);
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@FirstName", student.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", student.MiddleName);
                    command.Parameters.AddWithValue("@Group", student.Group);
                    command.Parameters.AddWithValue("@Course", student.Course);
                    command.Parameters.AddWithValue("@Speciality", student.Speciality);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Student WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Employee (LastName, FirstName, MiddleName, Position, Department, Phone, Email) 
                                 VALUES (@LastName, @FirstName, @MiddleName, @Position, @Department, @Phone, @Email)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@Email", employee.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE Employee 
                                 SET LastName = @LastName, 
                                     FirstName = @FirstName, 
                                     MiddleName = @MiddleName, 
                                     Position = @Position, 
                                     Department = @Department, 
                                     Phone = @Phone, 
                                     Email = @Email 
                                 WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@LastName", employee.LastName);
                    command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    command.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                    command.Parameters.AddWithValue("@Position", employee.Position);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@Phone", employee.Phone);
                    command.Parameters.AddWithValue("@Email", employee.Email);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Employee WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<StudyGroup> GetAllStudyGroups()
        {
            var groups = new List<StudyGroup>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(@"
                    SELECT g.*, CONCAT(s.LastName, ' ', s.FirstName, ' ', s.MiddleName) as HeadmanFullName 
                    FROM StudyGroup g 
                    LEFT JOIN Student s ON g.HeadmanId = s.Id", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groups.Add(new StudyGroup
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                GroupName = reader["GroupName"].ToString(),
                                Course = Convert.ToInt32(reader["Course"]),
                                Speciality = reader["Speciality"].ToString(),
                                FormationYear = Convert.ToInt32(reader["FormationYear"]),
                                HeadmanId = reader["HeadmanId"] != DBNull.Value ? Convert.ToInt32(reader["HeadmanId"]) : 0,
                                HeadmanFullName = reader["HeadmanFullName"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }

            return groups;
        }

        public void AddStudyGroup(StudyGroup group)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO StudyGroup (GroupName, Course, Speciality, FormationYear, HeadmanId) 
                               VALUES (@GroupName, @Course, @Speciality, @FormationYear, @HeadmanId)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@GroupName", group.GroupName);
                    command.Parameters.AddWithValue("@Course", group.Course);
                    command.Parameters.AddWithValue("@Speciality", group.Speciality);
                    command.Parameters.AddWithValue("@FormationYear", group.FormationYear);
                    command.Parameters.AddWithValue("@HeadmanId", group.HeadmanId > 0 ? (object)group.HeadmanId : DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudyGroup(StudyGroup group)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE StudyGroup 
                               SET GroupName = @GroupName,
                                   Course = @Course,
                                   Speciality = @Speciality,
                                   FormationYear = @FormationYear,
                                   HeadmanId = @HeadmanId
                               WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", group.Id);
                    command.Parameters.AddWithValue("@GroupName", group.GroupName);
                    command.Parameters.AddWithValue("@Course", group.Course);
                    command.Parameters.AddWithValue("@Speciality", group.Speciality);
                    command.Parameters.AddWithValue("@FormationYear", group.FormationYear);
                    command.Parameters.AddWithValue("@HeadmanId", group.HeadmanId > 0 ? (object)group.HeadmanId : DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudyGroup(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM StudyGroup WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetAvailableHeadmen()
        {
            var students = new List<Student>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("SELECT * FROM Student", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                LastName = reader["LastName"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                MiddleName = reader["MiddleName"].ToString(),
                                Group = reader["Group"].ToString(),
                                Course = Convert.ToInt32(reader["Course"]),
                                Speciality = reader["Speciality"].ToString()
                            });
                        }
                    }
                }
            }

            return students;
        }
    }
}
