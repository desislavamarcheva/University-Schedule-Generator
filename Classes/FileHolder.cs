using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UniSchedule.Helpers;
using System.Linq;

namespace UniSchedule.Classes
{
    class FileHolder
    {
        private string _inputDir;

        public List<PdfData> AllData { get; set; }
        public List<int> listIntegers { get; set; }
        public Dictionary<string, Dictionary<string, List<int>>> SpecDisciplineGroups;

        public FileHolder(string inputDir)
        {
            listIntegers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            _inputDir = inputDir;
            AllData = new List<PdfData>();
            SpecDisciplineGroups = new Dictionary<string, Dictionary<string, List<int>>>();
        }

        public void ParseAllFiles()
        {
            foreach (string file in Directory.EnumerateFiles(_inputDir))
            {
                try
                {
                    AllData.Add(RequestReader.ParseFile(file));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception {e.ToString()} was found while processing {file}");
                }
            }
            foreach (var item in AllData)
            {
                foreach (var row in item.TableDatas)
                {

                    if (!SpecDisciplineGroups.ContainsKey(row.Spec))
                        SpecDisciplineGroups.Add(row.Spec, new Dictionary<string, List<int>>());
                    if (!SpecDisciplineGroups[row.Spec].ContainsKey(row.Discipline))
                        SpecDisciplineGroups[row.Spec].Add(row.Discipline, listIntegers.GetRange(1, 0));

                    int k = SpecDisciplineGroups[row.Spec][row.Discipline].Count;

                    for (int i = k; i < row.LabGroups + k; i++)
                    {
                        SpecDisciplineGroups[row.Spec][row.Discipline].Add(listIntegers[i]);
                    }
                }

            }
            AllData.Sort();
        }
    }
}
