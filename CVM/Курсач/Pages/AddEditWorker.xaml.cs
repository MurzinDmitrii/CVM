using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Логика взаимодействия для AddEditWorker.xaml
    /// </summary>
    public partial class AddEditWorker : Page
    {
        Worker worker;
        public AddEditWorker()
        {
            InitializeComponent();
            worker = new Worker();
            this.DataContext = worker;
            PostBox.ItemsSource = DB.entities.Post.ToList();
        }

        public AddEditWorker(Worker worker)
        {
            InitializeComponent();
            this.worker = worker;
            this.DataContext = worker;
            PostBox.ItemsSource = DB.entities.Post.ToList();
            LoginBox.Text = worker.Login;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (worker.Login == null)
                {
                    EnterData enterData = new EnterData()
                    {
                        Login = LoginBox.Text,
                        Password = Cryptography.HashPassword(PassBox.Text)
                    };
                    DB.entities.EnterData.Add(enterData);
                }
                else
                {
                    EnterData enterData = DB.entities.EnterData.FirstOrDefault(c => c.Login == LoginBox.Text);
                    if (enterData != null && worker.Login == LoginBox.Text)
                    {
                        enterData.Password = Cryptography.HashPassword(PassBox.Text);
                    }
                    else
                    {
                        EnterData delEnterData = DB.entities.EnterData.FirstOrDefault(c => c.Login == worker.Login);
                        DB.entities.EnterData.Remove(delEnterData);
                        EnterData newEnterData = new EnterData()
                        {
                            Login = LoginBox.Text,
                            Password = Cryptography.HashPassword(PassBox.Text)
                        };
                        DB.entities.EnterData.Add(newEnterData);
                        enterData = newEnterData;
                    }
                    worker.EnterData = enterData;
                }
                worker.Login = LoginBox.Text;
                if (worker.WorkerId == 0)
                {
                    worker.Work = true;
                    DB.entities.Worker.Add(worker);
                }
                DB.entities.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Заполните все данные корректно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
