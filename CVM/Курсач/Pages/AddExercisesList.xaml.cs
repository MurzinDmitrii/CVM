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
    /// Логика взаимодействия для AddExercisesList.xaml
    /// </summary>
    public partial class AddExercisesList : Page
    {
        ExercisesList exercisesList;
        int clId;
        public AddExercisesList(ExercisesList exercisesList)
        {
            InitializeComponent();
            clId = exercisesList.ClientId;
            this.DataContext = exercisesList;
            DB.entities.DeleteExercisesList(exercisesList.ClientId, exercisesList.ExercisesId);
            ExercisesBox.ItemsSource = DB.entities.Exercises.ToList();
        }
        public AddExercisesList(Client client)
        {
            InitializeComponent();
            clId = client.ClientId;
            exercisesList = new ExercisesList();
            exercisesList.Client = client;
            ExercisesBox.ItemsSource = DB.entities.Exercises.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Exercises ex = ExercisesBox.SelectedItem as Exercises;
                int quantity = int.Parse(QuantityBox.Text);
                DB.entities.AddExercisesList(clId, ex.ExercisesId, quantity);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Заполните все данные корректно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
