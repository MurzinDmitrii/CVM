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
using Курсач.Model;

namespace Курсач.Pages
{
    /// <summary>
    /// Логика взаимодействия для CardPage.xaml
    /// </summary>
    public partial class CardPage : Page
    {
        Client client;
        bool newClient = false;
        Card card;
        public CardPage(Client client)
        {
            InitializeComponent();
            this.client = client;
            List<string> ListGroup = new List<string>();
            card = DB.entities.Card.FirstOrDefault(c => c.ClientId ==  client.ClientId);
            if (card == null)
            {
                card = new Card();
                newClient = true;
                card.ClientLook = false;
            }
            this.DataContext = card;
            for (int i = 1; i <= 4; i++)
            {
                ListGroup.Add(i.ToString());
            }
            GroupBox.ItemsSource = ListGroup;
            DesiaseBox.ItemsSource = DB.entities.Desiase.ToList();
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            card.Client = client;
            card.CardDateEnd = DateEndBox.DisplayDate;
            card.WorkerId = Properties.Settings.Default.WorkerId;
            card.Desiase = DesiaseBox.SelectedValue as Desiase;
            if(newClient)
            {
                card.CardDateStart = DateTime.Now;
                DB.entities.Card.Add(card);
            }
            try
            {
                DB.entities.SaveChanges();
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Корректно заполните поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddAddCardPage(card));
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddExercisesList(card.Client));
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            ExercisesList exercises = item.DataContext as ExercisesList;
            if(MessageBox.Show("Действительно удалить?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                MessageBoxResult.Yes)
            {
                DB.entities.DeleteExercisesList(exercises.ClientId, exercises.ExercisesId);
                Load();
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            ExercisesList exercises = item.DataContext as ExercisesList;
            if(exercises != null)
                NavigationService.Navigate(new AddExercisesList(exercises));
            else
                MessageBox.Show("Выберите поле!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }
        private void Load()
        {
            var ListDopDesiase = DB.entities.CardTablePart.Where(c => c.Card.ClientId == client.ClientId).ToList();
            DopDesiaseListView.ItemsSource = ListDopDesiase;
            var ListExercisesList = DB.entities.ExercisesList.Where(c => c.Client.ClientId == client.ClientId).ToList();
            ExercisesListView.ItemsSource = ListExercisesList;
        }
    }
}
