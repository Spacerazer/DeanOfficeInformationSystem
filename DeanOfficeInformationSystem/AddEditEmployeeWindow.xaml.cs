using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeanOfficeInformationSystem
{
    public partial class AddEditEmployeeWindow : Window
    {
        public DatabaseService.Employee Employee { get; private set; }
        private bool isEditing = false;

        // Конструктор для добавления нового сотрудника
        public AddEditEmployeeWindow()
        {
            InitializeComponent();
            Employee = new DatabaseService.Employee();
            windowTitle.Text = "Добавление сотрудника";
            Title = "Добавление сотрудника";
        }

        // Конструктор для редактирования существующего сотрудника
        public AddEditEmployeeWindow(DatabaseService.Employee employee)
        {
            InitializeComponent();
            Employee = new DatabaseService.Employee
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                Position = employee.Position,
                Department = employee.Department,
                Phone = employee.Phone,
                Email = employee.Email
            };

            txtLastName.Text = Employee.LastName;
            txtFirstName.Text = Employee.FirstName;
            txtMiddleName.Text = Employee.MiddleName;
            txtPosition.Text = Employee.Position;
            txtDepartment.Text = Employee.Department;
            txtPhone.Text = Employee.Phone;
            txtEmail.Text = Employee.Email;

            windowTitle.Text = "Редактирование сотрудника";
            Title = "Редактирование сотрудника";
            isEditing = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtPosition.Text) ||
                string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (Фамилия, Имя, Должность, Кафедра).",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Проверка формата email
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Пожалуйста, введите корректный email.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Обновляем данные сотрудника
            Employee.LastName = txtLastName.Text.Trim();
            Employee.FirstName = txtFirstName.Text.Trim();
            Employee.MiddleName = txtMiddleName.Text.Trim();
            Employee.Position = txtPosition.Text.Trim();
            Employee.Department = txtDepartment.Text.Trim();
            Employee.Phone = txtPhone.Text.Trim();
            Employee.Email = txtEmail.Text.Trim();

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        // Проверка формата email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
