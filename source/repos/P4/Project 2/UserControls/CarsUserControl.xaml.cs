using Project_2.dao;
using Project_2.model;
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

namespace Project_2.UserControls
{
    /// <summary>
    /// Interaction logic for CarsUserControl.xaml
    /// </summary>
    public partial class CarsUserControl : UserControl
    {

        CrDbContext context = new CrDbContext();
        GetQueries getQueries = new GetQueries();
        PostQueries postQueries = new PostQueries();
        public CarsUserControl()
        {
            InitializeComponent();
        }

        private void AddComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SearchCarListView.Items.Clear();
            /*            string keyWord = firstNameTextBox.Text;
                        if (keyWord is null)
                        {
                            EmployeesSearchListView.Items.Add("brak wyników");
                        }
                        var employeeList = getQueries.GetEmployeeByLastname(context, keyWord);
                        EmployeesSearchListView.ItemsSource = employeeList;*/
        }

        private void SearchCarsBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Car> cars;
            if (CarSearchAvailabilityToggleBtn.IsChecked == true)
            {
                cars = getQueries.GetAvailableCars(context);
                SearchCarListView.Items.Clear();
                if (cars != null)
                {
                    SearchCarListView.ItemsSource = cars;
                }
            }
            cars = getQueries.GetAllCars(context)!;
            SearchCarListView.Items.Clear();
            if (cars != null)
            {
                SearchCarListView.ItemsSource = cars;
            }
        }
        private void CarListViewHandleDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var SelectedCar = (Car)SearchCarListView.SelectedItem;
            newCarVINTextBox.Text = SelectedCar.VIN;
            newCarBrandTextBox.Text = SelectedCar.Brand;
            newCarModelTextBox.Text = SelectedCar.Model;
            newCarRegNumTextBox.Text = SelectedCar.RegNum;
            newCarEngCapTextBox.Text = SelectedCar.EngineCapacity.ToString();
        }
    }
}
