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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Курсач.Classes;
using Курсач.Model;

namespace Курсач.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            EnterData enterData = DB.entities.EnterData.FirstOrDefault(c => c.Login == LoginBox.Text);
            if (enterData != null)
            {
                string hashedPassword = enterData.Password;
                bool check = Cryptography.VerifyHashedPassword(hashedPassword, PasswordBox.Password);
                if (check)
                {
                    LoginBox.Text = "";
                    Worker worker = DB.entities.Worker.FirstOrDefault(c => c.Login == enterData.Login);
                    Properties.Settings.Default.WorkerId = worker.WorkerId;
                    if (worker.Work == false)
                    {
                        MessageBox.Show("Вы уволены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (worker.Post.PostName == "Менеджер")
                        NavigationService.Navigate(new AccountantPage());
                    else if (worker.Post.PostName == "Бухгалтер")
                    {
                        NavigationService.Navigate(new TrueAccountantPage());
                    }
                    else
                        NavigationService.Navigate(new DoctorPage());
                }
                return;
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!", "Внимание!",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
