using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeanOfficeInformationSystem
{
    public partial class MainWindow : Window
    {
        private StudentsTableControl studentsControl;
        private EmployeesTableControl employeesControl;
        private DatabaseService dbService;

        public MainWindow()
        {
            InitializeComponent();

            // Инициализация сервиса для работы с БД
            dbService = new DatabaseService();

            // Инициализация UserControl'ов для таблиц
            studentsControl = new StudentsTableControl();
            employeesControl = new EmployeesTableControl();

            // По умолчанию показываем таблицу студентов
            tableContentControl.Content = studentsControl;
            tableTitle.Text = "Студенты";
            LoadStudentsData();
        }

        private void btnStudents_Click(object sender, RoutedEventArgs e)
        {
            tableContentControl.Content = studentsControl;
            tableTitle.Text = "Студенты";
            LoadStudentsData();
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            tableContentControl.Content = employeesControl;
            tableTitle.Text = "Сотрудники";
            LoadEmployeesData();
        }

        private void LoadStudentsData()
        {
            try
            {
                studentsControl.LoadData(dbService.GetAllStudents());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных студентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadEmployeesData()
        {
            try
            {
                employeesControl.LoadData(dbService.GetAllEmployees());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных сотрудников: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tableContentControl.Content == studentsControl)
            {
                var addStudentWindow = new AddEditStudentWindow();
                if (addStudentWindow.ShowDialog() == true)
                {
                    dbService.AddStudent(addStudentWindow.Student);
                    LoadStudentsData();
                }
            }
            else if (tableContentControl.Content == employeesControl)
            {
                var addEmployeeWindow = new AddEditEmployeeWindow();
                if (addEmployeeWindow.ShowDialog() == true)
                {
                    dbService.AddEmployee(addEmployeeWindow.Employee);
                    LoadEmployeesData();
                }
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (tableContentControl.Content == studentsControl)
            {
                var selectedStudent = studentsControl.SelectedStudent;
                if (selectedStudent != null)
                {
                    var editStudentWindow = new AddEditStudentWindow(selectedStudent);
                    if (editStudentWindow.ShowDialog() == true)
                    {
                        dbService.UpdateStudent(editStudentWindow.Student);
                        LoadStudentsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите студента для редактирования", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (tableContentControl.Content == employeesControl)
            {
                var selectedEmployee = employeesControl.SelectedEmployee;
                if (selectedEmployee != null)
                {
                    var editEmployeeWindow = new AddEditEmployeeWindow(selectedEmployee);
                    if (editEmployeeWindow.ShowDialog() == true)
                    {
                        dbService.UpdateEmployee(editEmployeeWindow.Employee);
                        LoadEmployeesData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите сотрудника для редактирования", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (tableContentControl.Content == studentsControl)
            {
                var selectedStudent = studentsControl.SelectedStudent;
                if (selectedStudent != null)
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить студента {selectedStudent.LastName} {selectedStudent.FirstName}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        dbService.DeleteStudent(selectedStudent.Id);
                        LoadStudentsData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите студента для удаления", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (tableContentControl.Content == employeesControl)
            {
                var selectedEmployee = employeesControl.SelectedEmployee;
                if (selectedEmployee != null)
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить сотрудника {selectedEmployee.LastName} {selectedEmployee.FirstName}?",
                        "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        dbService.DeleteEmployee(selectedEmployee.Id);
                        LoadEmployeesData();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите сотрудника для удаления", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (tableContentControl.Content == studentsControl)
            {
                LoadStudentsData();
            }
            else if (tableContentControl.Content == employeesControl)
            {
                LoadEmployeesData();
            }
        }
    }
}