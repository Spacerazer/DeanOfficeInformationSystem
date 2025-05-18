using System;
using System.Collections.Generic;
using System.Windows;

namespace DeanOfficeInformationSystem
{
    public partial class AddEditStudyGroupWindow : Window
    {
        public DatabaseService.StudyGroup StudyGroup { get; private set; }
        private bool isEditing = false;
        private DatabaseService dbService;

        public AddEditStudyGroupWindow()
        {
            InitializeComponent();
            StudyGroup = new DatabaseService.StudyGroup();
            windowTitle.Text = "Добавление группы";
            Title = "Добавление группы";
            cmbCourse.SelectedIndex = 0;
            LoadHeadmen();
        }

        public AddEditStudyGroupWindow(DatabaseService.StudyGroup group)
        {
            InitializeComponent();
            StudyGroup = new DatabaseService.StudyGroup
            {
                Id = group.Id,
                GroupName = group.GroupName,
                Course = group.Course,
                Speciality = group.Speciality,
                FormationYear = group.FormationYear,
                HeadmanId = group.HeadmanId
            };

            txtGroupName.Text = StudyGroup.GroupName;
            cmbCourse.SelectedIndex = StudyGroup.Course - 1;
            txtSpeciality.Text = StudyGroup.Speciality;
            txtFormationYear.Text = StudyGroup.FormationYear.ToString();
            
            LoadHeadmen();
            if (StudyGroup.HeadmanId > 0)
            {
                cmbHeadman.SelectedValue = StudyGroup.HeadmanId;
            }

            windowTitle.Text = "Редактирование группы";
            Title = "Редактирование группы";
            isEditing = true;
        }

        private void LoadHeadmen()
        {
            dbService = new DatabaseService();
            var students = dbService.GetAvailableHeadmen();
            cmbHeadman.ItemsSource = students;
            cmbHeadman.DisplayMemberPath = "LastName";
            cmbHeadman.SelectedValuePath = "Id";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtGroupName.Text) ||
                string.IsNullOrWhiteSpace(txtSpeciality.Text) ||
                string.IsNullOrWhiteSpace(txtFormationYear.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля (Название группы, Специальность, Год формирования).",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtFormationYear.Text, out int formationYear))
            {
                MessageBox.Show("Год формирования должен быть числом.",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (formationYear < 1900 || formationYear > DateTime.Now.Year)
            {
                MessageBox.Show($"Год формирования должен быть между 1900 и {DateTime.Now.Year}.",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            StudyGroup.GroupName = txtGroupName.Text.Trim();
            StudyGroup.Course = cmbCourse.SelectedIndex + 1;
            StudyGroup.Speciality = txtSpeciality.Text.Trim();
            StudyGroup.FormationYear = formationYear;
            StudyGroup.HeadmanId = cmbHeadman.SelectedValue != null ? (int)cmbHeadman.SelectedValue : 0;

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