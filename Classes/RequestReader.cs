using GroupDocs.Parser;
using GroupDocs.Parser.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UniSchedule.Helpers;

namespace UniSchedule.Classes
{
    class RequestReader
    {
        private Regex _studyType;
        private Regex _professor;
        private Regex _depart;
        private Regex _semester;
        private Regex _year;
        private Regex _rectBoard;
        private Regex _academBoard;
        private Regex _facBoard;
        private Regex _catBoard;
        private Regex _contBoard;
        private Regex _otherBoard;
        private Regex _rooms;

        public string FileContent { get; set; }
        public List<TableData> td { get; set; }
        public Disciplines disciplines { get; set; }
        public SubGroups subGroups { get; set; }
        public SeminarGroups seminarGroups { get; set; }

        public RequestReader(string filePath)
        {
            td = new List<TableData>();
            disciplines = new Disciplines();
            subGroups = new SubGroups();
            seminarGroups = new SeminarGroups();
            FileContent = new Parser(filePath)
                .GetFormattedText(new FormattedTextOptions(FormattedTextMode.PlainText))
                .ReadToEnd()
                .Replace("\t", " ")
                .Replace('„', '"')
                .Replace('”', '"');
            _studyType = new Regex(@"занятията ([\w ]*)обучение");
            _professor = new Regex(@"преподавател: ([\w .-]*)");
            _depart = new Regex("катедра \\\"([\\w.-]*)\\\"");
            _semester = new Regex(@"([\w .-]*) семестър");
            _year = new Regex(@"учебната ([\w/.]*)");
            _rectBoard = new Regex(@"Ректорски съвет\W+(\w+)\W+(\d+)-(\d+)", RegexOptions.IgnoreCase);
            _academBoard = new Regex(@"Академичен съвет\W+(\w+)\W+(\d+)-(\d+)", RegexOptions.IgnoreCase);
            _facBoard = new Regex(@"Фак\. \(Колеж\.\) съвет\W+(\w+)\W+(\d+)-(\d+)", RegexOptions.IgnoreCase);
            _catBoard = new Regex(@"Кат\. \(Департ\.\) съвет\W+(\w+)\W+(\d+)-(\d+)", RegexOptions.IgnoreCase);
            _contBoard = new Regex(@"Контролен съвет\W+(\w+)\W+(\d+)-(\d+)", RegexOptions.IgnoreCase);
            _otherBoard = new Regex(@"\(комисии, съвети\)\W+(\w+)\W+(\d+)-(\d+)", RegexOptions.IgnoreCase);
            _rooms = new Regex(@"(\d+\w*)+");
        }


        public static PdfData ParseFile(string filePath)
        {
            RequestReader rr = new RequestReader(filePath);
            return new PdfData()
            {
                StudyType = rr.GetStudyType(),
                Professor = rr.GetProfessor(),
                Depart = rr.GetDepart(),
                Semester = rr.GetSemester(),
                Year = rr.GetYear(),
                TableDatas = rr.GetTable(),
                RectBoard = rr.GetRectBoard(),
                AcademBoard = rr.GetAcademBoard(),
                FacBoard = rr.GetFacBoard(),
                CatBoard = rr.GetCatBoard(),
                ContBoard = rr.GetContBoard(),
                OtherBoard = rr.GetOtherBoard()
            };
        }

        public string GetStudyType()
        {
            var match = _studyType.Match(FileContent).Groups[1].ToString().Trim();
            if (String.IsNullOrEmpty(match))
            {
                MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                    "Грешка при обработване на бланките за планиране на занятията",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }
            return match;
        }

        public string GetProfessor()
        {
            var match = _professor.Match(FileContent).Groups[1].ToString().Trim();
            if (String.IsNullOrEmpty(match))
            {
                MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                    "Грешка при обработване на бланките за планиране на занятията",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }
            return match;
        }

        public string GetDepart()
        {
            var match = _depart.Match(FileContent).Groups[1].ToString().Trim();
            if (String.IsNullOrEmpty(match))
            {
                MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                    "Грешка при обработване на бланките за планиране на занятията",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }
            return match;
        }
        public string GetSemester()
        {
            var match = _semester.Match(FileContent).Groups[1].ToString().Trim();
            if (String.IsNullOrEmpty(match))
            {
                MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                    "Грешка при обработване на бланките за планиране на занятията",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }
            return match;
        }
        public string GetYear()
        {
            var match = _year.Match(FileContent).Groups[1].ToString().Trim();
            if (String.IsNullOrEmpty(match))
            {
                MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                    "Грешка при обработване на бланките за планиране на занятията",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return null;
            }
            return match;
        }

        public Board GetRectBoard()
        {
            var x = _rectBoard.Match(FileContent);
            if (x.Groups.Count == 1)
                return new Board()
                {
                    Day = "",
                    StartsAt = 0,
                    EndsAt = 0
                };
            else if (x.Groups.Count == 4)
                return new Board()
                {
                    Day = x.Groups[1].ToString().Trim(),
                    StartsAt = int.Parse(x.Groups[2].ToString().Trim()),
                    EndsAt = int.Parse(x.Groups[3].ToString().Trim())
                };
            return null;
        }

        public Board GetAcademBoard()
        {
            var x = _academBoard.Match(FileContent);
            if (x.Groups.Count == 1)
                return new Board()
                {
                    Day = "",
                    StartsAt = 0,
                    EndsAt = 0
                };
            else if (x.Groups.Count == 4)
                return new Board()
                {
                    Day = x.Groups[1].ToString().Trim(),
                    StartsAt = int.Parse(x.Groups[2].ToString().Trim()),
                    EndsAt = int.Parse(x.Groups[3].ToString().Trim())
                };
            return null;
        }

        public Board GetFacBoard()
        {
            var x = _facBoard.Match(FileContent);
            if (x.Groups.Count == 1)
                return new Board()
                {
                    Day = "",
                    StartsAt = 0,
                    EndsAt = 0
                };
            else if (x.Groups.Count == 4)
                return new Board()
                {
                    Day = x.Groups[1].ToString().Trim(),
                    StartsAt = int.Parse(x.Groups[2].ToString().Trim()),
                    EndsAt = int.Parse(x.Groups[3].ToString().Trim())
                };
            return null;
        }

        public Board GetCatBoard()
        {
            var x = _catBoard.Match(FileContent);
            if (x.Groups.Count == 1)
                return new Board()
                {
                    Day = "",
                    StartsAt = 0,
                    EndsAt = 0
                };
            else if (x.Groups.Count == 4)
                return new Board()
                {
                    Day = x.Groups[1].ToString().Trim(),
                    StartsAt = int.Parse(x.Groups[2].ToString().Trim()),
                    EndsAt = int.Parse(x.Groups[3].ToString().Trim())
                };
            return null;
        }

        public Board GetContBoard()
        {
            var x = _contBoard.Match(FileContent);
            if (x.Groups.Count == 1)
                return new Board()
                {
                    Day = "",
                    StartsAt = 0,
                    EndsAt = 0
                };
            else if (x.Groups.Count == 4)
                return new Board()
                {
                    Day = x.Groups[1].ToString().Trim(),
                    StartsAt = int.Parse(x.Groups[2].ToString().Trim()),
                    EndsAt = int.Parse(x.Groups[3].ToString().Trim())
                };
            return null;
        }

        public Board GetOtherBoard()
        {
            var x = _otherBoard.Match(FileContent);
            if (x.Groups.Count == 1)
                return new Board()
                {
                    Day = "",
                    StartsAt = 0,
                    EndsAt = 0
                };
            else if (x.Groups.Count == 4)
                return new Board()
                {
                    Day = x.Groups[1].ToString().Trim(),
                    StartsAt = int.Parse(x.Groups[2].ToString().Trim()),
                    EndsAt = int.Parse(x.Groups[3].ToString().Trim())
                };
            return null;
        }
        public List<TableData> GetTable()
        {
            var data = PdfTableParser.Parse(FileContent);
            for (int i = 0; i < data.Count; i++)
            {
                if (i <= 1)
                    continue;
                else
                {
                    if (data[i][10] == "")
                        data[i][10] = data[i-1][10];

                    TableData tableData = new TableData();
                    if (disciplines.DisciplineNames.ContainsKey(data[i][0]))
                        tableData.Discipline = disciplines.DisciplineNames[data[i][0]];
                    else
                        MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                                        "Грешка при обработване на бланките за планиране на занятията",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                    if (String.IsNullOrEmpty(data[i][1].ToString()))
                        MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                                        "Грешка при обработване на бланките за планиране на занятията",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    else
                        tableData.Course = int.Parse(data[i][1].ToString());

                    if (String.IsNullOrEmpty(data[i][3].ToString()))
                        MessageBox.Show("Невалидни данни в бланка за планиране на занятията. Моля прегледайте бланките и се уверете, че данните са попълнени коректно.",
                                        "Грешка при обработване на бланките за планиране на занятията",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    else
                        tableData.Spec = data[i][3];

                    tableData.LectureHorarium = int.Parse(data[i][4].ToString() == "" ? "0" : data[i][4]);
                    tableData.Multimedia = data[i][5].ToString();
                    tableData.SeminarGroups = seminarGroups.SeminarGrs.ContainsKey(data[i][7]) ? seminarGroups.SeminarGrs[data[i][7]] : 0;
                    tableData.LabGroups = (subGroups.SubGrs.ContainsKey(data[i][9])) ? subGroups.SubGrs[data[i][9]] : 0;
                    tableData.Rooms = _rooms.Matches(data[i][10]).Cast<Match>().Select(m => m.Value).ToList();
                    tableData.CourseProjectRoom = _rooms.Matches(data[i][12]).Cast<Match>().Select(m => m.Value).ToList();
                    td.Add(tableData);
                }
            }
            return td;
        }
    }
}
