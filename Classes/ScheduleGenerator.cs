using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniSchedule.Helpers;

namespace UniSchedule.Classes
{
    class ScheduleGenerator
    {
        private List<string> _subgroups { get; set; }
        private Action<int> _setProgress;

        public Dictionary<string, ProfessorSchedule> ProfSchedules { get; set; }
        public Dictionary<Group, StudentSchedule> StudSchedules { get; set; }
        public Dictionary<string, RoomSchedule> RoomSchedules { get; set; }
        public List<Lecture> NotScheduledLectures { get; set; }
        public FileHolder fileHolder { get; set; }
        public List<string> DayOfWeek { get; set; }
        public List<int> StartingAt { get; set; }
        public LectureRooms LectureRoom { get; set; }
        public MainForm mainForm;
        public Dictionary<string, string> Departs { get; set; }

        public ScheduleGenerator(Action<int> setProgress, MainForm mf)
        {
            _setProgress = setProgress;
            _subgroups = new List<string> { "a", "б" };
            NotScheduledLectures = new List<Lecture>();
            ProfSchedules = new Dictionary<string, ProfessorSchedule>();
            StudSchedules = new Dictionary<Group, StudentSchedule>();
            RoomSchedules = new Dictionary<string, RoomSchedule>();
            DayOfWeek = new List<string> { "понеделник", "вторник", "сряда", "четвъртък", "петък" };
            StartingAt = new List<int> { 9, 11, 13, 15, 17, 7 };
            LectureRoom = new LectureRooms();
            mainForm = mf;
            Departs = new Dictionary<string, string>();
        }

        public List<Schedule> GetProfTable(string professor)
        {
            if (ProfSchedules.ContainsKey(professor))
            {
                return new List<Schedule>() { ProfSchedules[professor] };
            }

            return new List<Schedule>() { new Schedule() };
        }

        public List<Schedule> GetStudTable(Group group)
        {
            var list = new List<Schedule>();
            var gr = new Group()
            {
                Spec = group.Spec,
                Course = group.Course
            };
            if (StudSchedules.ContainsKey(gr))
                list.Add(StudSchedules[gr]);
            gr = new Group()
            {
                Spec = group.Spec,
                Course = group.Course,
                GroupNum = group.GroupNum
            };
            if (StudSchedules.ContainsKey(gr))
                list.Add(StudSchedules[gr]);

            foreach (var item in _subgroups)
            {
                gr = new Group()
                {
                    Spec = group.Spec,
                    Course = group.Course,
                    GroupNum = group.GroupNum,
                    SubGroup = item
                };
                if (StudSchedules.ContainsKey(gr))
                    list.Add(StudSchedules[gr]);
            }
            return list;
        }

        public List<Schedule> GetRoomsTable(string room)
        {
            if (RoomSchedules.ContainsKey(room))
            {
                return new List<Schedule>() { RoomSchedules[room] };
            }

            return new List<Schedule>() { new Schedule() };
        }

        public void Process(string path)
        {
            _setProgress(50);
            _setProgress(100);
            fileHolder = new FileHolder(path);
            _setProgress(700);
            fileHolder.ParseAllFiles();
            _setProgress(800);
            SetAllLectures();
            _setProgress(850);
            SetProfessorBusySchedule();
            _setProgress(1000);
            GenerateSchedule();
        }

        public void SetAllLectures()
        {
            Dictionary<string, int> added_non_lectures = new Dictionary<string, int>();
            foreach (var item in fileHolder.AllData)
            {
                foreach (var item2 in item.TableDatas)
                {
                    if (item2.LectureHorarium != 0)
                        NotScheduledLectures.Add(new Lecture()
                        {
                            Professor = item.Professor,
                            Discipline = item2.Discipline,
                            Course = item2.Course,
                            Spec = item2.Spec,
                            LectureHorarium = item2.LectureHorarium
                        });

                    if (item2.LabGroups > 0)
                    {
                        if (!added_non_lectures.ContainsKey(item2.Discipline))
                            added_non_lectures.Add(item2.Discipline, 2);
                        int j = fileHolder.SpecDisciplineGroups[item2.Spec][item2.Discipline].Min();
                        for (int i = j; i < j + item2.LabGroups; i++)
                        {
                            Lecture lecture = new Lecture();
                            lecture.Professor = item.Professor;
                            lecture.Spec = item2.Spec;
                            lecture.Course = item2.Course;
                            lecture.Discipline = item2.Discipline;
                            lecture.LabRooms = item2.Rooms;
                            lecture.Group = (added_non_lectures[item2.Discipline] / 2).ToString();
                            lecture.Subgroup = _subgroups[added_non_lectures[item2.Discipline] % 2];
                            fileHolder.SpecDisciplineGroups[lecture.Spec][lecture.Discipline].RemoveAt(0);
                            NotScheduledLectures.Add(lecture);
                            added_non_lectures[item2.Discipline] += 1;
                        }
                    }
                    //if (item2.SeminarGroups != 0)
                    //    NotScheduledLectures.Add(new Lecture()
                    //    {
                    //        Professor = item.Professor,
                    //        Discipline = item2.Discipline,
                    //        Course = item2.Course,
                    //        Spec = item2.Spec,
                    //        SeminarGroup = item2.SeminarGroups,
                    //        LabRooms = item2.Rooms
                    //    });
                    //if (!item2.CourseProjectRoom.Equals(""))
                    //    NotScheduledLectures.Add(new Lecture()
                    //    {
                    //        Professor = item.Professor,
                    //        Discipline = item2.Discipline,
                    //        Course = item2.Course,
                    //        Spec = item2.Spec,
                    //        CourseProjectRoom = item2.CourseProjectRoom
                    //    });

                }
            }
        }

        public void SetProfessorBusySchedule()
        {
            foreach (var item in fileHolder.AllData)
            {
                ProfessorSchedule professorSchedule = new ProfessorSchedule();
                if (item.RectBoard.isEmpty() == 0)
                {
                    professorSchedule.DayFromToBusy.Add(item.RectBoard.Day, new Dictionary<int, bool>());
                    professorSchedule.DayFromToBusy[item.RectBoard.Day].Add(item.RectBoard.StartsAt, true);
                }
                if (item.AcademBoard.isEmpty() == 0)
                {
                    professorSchedule.DayFromToBusy.Add(item.AcademBoard.Day, new Dictionary<int, bool>());
                    professorSchedule.DayFromToBusy[item.AcademBoard.Day].Add(item.AcademBoard.StartsAt, true);
                }
                if (item.FacBoard.isEmpty() == 0)
                {
                    professorSchedule.DayFromToBusy.Add(item.FacBoard.Day, new Dictionary<int, bool>());
                    professorSchedule.DayFromToBusy[item.FacBoard.Day].Add(item.FacBoard.StartsAt, true);
                }
                if (item.CatBoard.isEmpty() == 0)
                {
                    professorSchedule.DayFromToBusy.Add(item.CatBoard.Day, new Dictionary<int, bool>());
                    professorSchedule.DayFromToBusy[item.CatBoard.Day].Add(item.CatBoard.StartsAt, true);
                }
                if (item.ContBoard.isEmpty() == 0)
                {
                    professorSchedule.DayFromToBusy.Add(item.ContBoard.Day, new Dictionary<int, bool>());
                    professorSchedule.DayFromToBusy[item.ContBoard.Day].Add(item.ContBoard.StartsAt, true);
                }
                if (item.OtherBoard.isEmpty() == 0)
                {
                    professorSchedule.DayFromToBusy.Add(item.OtherBoard.Day, new Dictionary<int, bool>());
                    professorSchedule.DayFromToBusy[item.OtherBoard.Day].Add(item.OtherBoard.StartsAt, true);
                }
                professorSchedule.Professor = item.Professor;
                ProfSchedules.Add(professorSchedule.Professor, professorSchedule);
            }
        }

        public void GenerateSchedule()
        {
            var allgroups = new Dictionary<string, Dictionary<string, List<Group>>>();
            foreach (var item in NotScheduledLectures)
            {
                if (!allgroups.ContainsKey(item.Spec))
                    allgroups.Add(item.Spec, new Dictionary<string, List<Group>>());
                if (!allgroups[item.Spec].ContainsKey(item.Course.ToString()))
                    allgroups[item.Spec].Add(item.Course.ToString(), new List<Group>());
                var toAdd = new Group()
                {
                    Spec = item.Spec,
                    Course = item.Course.ToString(),
                    GroupNum = item.Group,
                    SubGroup = item.Subgroup
                };
                if (String.IsNullOrEmpty(toAdd.GroupNum) || String.IsNullOrEmpty(toAdd.SubGroup))
                    continue;
                if (!allgroups[item.Spec][item.Course.ToString()].Contains(toAdd))
                    allgroups[item.Spec][item.Course.ToString()].Add(toAdd);
            }

            for (int k = NotScheduledLectures.Count - 1; k >= 0; k--)
            {
                if (NotScheduledLectures[k].LectureHorarium == 0)
                    continue;
                for (int i = 0; i < StartingAt.Count; i++)
                {
                    for (int j = 0; j < DayOfWeek.Count; j++)
                    { 
                        if (ProfSchedules[NotScheduledLectures[k].Professor].DayFromToBusy.ContainsKey(DayOfWeek[j]) && ProfSchedules[NotScheduledLectures[k].Professor].DayFromToBusy[DayOfWeek[j]].ContainsKey(StartingAt[i]))
                            continue;
                        if (!ProfSchedules[NotScheduledLectures[k].Professor].IsFree(DayOfWeek[j], StartingAt[i]))
                            continue;
                        bool allarefree = true;
                        foreach (var group in allgroups[NotScheduledLectures[k].Spec][NotScheduledLectures[k].Course.ToString()])
                        {
                            if (StudSchedules.ContainsKey(group) && !StudSchedules[group].IsFree(DayOfWeek[j], StartingAt[i]))
                            {
                                allarefree = false;
                                continue;
                            }
                        }

                        if (!allarefree)
                            continue;

                        bool added = false;
                        var rooms = NotScheduledLectures[k].LectureHorarium != 0 ? LectureRoom.AllLectureRooms :
                                            NotScheduledLectures[k].Subgroup != "" || NotScheduledLectures[k].SeminarGroup != 0 ? NotScheduledLectures[k].LabRooms : new List<string>();
                        string room = null;
                        foreach (var r in rooms)
                        {
                            if (!RoomSchedules.ContainsKey(r) || (RoomSchedules.ContainsKey(r) && RoomSchedules[r].IsFree(DayOfWeek[j], StartingAt[i])))
                            {
                                room = r;
                                break;
                            }
                        }
                        if (room == null)
                        {
                            continue;
                        }

                        var addLecture = NotScheduledLectures[k];
                        addLecture.LabRooms = new List<string>() { room };
                        addLecture.CourseProjectRoom = new List<string>() { room };
                        if (!RoomSchedules.ContainsKey(room))
                            RoomSchedules.Add(room, new RoomSchedule(room));
                        RoomSchedules[room].Add(DayOfWeek[j], StartingAt[i], addLecture);
                        ProfSchedules[NotScheduledLectures[k].Professor].Add(DayOfWeek[j], StartingAt[i], addLecture);
                        foreach (var group in allgroups[NotScheduledLectures[k].Spec][NotScheduledLectures[k].Course.ToString()])
                        {
                            if (!StudSchedules.ContainsKey(group))
                                StudSchedules.Add(group, new StudentSchedule(group));
                            StudSchedules[group].Add(DayOfWeek[j], StartingAt[i], addLecture);
                            added = true;
                        }

                        if (added)
                        {
                            j = DayOfWeek.Count;
                            i = StartingAt.Count;
                            NotScheduledLectures.RemoveAt(k);
                            break;
                        }
                    }
                }
            }
            for (int k = NotScheduledLectures.Count - 1; k >= 0; k--)
            {
                for (int i = 0; i < StartingAt.Count; i++)
                {
                    for (int j = 0; j < DayOfWeek.Count; j++)
                    {
                        if (NotScheduledLectures[k].LectureHorarium != 0)
                            continue;
                        if (ProfSchedules[NotScheduledLectures[k].Professor].DayFromToBusy.ContainsKey(DayOfWeek[j]) && ProfSchedules[NotScheduledLectures[k].Professor].DayFromToBusy[DayOfWeek[j]].ContainsKey(StartingAt[i]))
                            continue;
                        if (!ProfSchedules[NotScheduledLectures[k].Professor].IsFree(DayOfWeek[j], StartingAt[i]))
                            continue;

                        var group = new Group()
                        {
                            Spec = NotScheduledLectures[k].Spec,
                            Course = NotScheduledLectures[k].Course.ToString(),
                            GroupNum = NotScheduledLectures[k].Group,
                            SubGroup = NotScheduledLectures[k].Subgroup
                        };
                        if (StudSchedules.ContainsKey(group) && !StudSchedules[group].IsFree(DayOfWeek[j], StartingAt[i]))
                            continue;
                        var rooms = NotScheduledLectures[k].LectureHorarium != 0 ? LectureRoom.AllLectureRooms :
                                            NotScheduledLectures[k].Subgroup != "" || NotScheduledLectures[k].SeminarGroup != 0 ? NotScheduledLectures[k].LabRooms : new List<string>();
                        string room = null;
                        foreach (var r in rooms)
                        {
                            if (!RoomSchedules.ContainsKey(r))
                            {
                                room = r;
                                break;
                            }
                            else if (RoomSchedules[r].IsFree(DayOfWeek[j], StartingAt[i]))
                            {
                                room = r;
                                break;
                            }
                        }
                        if (room == null)
                        {
                            Console.WriteLine("Could not generate the schedule");
                            j = DayOfWeek.Count;
                            i = StartingAt.Count;
                            NotScheduledLectures.RemoveAt(k);
                            break;
                        }

                        var addLecture = NotScheduledLectures[k];
                        addLecture.LabRooms = new List<string>() { room };
                        addLecture.CourseProjectRoom = new List<string>() { room };
                        if (!RoomSchedules.ContainsKey(room))
                            RoomSchedules.Add(room, new RoomSchedule(room));
                        RoomSchedules[room].Add(DayOfWeek[j], StartingAt[i], addLecture);
                        ProfSchedules[NotScheduledLectures[k].Professor].Add(DayOfWeek[j], StartingAt[i], addLecture);
                        if (!StudSchedules.ContainsKey(group))
                            StudSchedules.Add(group, new StudentSchedule(group));
                        StudSchedules[group].Add(DayOfWeek[j], StartingAt[i], addLecture);
                        j = DayOfWeek.Count;
                        i = StartingAt.Count;
                        NotScheduledLectures.RemoveAt(k);
                        break;
                    }
                }
            }
            
        }

        public Dictionary<string, string> GetDeparts()
        {
            foreach (var item in fileHolder.AllData)
            {
                if (Departs.ContainsKey(item.Professor))
                    continue;
                else
                    Departs.Add(item.Professor, item.Depart);
            }
            return Departs;
        }
    }
}
