using SmartSchool.Models.BusinessLogicLayer;
using SmartSchool.Models.EntityLayer;
using SmartSchool.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SmartSchool.ViewModels
{
    public class FromTeacherViewModel : BasePropertyChanged
    {
        private ObservableCollection<User> students;
        public ObservableCollection<User> Students
        {
            get
            {
                var _class = SmartSchoolBLL.Class.GetClasses().FirstOrDefault(c => c.FormTeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentClass = SmartSchoolBLL.StudentClass.GetStudentClasses().Where(sc => sc.ClassId == _class.ClassId).ToList();
                var _students = SmartSchoolBLL.User.GetUsers().Where(u => studentClass.Any(sc => sc.StudentId == u.UserId)).ToList();
                students = new ObservableCollection<User>(_students);
                return students;
            }
            set
            {
                students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }

        private User selectedStudent;
        public User SelectedStudent
        {
            get
            {
                return selectedStudent;
            }
            set
            {
                selectedStudent = value;
                NotifyPropertyChanged(nameof(SelectedStudent));
                NotifyPropertyChanged(nameof(Absences));
                NotifyPropertyChanged(nameof(Average));
            }
        }

        private ObservableCollection<Subject> subjects;
        public ObservableCollection<Subject> Subjects
        {
            get
            {
                var _class = SmartSchoolBLL.Class.GetClasses().FirstOrDefault(c => c.FormTeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentClass = SmartSchoolBLL.StudentClass.GetStudentClasses().Where(sc => sc.ClassId == _class.ClassId).ToList();
                var _students = SmartSchoolBLL.User.GetUsers().Where(u => studentClass.Any(sc => sc.StudentId == u.UserId)).ToList();
                var studentSubjects = SmartSchoolBLL.StudentSubject.GetStudentSubjects().Where(ss => _students.Any(s => s.UserId == ss.StudentId)).ToList();
                var _subjects = SmartSchoolBLL.Subject.GetSubjects().Where(s => studentSubjects.Any(ss => ss.SubjectId == s.SubjectId)).ToList();
                subjects = new ObservableCollection<Subject>(_subjects);
                return subjects;
            }
            set
            {
                subjects = value;
                NotifyPropertyChanged(nameof(Subjects));
            }
        }

        private Subject selectedSubject;
        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set
            {
                selectedSubject = value;
                NotifyPropertyChanged(nameof(SelectedSubject));
                NotifyPropertyChanged(nameof(Absences));
                NotifyPropertyChanged(nameof(Average));
            }
        }

        private ObservableCollection<Absence> absences;
        public ObservableCollection<Absence> Absences
        {
            get
            {
                if (selectedStudent == null)
                    return new ObservableCollection<Absence>();
                if (selectedSubject == null)
                    return new ObservableCollection<Absence>();
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects().
                        FirstOrDefault(ss => (ss.SubjectId == selectedSubject.SubjectId && ss.StudentId == selectedStudent.UserId));
                var _absences = SmartSchoolBLL.Absence.GetAbsences().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId).ToList();
                absences = new ObservableCollection<Absence>(_absences);
                return absences;
            }
            set
            {
                absences = value;
                NotifyPropertyChanged(nameof(Absences));
            }
        }

        private Absence selectedAbsence;
        public Absence SelectedAbsence
        {
            get { return selectedAbsence; }
            set
            {
                selectedAbsence = value;
                NotifyPropertyChanged(nameof(SelectedAbsence));
            }
        }

        private ICommand justifyAbsenceCommand;
        public ICommand JustifyAbsenceCommand
        {
            get
            {
                if (justifyAbsenceCommand == null)
                {
                    justifyAbsenceCommand = new RelayCommand<Absence>(JustifyAbsence);
                }
                return justifyAbsenceCommand;
            }
        }

        private void JustifyAbsence(object param)
        {
            if (selectedSubject == null)
                return;
            if (selectedStudent == null)
                return;
            if (selectedAbsence == null)
                return;
            selectedAbsence.IsJustified = true;
            SmartSchoolBLL.Absence.ModifyAbsence(selectedAbsence);
            NotifyPropertyChanged(nameof(Absences));
        }

        private string average;
        public string Average
        {
            get { return average; }
            set
            {
                average = value;
                NotifyPropertyChanged(nameof(Average));
            }
        }

        private ICommand calculateStudentAverageForSubjectCommand;
        public ICommand CalculateStudentAverageForSubjectCommand
        {
            get
            {
                if (calculateStudentAverageForSubjectCommand == null)
                {
                    calculateStudentAverageForSubjectCommand = new RelayCommand<Absence>(CalculateStudentAverageForSubject);
                }
                return calculateStudentAverageForSubjectCommand;
            }
        }



        private void CalculateStudentAverageForSubject(object param)
        {
            if (selectedStudent == null)
                return;
            if (selectedSubject == null)
                return;
            StringBuilder sb = new StringBuilder();
            var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                    .FirstOrDefault(ss => (ss.SubjectId == selectedSubject.SubjectId && ss.StudentId == selectedStudent.UserId));
            var grades = SmartSchoolBLL.Grade.GetGrades().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId).ToList();
            var _class = SmartSchoolBLL.Class.GetClasses().
                    FirstOrDefault(c => c.FormTeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
            bool hasExam = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects().
                    Any(ss => (ss.SpecializationId == _class.SpecializationId && ss.HasSemesterExam == true && ss.SubjectId == studentSubject.SubjectId));

            double firstFinalAverage = -1;
            double secondFinalAverage = -1;
            if (hasExam)
            {
                if (grades.Where(g => g.Semester == 1).Count() >= 4 && grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1)))
                {
                    int examGrade = grades.FirstOrDefault(g => (g.IsSemesterExamGrade == true && g.Semester == 1)).Value;
                    double sum = grades.Where(g => (!g.IsSemesterExamGrade && g.Semester == 1)).Sum(g => g.Value);
                    double average = (sum / (grades.Where(g => g.Semester == 1).Count() - 1)) * 3 + examGrade;
                    double finalAverage = average / 4;
                    firstFinalAverage = finalAverage;
                    sb.AppendLine($"For first semester: {finalAverage.ToString("F2")}");
                }
                else
                {
                    sb.AppendLine($"For first semester: Not enough grades");
                }
                if (grades.Where(g => g.Semester == 2).Count() >= 4 && grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 2)))
                {
                    int examGrade = grades.FirstOrDefault(g => (g.IsSemesterExamGrade == true && g.Semester == 2)).Value;
                    double sum = grades.Where(g => (!g.IsSemesterExamGrade && g.Semester == 2)).Sum(g => g.Value);
                    double average = (sum / (grades.Where(g => g.Semester == 2).Count() - 1)) * 3 + examGrade;
                    double finalAverage = average / 4;
                    secondFinalAverage = finalAverage;
                    sb.AppendLine($"For second semester: {finalAverage.ToString("F2")}");
                }
                else
                {
                    sb.AppendLine($"For second semester: Not enough grades");
                }
            }
            else
            {
                if (grades.Where(g => g.Semester == 1).Count() >= 3 && !grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1)))
                {
                    double average = grades.Where(g => g.Semester == 1).Average(g => g.Value);
                    firstFinalAverage = average;
                    sb.AppendLine($"For first semester: {average.ToString("F2")}");
                }
                else
                {
                    sb.AppendLine($"For first semester: Not enough grades");
                }
                if (grades.Where(g => g.Semester == 2).Count() >= 3 && !grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 2)))
                {
                    double average = grades.Where(g => g.Semester == 2).Average(g => g.Value);
                    secondFinalAverage = average;
                    sb.AppendLine($"For second semester: {average.ToString("F2")}");
                }
                else
                {
                    sb.AppendLine($"For second semester: Not enough grades");
                }
            }
            if (firstFinalAverage == -1 || secondFinalAverage == -1)
            {
                sb.AppendLine("Cannot calculate average!");
            }
            else
            {
                double finalAverage = (firstFinalAverage + secondFinalAverage) / 2;
                sb.AppendLine($"Final average: {finalAverage}");
            }
            average = sb.ToString();
            NotifyPropertyChanged(nameof(Average));
        }

        private string classTop;
        public string ClassTop
        {
            get
            {
                classTop = CalculateTop();
                return classTop;
            }
            set
            {
                classTop = value;
                NotifyPropertyChanged(nameof(ClassTop));
            }
        }

        private string CalculateTop()
        {
            StringBuilder sb = new StringBuilder();
            List<string> list = new List<string>();

            foreach (var student in students)
            {
                double finalYearlyAverage = 0.0;
                foreach (var subject in subjects)
                {
                    var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                           .FirstOrDefault(ss => (ss.SubjectId == subject.SubjectId && ss.StudentId == student.UserId));
                    var grades = SmartSchoolBLL.Grade.GetGrades().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId);
                    var _class = SmartSchoolBLL.Class.GetClasses().
                            FirstOrDefault(c => c.FormTeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                    bool hasExam = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects().
                                Any(ss => (ss.SpecializationId == _class.SpecializationId && ss.HasSemesterExam == true && ss.SubjectId == studentSubject.SubjectId));
                    double firstFinalAverage = -1;
                    double secondFinalAverage = -1;
                    if (hasExam)
                    {
                        if (grades.Where(g => g.Semester == 1).Count() >= 4 && grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1)))
                        {
                            int examGrade = grades.FirstOrDefault(g => (g.IsSemesterExamGrade == true && g.Semester == 1)).Value;
                            double sum = grades.Where(g => (!g.IsSemesterExamGrade && g.Semester == 1)).Sum(g => g.Value);
                            double average = (sum / (grades.Where(g => g.Semester == 1).Count() - 1)) * 3 + examGrade;
                            double finalAverage1 = average / 4;
                            firstFinalAverage = finalAverage1;
                        }
                        if (grades.Where(g => g.Semester == 2).Count() >= 4 && grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 2)))
                        {
                            int examGrade = grades.FirstOrDefault(g => (g.IsSemesterExamGrade == true && g.Semester == 2)).Value;
                            double sum = grades.Where(g => (!g.IsSemesterExamGrade && g.Semester == 2)).Sum(g => g.Value);
                            double average = (sum / (grades.Where(g => g.Semester == 2).Count() - 1)) * 3 + examGrade;
                            double finalAverage1 = average / 4;
                            secondFinalAverage = finalAverage1;
                        }
                    }
                    else
                    {
                        if (grades.Where(g => g.Semester == 1).Count() >= 3 && !grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1)))
                        {
                            double average = grades.Where(g => g.Semester == 1).Average(g => g.Value);
                            firstFinalAverage = average;
                        }
                        if (grades.Where(g => g.Semester == 2).Count() >= 3 && !grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 2)))
                        {
                            double average = grades.Where(g => g.Semester == 2).Average(g => g.Value);
                            secondFinalAverage = average;
                        }
                    }
                    double finalAverage = (firstFinalAverage + secondFinalAverage) / 2;
                    finalYearlyAverage += finalAverage;
                }
                list.Add(($"{student.FullName} : {finalYearlyAverage / subjects.Count():F2}"));

            }
            list.Sort((a, b) => b.Substring(b.Length - 4).CompareTo(a.Substring(a.Length - 4)));

            int valPremiu = 1;
            int valMentiune = 1;
            for (int i = 0; i < list.Count - 1; i++)  // Iterate until the second-to-last element
            {
                if (valPremiu <= 3)
                {
                    sb.AppendLine(list[i] + $" Premiul {valPremiu}");
                    if (list[i].Substring(list[i].Length - 4) != list[i + 1].Substring(list[i + 1].Length - 4))
                        valPremiu++;
                }
                else if (valMentiune <= 3)
                {
                    sb.AppendLine(list[i] + $" Mentiunea {valMentiune}");
                    if (list[i].Substring(list[i].Length - 4) != list[i + 1].Substring(list[i + 1].Length - 4))
                        valMentiune++;
                }
                else
                {
                    sb.AppendLine(list[i]);
                }
            }
            sb.AppendLine(list[list.Count - 1]);

            return sb.ToString();
        }

    }
}