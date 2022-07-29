using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace lab6_login
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<string> styles = new() { "Standard", "Enchanced"};
            comboBox.SelectionChanged += StyleChange;
            comboBox.ItemsSource = styles;
            comboBox.SelectedItem = "Standard";
        }

        private void StyleChange(object sender, SelectionChangedEventArgs e)
        {
            string style = comboBox.SelectedItem as string;
            // Określamy ścieżkę do słownika stylu
            var uri = new Uri(style + ".xaml", UriKind.Relative);
            // Pobieramy słownik
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // Kasujemy bieżący styl aplikacji
            Application.Current.Resources.Clear();
            // Ustawiamy nowy styl
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
