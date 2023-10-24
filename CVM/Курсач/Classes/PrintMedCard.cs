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
using Dock = Xceed.Document.NET;
using Курсач.Model;
using static System.Net.Mime.MediaTypeNames;

namespace Курсач.Classes
{
    public class PrintMedCard
    {
        public static void OutputMedCard(Card card)
        {
            AllAboutClientView client = DB.entities.AllAboutClientView.FirstOrDefault(c => c.ClientId == card.ClientId);
            try
            {
                var file = Properties.Resources.MedCardDoc;
                File.WriteAllBytes("MedCardDoc.docx", file);
            }
            catch
            {
                MessageBox.Show("Ошибка при открытии шаблона", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                Stream stream = new FileStream(System.IO.Path.GetFileName("MedCardDoc.docx"), FileMode.Open);
                DocX document = DocX.Load(stream);
                DateTime date = client.ClientHB.GetValueOrDefault(DateTime.Now);
                string dateStr = date.ToShortDateString();
                document.ReplaceText("{0}", card.ID.ToString());
                document.ReplaceText("{1}", card.CardDateStart.ToShortDateString());
                document.ReplaceText("{2}", client.CLientLN + " " + client.ClientFN + " " + client.ClientPatronomic);
                if(client.ClientGender == false)
                {
                    document.ReplaceText("{3}", "Женский");
                }
                else
                {
                    document.ReplaceText("{3}", "Мужской");
                }
                document.ReplaceText("{4}", dateStr);
                document.ReplaceText("{5}", client.ClientAddress.ToString());
                document.ReplaceText("{6}", client.ClientPhone.ToString());
                document.ReplaceText("{7}", client.PolisSeries.ToString());
                document.ReplaceText("{8}", client.PolisNumber.ToString());
                document.ReplaceText("{9}", client.ClientSnils.ToString());
                document.ReplaceText("{10}", client.InsuranceName.ToString());
                Benefits benefits = DB.entities.Benefits.FirstOrDefault(c => c.ClientId == client.ClientId);
                if(benefits != null)
                {
                    document.ReplaceText("{11}", benefits.BenefitsCategory.BCDescription);
                    document.ReplaceText("{12}", benefits.BemnefitsDocSeries);
                    document.ReplaceText("{13}", benefits.BenefitsDocNumber);
                }
                else
                {
                    document.ReplaceText("{11}", "");
                    document.ReplaceText("{12}", "");
                    document.ReplaceText("{13}", "");
                }
                Dock.Table table = document.Tables[12];
                int count = 1;
                table.InsertRow();
                table.Rows[count].Cells[0].Paragraphs[0].Append(card.CardDateStart.ToShortDateString());
                if(card.CardDateEnd != null)
                {
                    DateTime dateEnd = card.CardDateEnd.GetValueOrDefault(DateTime.Now);
                    string dateStrEnd = date.ToShortDateString();
                    table.Rows[count].Cells[1].Paragraphs[0].Append(card.CardDateStart.ToShortDateString());
                }
                else
                {
                    table.Rows[count].Cells[1].Paragraphs[0].Append("");
                }
                table.Rows[count].Cells[2].Paragraphs[0].Append(card.Desiase.DesiaseName);
                table.Rows[count].Cells[3].Paragraphs[0].Append(card.Desiase.DesiaseId);
                table.Rows[count].Cells[4].Paragraphs[0].Append(card.Worker.WorkerLN + " " + card.Worker.WorkerFN[0] +
                    ". " + card.Worker.WorkerPatronimic[0] + ".");
                count++;
                var DopDesiaseList = DB.entities.CardTablePart.Where(c => c.ID == card.ID).ToList();
                foreach( var d in DopDesiaseList)
                {
                    table.InsertRow();
                    table.Rows[count].Cells[0].Paragraphs[0].Append(card.CardDateStart.ToShortDateString());
                    if (card.CardDateEnd != null)
                    {
                        DateTime dateEnd = card.CardDateEnd.GetValueOrDefault(DateTime.Now);
                        string dateStrEnd = date.ToShortDateString();
                        table.Rows[count].Cells[1].Paragraphs[0].Append(card.CardDateStart.ToShortDateString());
                    }
                    else
                    {
                        table.Rows[count].Cells[1].Paragraphs[0].Append("");
                    }
                    table.Rows[count].Cells[2].Paragraphs[0].Append(d.Desiase.DesiaseName);
                    table.Rows[count].Cells[3].Paragraphs[0].Append(d.Desiase.DesiaseId);
                    table.Rows[count].Cells[4].Paragraphs[0].Append(d.Worker.WorkerLN + " " + d.Worker.WorkerFN[0] +
                        ". " + d.Worker.WorkerPatronimic[0] + ".");
                    count++;
                }
                document.ReplaceText("{14}", client.Position.ToString());
                document.ReplaceText("{15}", client.Education.ToString());
                document.ReplaceText("{16}", client.Busyness.ToString());
                var ben = DB.entities.Invalid.FirstOrDefault(c => c.ClientId == card.ClientId);
                if(ben != null)
                {
                    DateTime benDate = ben.InvalidDate.GetValueOrDefault(DateTime.Now);
                    string benDateStr = benDate.ToShortDateString();
                    document.ReplaceText("{17}", ben.First+", "+ben.InvalidGroup.IGName+", "+benDateStr);
                }
                else
                {
                    document.ReplaceText("{17}", "");
                }
                document.ReplaceText("{18}", client.ClientWorkPlaceAndPost.ToString());
                table = document.Tables[13];
                count = 1;
                table.InsertRow();
                table.Rows[count].Cells[0].Paragraphs[0].Append(card.CardDateStart.ToShortDateString());
                table.Rows[count].Cells[1].Paragraphs[0].Append(card.Desiase.DesiaseName);
                table.Rows[count].Cells[2].Paragraphs[0].Append("+");
                table.Rows[count].Cells[3].Paragraphs[0].Append(card.Worker.WorkerLN + " " + card.Worker.WorkerFN[0] +
                    ". " + card.Worker.WorkerPatronimic[0] + ".");
                count++;
                foreach (var d in DopDesiaseList)
                {
                    table.InsertRow();
                    table.Rows[count].Cells[0].Paragraphs[0].Append(card.CardDateStart.ToShortDateString());
                    table.Rows[count].Cells[1].Paragraphs[0].Append(d.Desiase.DesiaseName);
                    table.Rows[count].Cells[2].Paragraphs[0].Append("+");
                    table.Rows[count].Cells[3].Paragraphs[0].Append(d.Worker.WorkerLN + " " + d.Worker.WorkerFN[0] +
                        ". " + d.Worker.WorkerPatronimic[0] + ".");
                    count++;
                }
                document.ReplaceText("{19}", client.ClientBloodGroup.ToString());
                document.ReplaceText("{20}", client.RH);
                document.ReplaceText("{21}", client.Allergy);
                document.ReplaceText("{22}", card.CardDateStart.ToShortDateString());
                document.ReplaceText("{23}", card.Worker.Post.PostName);
                document.ReplaceText("{24}", card.ClientComplaints);
                document.ReplaceText("{25}", card.ClientAnamnesis);
                document.ReplaceText("{26}", card.ClientObjectiveData);
                document.ReplaceText("{27}", card.Desiase.DesiaseName);
                document.ReplaceText("{28}", card.DesiaseId);
                document.ReplaceText("{29}", card.ClientHarders);
                for(int i = 0; i < 3; i++)
                {
                    if (DopDesiaseList.Count != i)
                    {
                        document.ReplaceText("{" + Convert.ToString(30 + i * 2) + "}", DopDesiaseList[i].Desiase.DesiaseName);
                        document.ReplaceText("{" + Convert.ToString(30 + i * 2 + 1) + "}", DopDesiaseList[i].DesiaseId);
                    }
                    else
                    {
                        document.ReplaceText("{" + Convert.ToString(30 + i * 2) + "}", "");
                        document.ReplaceText("{" + Convert.ToString(30 + i * 2 + 1) + "}", "");
                    }
                }
                document.ReplaceText("{36}", card.ClientReason);
                document.ReplaceText("{37}", card.GroopHealth);
                document.ReplaceText("{38}", card.LookOrNot);
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
