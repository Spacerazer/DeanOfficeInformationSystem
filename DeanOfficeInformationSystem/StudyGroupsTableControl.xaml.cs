using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace DeanOfficeInformationSystem
{
    public partial class StudyGroupsTableControl : UserControl
    {
        private List<DatabaseService.StudyGroup> allGroups;
        public DatabaseService.StudyGroup SelectedGroup { get; private set; }

        public StudyGroupsTableControl()
        {
            InitializeComponent();
            courseFilter.SelectedIndex = 0;
        }

        public void LoadData(List<DatabaseService.StudyGroup> groups)
        {
            allGroups = groups;
            UpdateSpecialityFilter();
            ApplyFilters();
        }

        private void UpdateSpecialityFilter()
        {
            var specialities = allGroups.Select(g => g.Speciality).Distinct().OrderBy(s => s).ToList();
            specialityFilter.Items.Clear();
            specialityFilter.Items.Add("Все");
            foreach (var speciality in specialities)
            {
                specialityFilter.Items.Add(speciality);
            }
            specialityFilter.SelectedIndex = 0;
        }

        private void ApplyFilters()
        {
            var filteredGroups = allGroups;

            // Применяем фильтр по курсу
            if (courseFilter.SelectedIndex > 0)
            {
                int selectedCourse = courseFilter.SelectedIndex;
                filteredGroups = filteredGroups.Where(g => g.Course == selectedCourse).ToList();
            }

            // Применяем фильтр по специальности
            if (specialityFilter.SelectedIndex > 0)
            {
                string selectedSpeciality = specialityFilter.SelectedItem.ToString();
                filteredGroups = filteredGroups.Where(g => g.Speciality == selectedSpeciality).ToList();
            }

            groupsDataGrid.ItemsSource = filteredGroups;
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (allGroups != null)
            {
                ApplyFilters();
            }
        }

        private void groupsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedGroup = groupsDataGrid.SelectedItem as DatabaseService.StudyGroup;
        }
    }
} 