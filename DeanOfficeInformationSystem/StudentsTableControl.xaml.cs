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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeanOfficeInformationSystem
{
    public partial class StudentsTableControl : UserControl
    {
        public DatabaseService.Student SelectedStudent { get; private set; }

        public StudentsTableControl()
        {
            InitializeComponent();
        }

        public void LoadData(List<DatabaseService.Student> student)
        {
            studentsDataGrid.ItemsSource = student;
        }

        private void studentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedStudent = studentsDataGrid.SelectedItem as DatabaseService.Student;
        }
    }
}
