using Aspose.Words.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Microsoft.Win32;
using System.Windows;

namespace Курсач.Classes
{
    public class EmailService
    {
        public static void Send(string mail)
        {
            MailAddress from = new MailAddress("Dmitriymyrzin0908@yandex.ru", "Центр восстановительной медицины");
            MailAddress to = new MailAddress(mail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Медицинская карта";
            m.Body = "Здравствуйте, вот Ваша медицинская карта";
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential("Dmitriymyrzin0908", "Dima005dimon");
            smtp.EnableSsl = true;
            string file = "";
            OpenFileDialog fileDialog = new OpenFileDialog() { Filter = "Файлы Word (*.docx)|*.docx" };
            var g = fileDialog.ShowDialog();
            if ((bool)g)
            {
                file = fileDialog.FileName;
                Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);
                System.Net.Mime.ContentDisposition disposition = attach.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                m.Attachments.Add(attach);
                smtp.Send(m);
                MessageBox.Show("Успешно отправлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите файл!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
