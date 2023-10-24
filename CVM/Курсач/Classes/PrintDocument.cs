using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Xceed.Words.NET;
using Курсач.Model;

namespace Курсач.Classes
{
    public class PrintDocument
    {
        public static void OutputDocument(Model.Application application)
        {
            try
            {
                var file = Properties.Resources.Document;
                File.WriteAllBytes("Document.docx", file);
            }
            catch
            {
                MessageBox.Show("Ошибка при открытии шаблона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                Stream stream = new FileStream(System.IO.Path.GetFileName("Document.docx"), FileMode.Open);
                DocX document = DocX.Load(stream);
                Document doc = application.Document;
                document.ReplaceText("{0}", application.DocumentId.ToString()+"/"
                    +application.ApplicationDate.Month+"/"+application.ApplicationDate.Year);
                document.ReplaceText("{1}", doc.DocumentDate.ToShortDateString());
                document.ReplaceText("{2}", doc.Client.FullName);
                document.ReplaceText("{3}", doc.Client.ClientAddress);
                document.ReplaceText("{4}", doc.Client.ClientPhone);
                document.ReplaceText("{5}", doc.Client.Email);
                if(DB.entities.Guardian.FirstOrDefault(c => c.ClientId == doc.Client.ClientId) != null) 
                {
                    Guardian guardian = DB.entities.Guardian.FirstOrDefault(c => c.ClientId == doc.Client.ClientId);
                    document.ReplaceText("{6}", guardian.GuardianFullName);
                    document.ReplaceText("{7}", guardian.GuardianAddress);
                    document.ReplaceText("{8}", guardian.GuardianPassport.PassportSeria);
                    document.ReplaceText("{9}", guardian.GuardianPassport.PassportNumber);
                    document.ReplaceText("{10}", guardian.GuardianPassport.PassportWho);
                }
                else
                {
                    document.ReplaceText("{6}", "");
                    document.ReplaceText("{7}", "");
                    document.ReplaceText("{8}", "");
                    document.ReplaceText("{9}", "");
                    document.ReplaceText("{10}", "");
                }
                document.ReplaceText("{11}", doc.Worker.WorkerLN+" "+doc.Worker.WorkerFN+" "+doc.Worker.WorkerPatronimic);
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
