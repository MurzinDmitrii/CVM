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
using Курсач.Classes;
using Курсач.Model;

namespace Курсач.Pages
{
    /// <summary>
    /// Логика взаимодействия для TrueAccountantPage.xaml
    /// </summary>
    public partial class TrueAccountantPage : Page
    {
        public TrueAccountantPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            var WorkerList = DB.entities.Worker.Where(c => c.Work == true).ToList();
            if(!string.IsNullOrEmpty(SearchBox.Text) )
            {
                WorkerList = WorkerList.Where(c => c.FullName.ToLower().Contains(SearchBox.Text.ToLower())).ToList();
            }
            WorkerListView.ItemsSource = WorkerList;
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditWorker());
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            Worker selectedItem = menu.DataContext as Worker;
            NavigationService.Navigate(new AddEditWorker(selectedItem));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if(MessageBoxResult.Yes == MessageBox.Show("Действительно уволить?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                MenuItem menu = sender as MenuItem;
                Worker selectedItem = menu.DataContext as Worker;
                selectedItem.Work = false;
                DB.entities.SaveChanges();
                Load();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
