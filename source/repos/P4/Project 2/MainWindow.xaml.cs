using Project_2.dao;
using Project_2.model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Project_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CrDbContext context = new CrDbContext();
        GetQueries getQueries = new GetQueries();
        PostQueries postQueries = new PostQueries();
        public MainWindow()
        {
            context.Database.EnsureCreated();
            postQueries.FillCars(context);
            postQueries.FillEmployees(context);
            InitializeComponent();
        }      
    }
}
