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
    /// Логика взаимодействия для AddApplication.xaml
    /// </summary>
    public partial class AddApplication : Page
    {
        public AddApplication()
        {
            InitializeComponent();
            ServiceBox.ItemsSource = DB.entities.Service.ToList();
            PayWayBox.ItemsSource = DB.entities.PayWay.ToList();
            ClientBox.ItemsSource = DB.entities.Client.ToList();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = ClientBox.SelectedItem as Client;
                DateTime newDate = new DateTime();
                Document document = DB.entities.Document.FirstOrDefault(c => c.ClientId == client.ClientId);
                if (document == null)
                {
                    DB.entities.AddDocument(DateTime.Now, client.ClientId, Properties.Settings.Default.WorkerId);
                    document = DB.entities.Document.FirstOrDefault(c => c.ClientId == client.ClientId) as Document;
                }
                Service service = ServiceBox.SelectedItem as Service;
                DateTime applicationDate = DateBox.SelectedDate ?? DateTime.Now;
                if(service.ServiceName == "Консультация у врача")
                {
                    if (applicationDate.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("На этот день записей больше нет!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    var applicationList = DB.entities.Application.ToList();
                    bool t = false;
                    for (int i = DateTime.Now.Hour + 1; i < 20; i++)
                    {
                        t = false;
                        foreach (var item in applicationList)
                        {
                            if (item.ApplicationDate.Hour == i
                                && item.ApplicationDate.Date == DateBox.SelectedDate && item.Service.ServiceName == "Консультация у врача")
                            {
                                t = true;
                                break;
                            }
                        }
                        if (!t)
                        {
                            newDate = applicationDate.AddHours(i);
                            break;
                        }
                    }
                    if (newDate.Hour == 0)
                    {
                        MessageBox.Show("На этот день записей больше нет!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    newDate = DateBox.SelectedDate ?? DateTime.Now;
                }
                PayWay payWay = PayWayBox.SelectedItem as PayWay;
                DB.entities.AddApplication(document.DocumentId, document.DocumentDate, service.ServiceId,
                    newDate, payWay.PayWayId);  
            }
            catch
            {
                MessageBox.Show("Корректно заполните поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            NavigationService.GoBack();
        }
    }
}
