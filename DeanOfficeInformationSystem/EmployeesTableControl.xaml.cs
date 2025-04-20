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
    /// <summary>
    /// Логика взаимодействия для EmployeesTableControl.xaml
    /// </summary>
    public partial class EmployeesTableControl : UserControl
    {
        public DatabaseService.Employee SelectedEmployee { get; private set; }

        public EmployeesTableControl()
        {
            InitializeComponent();
        }

        public void LoadData(List<DatabaseService.Employee> employee)
        {
            employeesDataGrid.ItemsSource = employee;
        }

        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEmployee = employeesDataGrid.SelectedItem as DatabaseService.Employee;
        }
    }
}
