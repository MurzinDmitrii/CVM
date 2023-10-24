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
    /// Логика взаимодействия для AddAddCardPage.xaml
    /// </summary>
    public partial class AddAddCardPage : Page
    {
        Card card;
        public AddAddCardPage(Card card)
        {
            InitializeComponent();
            this.card = card;
            DesiaseBox.ItemsSource = DB.entities.Desiase.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Desiase desiase = DesiaseBox.SelectedValue as Desiase;
            if (desiase != null)
            {
                if(DB.entities.CardTablePart.FirstOrDefault
                    (c => c.ID == card.ID && c.Desiase.DesiaseName == desiase.DesiaseName) == null)
                {
                    CardTablePart cardTablePart = new CardTablePart();
                    cardTablePart.ID = card.ID;
                    cardTablePart.DesiaseId = desiase.DesiaseId;
                    cardTablePart.WorkerId = Properties.Settings.Default.WorkerId;
                    DB.entities.CardTablePart.Add(cardTablePart);
                    DB.entities.SaveChanges();
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("У данного пациента уже выписан такой диагноз!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    NavigationService.GoBack();
                }
            }
            else
            {
                MessageBox.Show("Выберите диагноз!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                NavigationService.GoBack();
            }
        }
    }
}
