using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Курсач.Model;
using Xceed.Words.NET;
using Dock = Xceed.Document.NET;

namespace Курсач.Classes
{
    public class PrintCheck
    {
        public static void OutputApplication(Model.Application application)
        {
            try
            {
                var file = Properties.Resources.ApplicationDoc;
                File.WriteAllBytes("ApplicationDoc.docx", file);
            }
            catch
            {
                MessageBox.Show("Ошибка при открытии шаблона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                Stream stream = new FileStream(System.IO.Path.GetFileName("ApplicationDoc.docx"), FileMode.Open);
                DocX document = DocX.Load(stream);
                string dateStr = application.ApplicationDate.ToShortDateString();
                document.ReplaceText("{0}", application.DocumentId.ToString());
                document.ReplaceText("{1}", application.DocumentDate.ToShortDateString());
                document.ReplaceText("{2}", dateStr);
                document.ReplaceText("{3}", application.PayWay.PayWayName.ToString());
                document.ReplaceText("{4}", application.Document.Client.FIO.ToString());
                Dock.Table table = document.Tables[1];
                int count = 1;
                decimal sum = 0;
                table.InsertRow();
                table.Rows[count].Cells[0].Paragraphs[0].Append(count.ToString());
                table.Rows[count].Cells[1].Paragraphs[0].Append(application.Service.ServiceName.ToString());
                var PriceList = DB.entities.Price.ToList();
                PriceList = PriceList.OrderByDescending(c => c.PriceId).ToList();
                Price price = PriceList.FirstOrDefault(c => c.ServiceId == application.ServiceId);
                if (price != null)
                {
                    table.Rows[count].Cells[2].Paragraphs[0].Append(price.PriceNormal.ToString());
                }
                else
                {
                    table.Rows[count].Cells[2].Paragraphs[0].Append("");
                }
                table.Rows[count].Cells[3].Paragraphs[0].Append(dateStr);
                SaveFileDialog saveFile = new SaveFileDialog() { Filter = "Файлы Word (*.docx)|*.docx" };
                var g = saveFile.ShowDialog();
                if ((bool)g)
                {
                    document.SaveAs(saveFile.FileName);
                    MessageBox.Show("Успешно сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка при создании файла", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
