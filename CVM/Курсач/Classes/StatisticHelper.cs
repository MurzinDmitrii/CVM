using Aspose.Words.Drawing.Charts;
using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Words.Drawing;
using Курсач.Model;
using Microsoft.Win32;
using System.Windows;

namespace Курсач.Classes
{
    public class StatisticHelper
    {
        public enum Months
        {
            Январь, Февраль, Март, Апрель, Май, Июнь, Июль, Август, Сентябрь, Октябрь, Ноябрь, Декабрь
        }
        public static void StatisticOut()
        {
            Aspose.Words.Document doc = new Aspose.Words.Document();
            DocumentBuilder builder = new DocumentBuilder(doc);
            Shape shape = builder.InsertChart(ChartType.Column, 432, 252);
            Chart chart = shape.Chart;
            chart.Title.Text = "Статистика за квартал";
            ChartSeriesCollection seriesColl = chart.Series;
            Console.WriteLine(seriesColl.Count);
            seriesColl.Clear();
            int date = DateTime.Now.Month - 3;
            string[] categories = new string[] { "Статистика за " + (Months)date++,
                                                 "Статистика за " + (Months)date++, "Статистика за " + (Months)date++,};
            date = DateTime.Now.Month - 2;
            var ServiseList = DB.entities.Service.ToList();
            foreach (var item in ServiseList)
            {
                seriesColl.Add(item.ServiceName, categories, new double[] {
                    DB.entities.Application.Where(c => c.ServiceId == item.ServiceId &&
                    c.ApplicationDate.Month == date).Count(),
                    DB.entities.Application.Where(c => c.ServiceId == item.ServiceId &&
                    c.ApplicationDate.Month == date+1).Count(),
                    DB.entities.Application.Where(c => c.ServiceId == item.ServiceId &&
                    c.ApplicationDate.Month == date+2).Count()});
            }
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog() { Filter = "Файлы Word (*.docx)|*.docx" };
                var g = saveFile.ShowDialog();
                if ((bool)g)
                {
                    doc.Save(saveFile.FileName);
                    MessageBox.Show("Успешно сохранено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch 
            {
                MessageBox.Show("Пожалуйста, закройте документ, в который происходит сохранение!",
                    "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
