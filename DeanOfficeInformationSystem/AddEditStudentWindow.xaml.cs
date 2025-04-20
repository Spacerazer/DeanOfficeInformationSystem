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
    public partial class AddEditStudentWindow : Window
    {
        public DatabaseService.Student Student { get; private set; }
        private bool isEditing = false;

        // Конструктор для добавления нового студента
        public AddEditStudentWindow()
        {
            InitializeComponent();
            Student = new DatabaseService.Student();
            windowTitle.Text = "Добавление студента";
            Title = "Добавление студента";
            cmbCourse.SelectedIndex = 0;
        }

        // Конструктор для редактирования существующего студента
        public AddEditStudentWindow(DatabaseService.Student student)
        {
            InitializeComponent();
            Student = new DatabaseService.Student
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                Group = student.Group,
                Course = student.Course,
                Speciality = student.Speciality
            };

            txtLastName.Text = Student.LastName;
            txtFirstName.Text = Student.FirstName;
            txtMiddleName.Text = Student.MiddleName;
            txtGroup.Text = Student.Group;
            cmbCourse.SelectedIndex = Student.Course - 1;
            txtSpeciality.Text = Student.Speciality;

            windowTitle.Text = "Редактирование студента";
            Title = "Редактирование студента";
            isEditing = true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtGroup.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (Фамилия, Имя, Группа).",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Обновляем данные студента
            Student.LastName = txtLastName.Text.Trim();
            Student.FirstName = txtFirstName.Text.Trim();
            Student.MiddleName = txtMiddleName.Text.Trim();
            Student.Group = txtGroup.Text.Trim();
            Student.Course = cmbCourse.SelectedIndex + 1;
            Student.Speciality = txtSpeciality.Text.Trim();

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
