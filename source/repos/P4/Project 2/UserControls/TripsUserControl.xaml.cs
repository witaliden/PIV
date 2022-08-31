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
    /// Interaction logic for TripsUserControl.xaml
    /// </summary>
    public partial class TripsUserControl : UserControl
    {
        CrDbContext context = new CrDbContext();
        GetQueries getQueries = new GetQueries();
        PostQueries postQueries = new PostQueries();
        List<Trip>? trips = new List<Trip>();
        public TripsUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Wyszukiwanie samomochodów według wypełnionych filtrów
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchTripsBtn_Click(object sender, RoutedEventArgs e)
        {
            trips = TripActiveToggleBtn.IsChecked == true ? getQueries.GetActiveTrips(context) : getQueries.GetAllTrips(context);
            trips = TripIdSearchTextBox.Text.Length > 0 ? trips.Where(x => x.TripID.ToString().Contains(TripIdSearchTextBox.Text)).ToList() : trips;
            trips = TripVINSearchTextBox.Text.Length > 0 ? trips.Where(x => x.VIN.Contains(TripVINSearchTextBox.Text)).ToList() : trips;
            trips = TripEmployeeIdSearchTextBox.Text.Length > 0 ? trips.Where(x => x.EmployeeID.ToString().Contains(TripEmployeeIdSearchTextBox.Text)).ToList() : trips;
            trips = TripPurposeSearchTextBox.Text.Length > 0 ? trips.Where(x => x.TripPurpose.ToString().Contains(TripPurposeSearchTextBox.Text)).ToList() : trips;
            trips = TripTakeDateTimeSearchTextBox.Text.Length > 0 ? trips.Where(x => x.TakeDateTime.Equals(TripTakeDateTimeSearchTextBox.Text)).ToList() : trips;
            trips = TripReturnDateTimeSearchTextBox.Text.Length > 0 ? trips.Where(x => x.ReturnDateTime.Equals(TripReturnDateTimeSearchTextBox.Text)).ToList() : trips;

            SearchTripListView.ItemsSource = trips != null ? trips : null;
        }

        /// <summary>
        /// Podwójny klik kopije dane do formularza (textblock in groupbox), gdzie dane mogą być zedytowane i zapisane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TripListViewHandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var SelectedTrip = (Trip)SearchTripListView.SelectedItem;
            newTripIdTextBox.Text = SelectedTrip.TripID.ToString();
            newTripCarTextBox.Text = SelectedTrip.VIN;
            newTripEmployeeIdTextBox.Text = SelectedTrip.EmployeeID.ToString();
            newTripPurposeTextBox.Text = SelectedTrip.TripPurpose;
            newTripCounterBeforeTextBox.Text = SelectedTrip.CounterBefore.ToString();
        }

        /// <summary>
        /// Po wciśnienciu przyciska "Zapisz" zostaje wywołana metoda, która najpierw sprawdza czy samochód z takim VIN istnieje w bazie, w zależności od czego wykonuje się Update lub Create.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TripSaveButton_Click(object sender, RoutedEventArgs e)
        {
            postQueries.startTrip(context, Int32.Parse(newTripIdTextBox.Text), new Trip()
            {
                VIN = newTripCarTextBox.Text,
                EmployeeID = Int32.Parse(newTripEmployeeIdTextBox.Text),
                TripPurpose = newTripPurposeTextBox.Text,
                TakeDateTime = DateTime.Now,
                ReturnDateTime = null,
                CounterBefore = Int32.Parse(newTripCounterBeforeTextBox.Text),
                CounterAfter = 0
            });
            MessageBox.Show("Udanego wyjazdu!");
        }

        private void QuickTripFinish(object sender, RoutedEventArgs e)
        {
            if (Int32.Parse(TripEndCounterAfterTextBox.Text) < getQueries.GetTripById(context, Int32.Parse(TripEndIdTextBox.Text)).CounterBefore){
                MessageBox.Show("Stan licznika jest mniejszy niż przed wyjazdem.");
            } else
            {
                postQueries.quickTripFinish(context, Int32.Parse(TripEndIdTextBox.Text), Int32.Parse(TripEndCounterAfterTextBox.Text));
                MessageBox.Show("Witamy z powrotem!");
            }
        }
    }
}
