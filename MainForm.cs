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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniSchedule.Classes;
using UniSchedule.Helpers;

namespace UniSchedule
{
    public partial class MainForm : Form
    {
        public string Spec { get; set; }
        public int Course { get; set; }
        public string Group { get; set; }
        public string Professor { get; set; }
        private ScheduleGenerator _scheduleGenerator;
        private ScheduleForm _form2;

        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Normal;
            _form2 = new ScheduleForm();
            _scheduleGenerator = new ScheduleGenerator(IncrementProgressBar, this);
        }

        private void uploadDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string file = folderBrowserDialog.SelectedPath.ToString();
                {
                    try
                    {
                        textBox1.Text = file;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Възникна грешка при опит за зареждане директорията " + ex.Message);
                    }
                }
            }
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Инструкции за използване: \n" +
                "1. Прикачете директорията, съдържаща бланките за планиране на занятията, нужни за генериране на учебните разписи, чрез бутон \"Прикачи директория\". \n" +
                "2. С натискане на бутон \"Генерирай\" по конкретен алгоритъм се генерира предложение на учебни разписи за студенти, преподаватели и стаи. \n" +
                "3. От графата \"Изберете учебен разпис\" се посочват данните за разписът, който ще се визуализира. \n" +
                "4. С натискане на бутон \"Покажи\" се визуализира избраният учебен разпис. \n" +
                "5. Визуализираният учебен разпис е възможно да се запише като PDF файл, чрез бутон \"Запиши разпис\". \n" +
                "6. С натискане на бутон \"Връщане\" във формата с визуализирания учебен разпис се преминава отново към главната форма, където може да се избере визуализиране на друг учебен разпис или запис на всички разписи. \n" +
                "7 .Записът на всички разписи се осъществява чрез бутоните \"Запиши разписите на всички студенти/преподаватели/стаи\". \n",
                "Генератор на учебни разписи",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

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

        private void IncrementGently(int progress)
        {
            for (int i = generatingProgressBar.Value; i < progress; i++)
            {
                generatingProgressBar.Invoke((MethodInvoker)delegate
                {
                    generatingProgressBar.Value = i;
                });
            }
        }

        public void IncrementProgressBar(int progress)
        {
            new Thread(() =>
            {
                IncrementGently(progress);
            }).Start();
        }

        private async void generateButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                generating.Visible = true;
                generatingProgressBar.Visible = true;
                generatingProgressBar.Value = 0;
                generatingProgressBar.Minimum = 0;
                generatingProgressBar.Maximum = 1000;
                await Task.Run(() => _scheduleGenerator.Process(textBox1.Text));
                
                generatingProgressBar.Visible = false;
                generatingProgressBar.Value = 0;

                wasSuccessful();


            }
            else
                MessageBox.Show("Моля посочете път до директория.", "Не сте посочили път до директория.");
            Showbutton.Enabled = true;
        }

        private void radioButtonProfessors_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSpec.Enabled = false;
            comboBoxCourse.Enabled = false;
            comboBoxGroup.Enabled = false;
            comboBoxRooms.Enabled = false;
            comboBoxDepart.Enabled = true;
            comboBoxProfessor.Enabled = true;
        }

        private void radioButtonStudents_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxSpec.Enabled = true;
            comboBoxCourse.Enabled = true;
            comboBoxGroup.Enabled = true;
            comboBoxDepart.Enabled = false;
            comboBoxProfessor.Enabled = false;
            comboBoxRooms.Enabled = false;
        }

        private void roomsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxRooms.Enabled = true;
            comboBoxSpec.Enabled = false;
            comboBoxCourse.Enabled = false;
            comboBoxGroup.Enabled = false;
            comboBoxDepart.Enabled = false;
            comboBoxProfessor.Enabled = false;
        }

        private void Show_Click(object sender, EventArgs e)
        {
            if (radioButtonProfessors.Checked == true)
            {
                _form2.UpdateTable(_scheduleGenerator.GetProfTable(comboBoxProfessor.SelectedItem.ToString()), "prof");
                _form2.UpdateScheduleInfo(_scheduleGenerator.fileHolder.AllData.ElementAt(0).Year,
                    _scheduleGenerator.fileHolder.AllData.ElementAt(0).Semester,
                    comboBoxDepart.SelectedItem.ToString(), comboBoxProfessor.SelectedItem.ToString(), "");
            }
            else if (radioButtonStudents.Checked == true)
            {
                _form2.UpdateTable(_scheduleGenerator.GetStudTable(
                    new Group()
                    {
                        Spec = comboBoxSpec.SelectedItem.ToString(),
                        Course = comboBoxCourse.SelectedItem.ToString(),
                        GroupNum = comboBoxGroup.SelectedItem.ToString()
                    }), "");
                _form2.UpdateScheduleInfo(_scheduleGenerator.fileHolder.AllData.ElementAt(0).Year,
                    _scheduleGenerator.fileHolder.AllData.ElementAt(0).Semester,
                    comboBoxSpec.SelectedItem.ToString(), comboBoxCourse.SelectedItem.ToString(), comboBoxGroup.SelectedItem.ToString());
            }
            else
            {
                _form2.UpdateTable(_scheduleGenerator.GetRoomsTable(comboBoxRooms.SelectedItem.ToString()), "room");
                _form2.UpdateScheduleInfo(_scheduleGenerator.fileHolder.AllData.ElementAt(0).Year,
                    _scheduleGenerator.fileHolder.AllData.ElementAt(0).Semester,
                    comboBoxRooms.SelectedItem.ToString(), "", "");
            }
            _form2.Show();
        }

        private void saveStudSchedulesButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF (*.pdf)|*.pdf";
            save.FileName = "StudentSchedules.pdf";

            if (save.ShowDialog() == DialogResult.OK)
                if (File.Exists(save.FileName))
                {
                    try
                    {
                        File.Delete(save.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Файл със същото име вече съществува и не може да бъде презаписан!" + ex.Message);
                    }
                }
            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
            {
                Document document = new Document(PageSize.A4, 20f, 40f, 40f, 20f);
                PdfWriter.GetInstance(document, fileStream);
                document.Open();

                foreach (var studSchedule in _scheduleGenerator.StudSchedules)
                {
                    if (studSchedule.Key.SubGroup == "a")
                        continue;
                    else
                    {
                        _form2.UpdateTable(_scheduleGenerator.GetStudTable(studSchedule.Key), "");
                        _form2.UpdateScheduleInfo(_scheduleGenerator.fileHolder.AllData.ElementAt(0).Year,
                        _scheduleGenerator.fileHolder.AllData.ElementAt(0).Semester,
                        studSchedule.Key.Spec, studSchedule.Key.Course, studSchedule.Key.GroupNum);

                        if (_form2.dataTable.Rows.Count > 0 && _form2.dataInfo.Rows.Count > 0)
                        {

                            try
                            {
                                PdfPTable pTable = new PdfPTable(_form2.dataTable.Columns.Count);
                                PdfPTable pTable1 = new PdfPTable(_form2.dataInfo.Columns.Count);
                                pTable.DefaultCell.Padding = 6;
                                pTable.WidthPercentage = 100;
                                pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                                pTable1.DefaultCell.Padding = 6;
                                pTable1.WidthPercentage = 100;
                                pTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                                var baseFont = BaseFont.CreateFont(@"C:\Users\desis\source\repos\UniSchedule\Resources\arial-unicode-ms.ttf", "CP1251", BaseFont.EMBEDDED);
                                var myFont = new iTextSharp.text.Font(baseFont, 10);
                                foreach (DataGridViewColumn col in _form2.dataTable.Columns)
                                {
                                    PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                    pTable.AddCell(pCell);
                                }
                                foreach (DataGridViewRow Row in _form2.dataTable.Rows)
                                {
                                    foreach (DataGridViewCell cell in Row.Cells)
                                    {
                                        pTable.AddCell(new Phrase(cell.Value.ToString(), myFont));
                                    }

                                }

                                foreach (DataGridViewColumn col in _form2.dataInfo.Columns)
                                {
                                    PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                    pTable1.AddCell(pCell);
                                }
                                foreach (DataGridViewRow Row in _form2.dataInfo.Rows)
                                {
                                    foreach (DataGridViewCell cell in Row.Cells)
                                    {
                                        if (cell.Value == null)
                                            continue;
                                        else
                                            pTable1.AddCell(new Phrase(cell.Value.ToString(), myFont));
                                    }

                                }
                                pTable.SpacingAfter = 30f;
                                pTable1.SpacingAfter = 10f;
                                document.Add(pTable1);
                                document.Add(pTable);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Възникна грешка при експортиране на таблицата!" + ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Не са открити данни в таблицата!", "Грешка");
                        }
                        _form2.dataInfo.Rows.Clear();
                        _form2.dataInfo.Columns.Clear();
                        _form2.dataTable.Rows.Clear();
                    }
                }

                document.Close();
                fileStream.Close();
                MessageBox.Show("Успешно експортиране на таблицата!", "Информация");
            }
        }

        private void saveProfSchedulesButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF (*.pdf)|*.pdf";
            save.FileName = "ProfessorSchedules.pdf";

            if (save.ShowDialog() == DialogResult.OK)
                if (File.Exists(save.FileName))
                {
                    try
                    {
                        File.Delete(save.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Файл със същото име вече съществува и не може да бъде презаписан!" + ex.Message);
                    }
                }
            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
            {
                Document document = new Document(PageSize.A4, 20f, 40f, 40f, 20f);
                PdfWriter.GetInstance(document, fileStream);
                document.Open();

                foreach (var profSchedule in _scheduleGenerator.ProfSchedules)
                {

                    _form2.UpdateTable(_scheduleGenerator.GetProfTable(profSchedule.Key), "prof");
                    _form2.UpdateScheduleInfo(_scheduleGenerator.fileHolder.AllData.ElementAt(0).Year,
                        _scheduleGenerator.fileHolder.AllData.ElementAt(0).Semester,
                        "КНТ", profSchedule.Value.Professor, "");

                    if (_form2.dataTable.Rows.Count > 0 && _form2.dataInfo.Rows.Count > 0)
                    {

                        try
                        {
                            PdfPTable pTable = new PdfPTable(_form2.dataTable.Columns.Count);
                            PdfPTable pTable1 = new PdfPTable(_form2.dataInfo.Columns.Count);
                            pTable.DefaultCell.Padding = 6;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            pTable1.DefaultCell.Padding = 6;
                            pTable1.WidthPercentage = 100;
                            pTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            var baseFont = BaseFont.CreateFont(@"C:\Users\desis\source\repos\UniSchedule\Resources\arial-unicode-ms.ttf", "CP1251", BaseFont.EMBEDDED);
                            var myFont = new iTextSharp.text.Font(baseFont, 10);
                            foreach (DataGridViewColumn col in _form2.dataTable.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow Row in _form2.dataTable.Rows)
                            {
                                foreach (DataGridViewCell cell in Row.Cells)
                                {
                                    pTable.AddCell(new Phrase(cell.Value.ToString(), myFont));
                                }

                            }

                            foreach (DataGridViewColumn col in _form2.dataInfo.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                pTable1.AddCell(pCell);
                            }
                            foreach (DataGridViewRow Row in _form2.dataInfo.Rows)
                            {
                                foreach (DataGridViewCell cell in Row.Cells)
                                {
                                    if (cell.Value == null)
                                        continue;
                                    else
                                        pTable1.AddCell(new Phrase(cell.Value.ToString(), myFont));
                                }

                            }

                            pTable.SpacingAfter = 30f;
                            pTable1.SpacingAfter = 10f;
                            document.Add(pTable1);
                            document.Add(pTable);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Възникна грешка при експортиране на таблицата!" + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не са открити данни в таблицата!", "Грешка");
                    }
                    _form2.dataInfo.Rows.Clear();
                    _form2.dataInfo.Columns.Clear();
                    _form2.dataTable.Rows.Clear();
                }

                document.Close();
                fileStream.Close();
                MessageBox.Show("Успешно експортиране на таблицата!", "Информация");
            }
        }

        private void saveRoomSchedulesButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF (*.pdf)|*.pdf";
            save.FileName = "RoomSchedules.pdf";

            if (save.ShowDialog() == DialogResult.OK)
                if (File.Exists(save.FileName))
                {
                    try
                    {
                        File.Delete(save.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Файл със същото име вече съществува и не може да бъде презаписан!" + ex.Message);
                    }
                }
            using (FileStream fileStream = new FileStream(save.FileName, FileMode.Create))
            {
                Document document = new Document(PageSize.A4, 20f, 40f, 40f, 20f);
                PdfWriter.GetInstance(document, fileStream);
                document.Open();

                foreach (var roomSchedule in _scheduleGenerator.RoomSchedules)
                {

                    _form2.UpdateTable(_scheduleGenerator.GetRoomsTable(roomSchedule.Key), "room");
                    _form2.UpdateScheduleInfo(_scheduleGenerator.fileHolder.AllData.ElementAt(0).Year,
                    _scheduleGenerator.fileHolder.AllData.ElementAt(0).Semester,
                    roomSchedule.Key, "", "");

                    if (_form2.dataTable.Rows.Count > 0 && _form2.dataInfo.Rows.Count > 0)
                    {

                        try
                        {
                            PdfPTable pTable = new PdfPTable(_form2.dataTable.Columns.Count);
                            PdfPTable pTable1 = new PdfPTable(_form2.dataInfo.Columns.Count);
                            pTable.DefaultCell.Padding = 6;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            pTable1.DefaultCell.Padding = 6;
                            pTable1.WidthPercentage = 100;
                            pTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            var baseFont = BaseFont.CreateFont(@"C:\Users\desis\source\repos\UniSchedule\Resources\arial-unicode-ms.ttf", "CP1251", BaseFont.EMBEDDED);
                            var myFont = new iTextSharp.text.Font(baseFont, 10);
                            foreach (DataGridViewColumn col in _form2.dataTable.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow Row in _form2.dataTable.Rows)
                            {
                                foreach (DataGridViewCell cell in Row.Cells)
                                {
                                    pTable.AddCell(new Phrase(cell.Value.ToString(), myFont));
                                }

                            }

                            foreach (DataGridViewColumn col in _form2.dataInfo.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText, myFont));
                                pTable1.AddCell(pCell);
                            }
                            foreach (DataGridViewRow Row in _form2.dataInfo.Rows)
                            {
                                foreach (DataGridViewCell cell in Row.Cells)
                                {
                                    if (cell.Value == null)
                                        continue;
                                    else
                                        pTable1.AddCell(new Phrase(cell.Value.ToString(), myFont));
                                }

                            }

                            pTable.SpacingAfter = 30f;
                            pTable1.SpacingAfter = 10f;
                            document.Add(pTable1);
                            document.Add(pTable);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Възникна грешка при експортиране на таблицата!" + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не са открити данни в таблицата!", "Грешка");
                    }
                    _form2.dataInfo.Rows.Clear();
                    _form2.dataInfo.Columns.Clear();
                    _form2.dataTable.Rows.Clear();
                }

                document.Close();
                fileStream.Close();
                MessageBox.Show("Успешно експортиране на таблицата!", "Информация");
            }
        }

        private void comboBoxSpec_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var spec in _scheduleGenerator.StudSchedules)
            {
                if (comboBoxSpec.Items.Contains(spec.Key.Spec))
                    continue;
                else
                    comboBoxSpec.Items.Add(spec.Key.Spec);
            }
            comboBoxSpec.Sorted = true;
        }

        private void comboBoxRooms_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (var room in _scheduleGenerator.RoomSchedules.Keys)
            {
                if (comboBoxRooms.Items.Contains(room))
                    continue;
                else
                    comboBoxRooms.Items.Add(room);
            }
            comboBoxRooms.Sorted = true;
        }

        private void comboBoxSpec_TextChanged(object sender, EventArgs e)
        {
            comboBoxCourse.Items.Clear();
            foreach (var item in _scheduleGenerator.StudSchedules)
            {
                if (item.Key.Spec == comboBoxSpec.SelectedItem.ToString())
                {
                    if (comboBoxCourse.Items.Contains(item.Key.Course))
                        continue;
                    else
                        comboBoxCourse.Items.Add(item.Key.Course);
                }
            }
            comboBoxCourse.Sorted = true;
        }

        private void comboBoxCourse_TextChanged(object sender, EventArgs e)
        {
            comboBoxGroup.Items.Clear();
            foreach (var item in _scheduleGenerator.StudSchedules)
            {
                if (item.Key.Course == comboBoxCourse.SelectedItem.ToString())
                {
                    if (comboBoxGroup.Items.Contains(item.Key.GroupNum))
                        continue;
                    else
                        comboBoxGroup.Items.Add(item.Key.GroupNum);
                }
            }

        }

        private async void wasSuccessful()
        {
            if (_scheduleGenerator.NotScheduledLectures.Count > 0)
            {
                generating.Text = "Неуспешно генерирани!";
                var neverScheduled = "";
                foreach (var notScheduled in _scheduleGenerator.NotScheduledLectures)
                {
                    neverScheduled += notScheduled.Professor + " " + notScheduled.Discipline + " " + notScheduled.Spec + " " + notScheduled.Course + " курс" + System.Environment.NewLine;
                }
                
                MessageBox.Show(string.Concat("Информация за неразпределените занятия: ", neverScheduled),
                    _scheduleGenerator.NotScheduledLectures.Count.ToString() + " на брой занятията не успяха да бъдат разпределени.",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
                generating.Text = "Успешно генерирани!";
        }

        private void comboBoxDepart_MouseClick(object sender, MouseEventArgs e)
        {
            comboBoxProfessor.Items.Clear();
            var departs = _scheduleGenerator.GetDeparts();

            foreach (var depart in departs)
            {
                if (comboBoxDepart.Items.Contains(depart.Value))
                    continue;
                else
                    comboBoxDepart.Items.Add(depart.Value);
            }
            comboBoxDepart.Sorted = true;
        }

        private void comboBoxDepart_TextChanged(object sender, EventArgs e)
        {
            var departs = _scheduleGenerator.GetDeparts();

            foreach (var item in departs)
            {
                if (item.Value == comboBoxDepart.SelectedItem.ToString())
                    comboBoxProfessor.Items.Add(item.Key);
            }
            comboBoxProfessor.Sorted = true;
        }
    }
}
