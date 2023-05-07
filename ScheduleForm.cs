using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniSchedule.Classes;
using UniSchedule.Helpers;

namespace UniSchedule
{
    public partial class ScheduleForm : Form
    {
        private Dictionary<int, int> _startingAt;
        public DisciplinesAbbvs disciplineAbbvs;
        public ScheduleForm()
        {
            InitializeComponent();
            _startingAt = new Dictionary<int, int>()
            {
                { 1, 7 },
                { 2, 9 },
                { 3, 11 },
                { 4, 13 },
                { 5, 15 },
                { 6, 17 }
            };
            disciplineAbbvs = new DisciplinesAbbvs();
        }

        public void UpdateTable(List<Schedule> schedules, string ProfOrRoom)
        {
            dataTable.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataTable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataTable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            foreach (var day in new List<string>() { "понеделник", "вторник", "сряда", "четвъртък", "петък"})
            {
                var row = dataTable.Rows.Add();
                var isLecture = false;
                var lab = "";
                dataTable.Rows[row].Cells[0].Value = day;
                for (int i = 1; i < dataTable.Columns.Count; i++)
                {
                    string value = "";
                    foreach (var schedule in schedules)
                    {
                        if (schedule.Timetable.ContainsKey(day) && schedule.Timetable[day].ContainsKey(_startingAt[i]))
                        {
                            if (schedule.Timetable[day][_startingAt[i]].LectureHorarium == 2)
                            {
                                lab = "л. " + schedule.Timetable[day][_startingAt[i]].Discipline;
                                isLecture = true;
                            }
                            else
                                lab = "л.y. " + disciplineAbbvs.DisciplineAbbvs[schedule.Timetable[day][_startingAt[i]].Discipline];
                            if (ProfOrRoom == "prof")
                            {
                                value = lab + " " +
                                    schedule.Timetable[day][_startingAt[i]].LabRooms.First() + " " +
                                    schedule.Timetable[day][_startingAt[i]].Spec + " " +
                                    schedule.Timetable[day][_startingAt[i]].Course + " к. " +
                                    schedule.Timetable[day][_startingAt[i]].Group +
                                    schedule.Timetable[day][_startingAt[i]].Subgroup + " \n";
                            }
                            else if (ProfOrRoom == "room")
                            {
                                value = schedule.Timetable[day][_startingAt[i]].Professor + " \n" +
                                    disciplineAbbvs.DisciplineAbbvs[schedule.Timetable[day][_startingAt[i]].Discipline] + " " +
                                    schedule.Timetable[day][_startingAt[i]].Spec + " " +
                                    schedule.Timetable[day][_startingAt[i]].Course + " к. " +
                                    schedule.Timetable[day][_startingAt[i]].Group +
                                    schedule.Timetable[day][_startingAt[i]].Subgroup + " \n";
                            }
                            else
                            {

                                if (value != lab + " " +
                                      schedule.Timetable[day][_startingAt[i]].LabRooms.First() + " " +
                                      schedule.Timetable[day][_startingAt[i]].Group +
                                      schedule.Timetable[day][_startingAt[i]].Subgroup + " \n")
                                    value += lab + " " +
                                        schedule.Timetable[day][_startingAt[i]].LabRooms.First() + " " +
                                        schedule.Timetable[day][_startingAt[i]].Group +
                                        schedule.Timetable[day][_startingAt[i]].Subgroup + " \n";
                            }

                        }
                    }
                    if (isLecture)
                    {
                        dataTable.Rows[row].Cells[i].Style.Font = new System.Drawing.Font(dataTable.Font, FontStyle.Bold);
                    }
                    isLecture = false;
                    dataTable.Rows[row].Cells[i].Value = value;
                }

            }
        }
        
        public void UpdateScheduleInfo(string col1, string col2, string col3, string col4, string col5)
        {
            dataInfo.Columns.Add("учебнагодина", "учебна година");
            dataInfo.Columns.Add("семестър", "семестър");
            var row = dataInfo.Rows.Add();
            dataInfo.Rows[row].Cells[0].Value = col1;
            dataInfo.Rows[row].Cells[1].Value = col2;
            if (col4 == "" && col5 == "")
            {
                dataInfo.Columns.Add("стая", "стая");
                dataInfo.Rows[row].Cells[2].Value = col3;
            }
            else if (col5 == "")
            {
                dataInfo.Columns.Add("катедра", "катедра");
                dataInfo.Columns.Add("преподавател", "преподавател");
                dataInfo.Rows[row].Cells[2].Value = col3;
                dataInfo.Rows[row].Cells[3].Value = col4;
            }
            else
            {
                dataInfo.Columns.Add("курс", "курс");
                dataInfo.Columns.Add("специалност", "специалност");
                dataInfo.Columns.Add("група", "група");
                dataInfo.Rows[row].Cells[2].Value = col4;
                dataInfo.Rows[row].Cells[3].Value = col3;
                dataInfo.Rows[row].Cells[4].Value = col5;
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            dataInfo.Rows.Clear();
            dataInfo.Columns.Clear();
            dataTable.Rows.Clear();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Сигурни ли сте, че искате да излезете от програмата?",
                "Изход",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
                Application.Exit();
        }

        private void exportSchedule_Click(object sender, EventArgs e)
        {
            if (dataTable.Rows.Count > 0 && dataInfo.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf)|*.pdf";
                save.FileName = "Schedule.pdf";
                bool ErrorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);
                        }
                        catch (Exception ex)
                        {
                            ErrorMessage = true;
                            MessageBox.Show("Файл със същото име вече съществува и не може да бъде презаписан!" + ex.Message);
                        }
                    }
                    if (!ErrorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dataTable.Columns.Count);
                            PdfPTable pTable1 = new PdfPTable(dataInfo.Columns.Count);
                            pTable.DefaultCell.Padding = 6;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            pTable1.DefaultCell.Padding = 6;
                            pTable1.WidthPercentage = 100;
                            pTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            iTextSharp.text.Font fon = FontFactory.GetFont("Arial Unicode MS", 10);
                            var baseFont = BaseFont.CreateFont(@"C:\Users\desis\source\repos\UniSchedule\Resources\arial-unicode-ms.ttf", "CP1251", BaseFont.EMBEDDED);
                            var myFont = new iTextSharp.text.Font(baseFont, 10);
                            foreach (DataGridViewColumn col in dataTable.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow Row in dataTable.Rows)
                            {
                                foreach (DataGridViewCell cell in Row.Cells)
                                {
                                    pTable.AddCell(new Phrase(cell.Value.ToString(), myFont));                                  
                                }
                                
                            }

                            foreach (DataGridViewColumn col in dataInfo.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                pTable1.AddCell(pCell);
                            }
                            foreach (DataGridViewRow Row in dataInfo.Rows)
                            {
                                foreach (DataGridViewCell cell in Row.Cells)
                                {
                                    pTable1.AddCell(new Phrase(cell.Value.ToString(), myFont));
                                }

                            }

                            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4, 20f, 40f, 40f, 20f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable1);
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();
                            }
                            MessageBox.Show("Успешно експортиране на таблицата!", "Информация");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Възникна грешка при експортиране на таблицата!" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не са открити данни в таблицата!", "Грешка");
            }
        }

    }
}
