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
    /// Interaction logic for CarsUserControl.xaml
    /// </summary>
    public partial class CarsUserControl : UserControl
    {
        CrDbContext context = new CrDbContext();
        GetQueries getQueries = new GetQueries();
        PostQueries postQueries = new PostQueries();
        ICollection<Car>? cars = new List<Car>();

        public CarsUserControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Wyszukiwanie samomochodów według wypełnionych filtrów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchCarsBtn_Click(object sender, RoutedEventArgs e)
        {
            cars = CarSearchAvailabilityToggleBtn.IsChecked == true ? getQueries.GetAvailableCars(context) : getQueries.GetAllCars(context);
            cars = CarSearchBrandTextBox.Text.Length > 0 ? cars.Where(x => x.Brand.Contains(CarSearchBrandTextBox.Text)).ToList() : cars;
            cars = CarSearchModelTextBox.Text.Length > 0 ? cars.Where(x => x.Model.Contains(CarSearchModelTextBox.Text)).ToList() : cars;
            cars = CarSearchRegNumTextBox.Text.Length > 0 ?cars.Where(x => x.RegNum.Contains(CarSearchRegNumTextBox.Text)).ToList() : cars;
            cars = CarSearchEngCapTextBox.Text.Length > 0 ?cars.Where(x => x.RegNum.ToString().Contains(CarSearchEngCapTextBox.Text)).ToList() : cars;
            cars = CarSearchVINTextBox.Text.Length > 0 ? cars.Where(x => x.RegNum.Contains(CarSearchVINTextBox.Text)).ToList() : cars;

            SearchCarListView.ItemsSource = cars != null ? cars : null;
        }

        /// <summary>
        /// Podwójny klik kopije dane do formularza (textblock in groupbox), gdzie dane mogą być zedytowane i zapisane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarListViewHandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var SelectedCar = (Car)SearchCarListView.SelectedItem;
            newCarVINTextBox.Text = SelectedCar.VIN;
            newCarBrandTextBox.Text = SelectedCar.Brand;
            newCarModelTextBox.Text = SelectedCar.Model;
            newCarRegNumTextBox.Text = SelectedCar.RegNum;
            newCarEngCapTextBox.Text = SelectedCar.EngineCapacity.ToString();
        }

        /// <summary>
        /// Po wciśnienciu przyciska "Zapisz" zostaje wywołana metoda, która najpierw sprawdza czy samochód z takim VIN istnieje w bazie, w zależności od czego wykonuje się Update lub Create.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarSaveButton_Click(object sender, RoutedEventArgs e)
        {
            postQueries.AddCar(context, new Car()
            {
                VIN = newCarVINTextBox.Text,
                Availability = "Dostępny",
                RegNum = newCarRegNumTextBox.Text,
                Brand = newCarBrandTextBox.Text,
                Model = newCarModelTextBox.Text,
                EngineCapacity = Int16.Parse(newCarEngCapTextBox.Text)
            });
        }
    }
}
