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
        private readonly string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=DeanOfficeDB;Integrated Security=True";

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
    }
}
