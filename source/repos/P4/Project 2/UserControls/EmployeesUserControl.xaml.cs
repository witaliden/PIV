using Project_2.dao;
using Project_2.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_2.UserControls
{
    /// <summary>
    /// Interaction logic for EmployeesUserControl.xaml
    /// </summary>
    public partial class EmployeesUserControl : UserControl
    {
        CrDbContext context = new CrDbContext();
        GetQueries getQueries = new GetQueries();
        PostQueries postQueries = new PostQueries();
        ICollection<Employee>? employees = new List<Employee>();
        public EmployeesUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wyszukiwanie pracowników według wypełnionych filtrów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            employees = EmployeeHasDlSearchToggleBtn.IsChecked == true ? getQueries.GetEmployeesWithDriverLicense(context) : getQueries.GetAllEmployees(context);
            employees = EmployeeSearchId.Text.Length > 0 ? employees.Where(x => x.EmployeeID.ToString().Contains(EmployeeSearchId.Text)).ToList() : employees;
            employees = EmployeeSearchFirstName.Text.Length > 0 ? employees.Where(x => x.FirstName.Contains(EmployeeSearchFirstName.Text)).ToList() : employees;
            employees = EmployeeSearchLastName.Text.Length > 0 ? employees.Where(x => x.LastName.Contains(EmployeeSearchLastName.Text)).ToList() : employees;
            employees = EmployeeSearchPESEL.Text.Length > 0 ? employees.Where(x => x.Pesel.ToString().Contains(EmployeeSearchPESEL.Text)).ToList() : employees;
            employees = EmployeeSearchGender.Text.Length > 0 ? employees.Where(x => x.Gender.Contains(EmployeeSearchGender.Text)).ToList() : employees;
            employees = EmployeeSearchJobTitle.Text.Length > 0 ? employees.Where(x => x.JobTitle.Contains(EmployeeSearchJobTitle.Text)).ToList() : employees;
            employees = EmployeeSearchCity.Text.Length > 0 ? employees.Where(x => x.City.Contains(EmployeeSearchCity.Text)).ToList() : employees;
            employees = EmployeeSearchStreet.Text.Length > 0 ? employees.Where(x => x.Street.Contains(EmployeeSearchStreet.Text)).ToList() : employees;
            employees = EmployeeSearchPhone.Text.Length > 0 ? employees.Where(x => x.Phone.Contains(EmployeeSearchPhone.Text)).ToList() : employees;

            SearchEmployeeListView.ItemsSource = employees != null ? employees : null;
        }

        // <summary>
        /// Podwójny klik kopije dane do formularza (textblock in groupbox), gdzie dane mogą być zedytowane i zapisane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchEmployeeListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var SelectedEmployee = (Employee)SearchEmployeeListView.SelectedItem;
            newEmployeeIDTextBox.Text = SelectedEmployee.EmployeeID.ToString();
            newEmployeeFirstNameTextBox.Text = SelectedEmployee.FirstName;
            newEmployeeLastNameTextBox.Text = SelectedEmployee.LastName;
            newEmployeePeselTextBox.Text = SelectedEmployee.Pesel.ToString();
            newEmployeeGenderTextBox.Text = SelectedEmployee.Gender;
            newEmployeeJobTitleTextBox.Text = SelectedEmployee.JobTitle;
            newEmployeeCityTextBox.Text = SelectedEmployee.City;
            newEmployeeStreetTextBox.Text = SelectedEmployee.Street;
            newEmployeePhoneTextBox.Text = SelectedEmployee.Phone;
            newEmployeeDlCategory1TextBox.Text = SelectedEmployee.EmployeeDriverLicense.Category1 != null ? SelectedEmployee.EmployeeDriverLicense.Category1 : "empty";
            newEmployeeDlCategory2TextBox.Text = SelectedEmployee.EmployeeDriverLicense.Category2 != null ? SelectedEmployee.EmployeeDriverLicense.Category2 : "empty";
            newEmployeeDlDescriptionTextBox.Text = SelectedEmployee.EmployeeDriverLicense.Description! != null ? SelectedEmployee.EmployeeDriverLicense.Description : "0";
        }

        public void EmployeeSaveButton_Click(object sender, RoutedEventArgs e)
        {
            postQueries.AddEmployee(context, int.Parse(newEmployeeIDTextBox.Text), new Employee()
            {
                FirstName = newEmployeeFirstNameTextBox.Text,
                LastName = newEmployeeLastNameTextBox.Text,
                Pesel = Int64.Parse(newEmployeePeselTextBox.Text),
                JobTitle = newEmployeeJobTitleTextBox.Text,
                //employeeDlId = Int32.Parse(newEmployeeDL_IDTextBox.Text),
                Gender = newEmployeeGenderTextBox.Text,
                City = newEmployeeCityTextBox.Text,
                Street = newEmployeeStreetTextBox.Text,
                Phone = newEmployeePhoneTextBox.Text,
                EmployeeDriverLicense = new DriverLicense()
                {
                    Category1 = newEmployeeDlCategory1TextBox.Text,
                    Category2 = newEmployeeDlCategory2TextBox.Text,
                    Description = newEmployeeDlDescriptionTextBox.Text
                }
            });

        }
    }
}
