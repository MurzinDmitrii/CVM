using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        Client cl;
        Guardian guardian;
        public AddClientPage()
        {
            cl = new Client();
            cl.ClientRh_ = false;
            cl.ClientGender = false;
            cl.Passport = new Passport();
            cl.Polis = new Polis();
            this.DataContext = cl;
            InitializeComponent();
            FamilyBox.ItemsSource = DB.entities.FamilyPosition.ToList();
            EducationBox.ItemsSource = DB.entities.Education.ToList();
            BusyBox.ItemsSource = DB.entities.Busyness.ToList();
            InsuranceBox.ItemsSource = DB.entities.Insurance.ToList();
            List<string> ListGroupBlood = new List<string>();
            for (int i = 1; i <= 4; i++)
            {
                ListGroupBlood.Add(i.ToString());
            }
            GroupBloodBox.ItemsSource = ListGroupBlood;
            ImageBox.DataContext = File.ReadAllBytes(@"photo.jpg");
            cl.Photo = File.ReadAllBytes(@"photo.jpg");
            PrivilegesBox.ItemsSource = DB.entities.BenefitsCategory.ToList();
        }

        public AddClientPage(Model.Application application)
        {
            cl = application.Document.Client;
            this.DataContext = cl;
            InitializeComponent();
            guardian = DB.entities.Guardian.FirstOrDefault(c => c.ClientId == cl.ClientId);
            if (guardian != null)
            {
                GuardianBox.DataContext = guardian;
                GuarCheckBox.IsChecked = true;
            }
            FamilyBox.ItemsSource = DB.entities.FamilyPosition.ToList();
            EducationBox.ItemsSource = DB.entities.Education.ToList();
            BusyBox.ItemsSource = DB.entities.Busyness.ToList();
            InsuranceBox.ItemsSource = DB.entities.Insurance.ToList();
            List<string> ListGroupBlood = new List<string>();
            for (int i = 1; i <= 4; i++)
            {
                ListGroupBlood.Add(i.ToString());
            }
            GroupBloodBox.ItemsSource = ListGroupBlood;
            PrivilegesBox.ItemsSource = DB.entities.BenefitsCategory.ToList();
            PassportSeryBox.Text = System.Text.Encoding.UTF8.GetString(Cryptography.DecryptData(cl.Passport.PassportSeria));
            PassportNumberBox.Text = System.Text.Encoding.UTF8.GetString(Cryptography.DecryptData(cl.Passport.PassportNumber));
            ImageBox.DataContext = cl.Photo;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenderBox.IsChecked == true)
            {
                cl.ClientGender = true;
            }
            else
            {
                cl.ClientGender = false;
            }
            if (RHBox.IsChecked == true)
            {
                cl.ClientRh_ = true;
            }
            else
            {
                cl.ClientRh_ = false;
            }
            cl.ClientBloodGroup = GroupBloodBox.SelectedItem.ToString();
            cl.ClientHB = HBBox.SelectedDate;
            if(cl.Passport == null)cl.Passport = new Passport();
            cl.Passport.PassportDate = (DateTime)PassportDateBox.SelectedDate;
            cl.Passport.PassportSeria = Cryptography.EncryptData(Encoding.UTF8.GetBytes(PassportSeryBox.Text));
            cl.Passport.PassportNumber = Cryptography.EncryptData(Encoding.UTF8.GetBytes(PassportNumberBox.Text));
            if (cl.ClientId == 0)DB.entities.Client.Add(cl);
            if (!string.IsNullOrEmpty(PrivilegesSeryBox.Text)&&DB.entities.Benefits.FirstOrDefault(c => c.ClientId == cl.ClientId)==null)
            {
                cl.Benefits = new Benefits();
                BenefitsCategory bc = (BenefitsCategory)PrivilegesBox.SelectedItem;
                cl.Benefits.BCId = bc.BCId;
                cl.Benefits.BemnefitsDocSeries = PrivilegesSeryBox.Text;
                cl.Benefits.BenefitsDocNumber = PrivilegesNumberBox.Text;
            }
            if (guardian != null && guardian.GuardianId == 0 && cl.ClientId == 0)
            {
                guardian.GuardianPassport = new GuardianPassport();
                var GuarList = DB.entities.Client.ToList();
                guardian.ClientId = GuarList.Count+1;
                Guardian guar = DB.entities.Guardian.FirstOrDefault(x => x.ClientId == guardian.ClientId);
                DB.entities.AddGuardian
                    (guardian.GuardianFullName, guardian.ClientId, guardian.GuardianAddress, guardian.GuardianPhone);
                DB.entities.AddGuardianPassport(GuardianPassportSeryBox.Text, GuardianPassportNumberBox.Text,
                    GuardianAddresBox.Text, GuardianPassportDateBox.SelectedDate,
                    guardian.ClientId, guar.GuardianId);
            }
            if (guardian != null && guardian.GuardianId == 0)
            {
                guardian.GuardianPassport = new GuardianPassport();
                guardian.ClientId = cl.ClientId;
                DB.entities.AddGuardian
                    (guardian.GuardianFullName, guardian.ClientId, guardian.GuardianAddress, guardian.GuardianPhone);
                Guardian guar = DB.entities.Guardian.FirstOrDefault(x => x.ClientId == guardian.ClientId);
                DB.entities.AddGuardianPassport(GuardianPassportSeryBox.Text, GuardianPassportNumberBox.Text,
                    GuardianAddresBox.Text, GuardianPassportDateBox.SelectedDate,
                    guardian.ClientId, guar.GuardianId);
            }
            try
            {
                DB.entities.SaveChanges();
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Заполните все данные корректно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            GuardianBox.Visibility = Visibility.Visible;
            if(guardian == null)
            {
                guardian = new Guardian();
                GuardianBox.DataContext = guardian;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            GuardianBox.Visibility = Visibility.Collapsed;
        }

        private void ImageBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            bool? t = openFileDialog.ShowDialog();
            if (t == true)
            {
                cl.Photo = File.ReadAllBytes(openFileDialog.FileName);
                ImageBox.DataContext = cl.Photo;
            }
        }

        private void Grid_PreviewDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files[0].Split('.').Last() == "jpg" || files[0].Split('.').Last() == "png" || files[0].Split('.').Last() == "gif" || files[0].Split('.').Last() == "jpeg")
                {
                    cl.Photo = File.ReadAllBytes(files[0]);
                }
                ImageBox.DataContext = cl.Photo;
            }
        }
    }
}
