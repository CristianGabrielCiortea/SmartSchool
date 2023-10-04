using SmartSchool.Models.BusinessLogicLayer;
using SmartSchool.Models.EntityLayer;
using SmartSchool.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SmartSchool.ViewModels
{
    public class StudentViewModel: BasePropertyChanged
    {
        private ObservableCollection<Subject> subjects;
        public ObservableCollection<Subject> Subjects
        {
            get
            {
                var studentSubjects = SmartSchoolBLL.StudentSubject.GetStudentSubjects().Where(ss => ss.StudentId == SmartSchoolBLL.User.CurrentUser.UserId).ToList();
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
            get
            {
                return selectedSubject;
            }
            set
            {
                selectedSubject = value;
                NotifyPropertyChanged(nameof(SelectedSubject));
                NotifyPropertyChanged(nameof(Grades));
                NotifyPropertyChanged(nameof(Absences));
                NotifyPropertyChanged(nameof(TeachingMaterials));
            }
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
            if (selectedSubject == null)
                return;
            StringBuilder sb = new StringBuilder();
            var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                    .FirstOrDefault(ss => (ss.SubjectId == selectedSubject.SubjectId && ss.StudentId == SmartSchoolBLL.User.CurrentUser.UserId));
            var grades = SmartSchoolBLL.Grade.GetGrades().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId).ToList();
            var studentClass = SmartSchoolBLL.StudentClass.GetStudentClasses().
                    FirstOrDefault(sc => sc.StudentId == SmartSchoolBLL.User.CurrentUser.UserId);
            var _class = SmartSchoolBLL.Class.GetClasses().
                    FirstOrDefault(c => c.ClassId == studentClass.ClassId);
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

        private ObservableCollection<Grade> grades;
        public ObservableCollection<Grade> Grades
        {
            get
            {
                if (selectedSubject == null)
                    return new ObservableCollection<Grade>();
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects().
                        FirstOrDefault(ss => (ss.SubjectId == selectedSubject.SubjectId && ss.StudentId == SmartSchoolBLL.User.CurrentUser.UserId));
                var _grades = SmartSchoolBLL.Grade.GetGrades().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId).ToList();
                grades = new ObservableCollection<Grade>(_grades);
                return grades;
            }
            set
            {
                grades = value;
                NotifyPropertyChanged(nameof(Grades));
            }
        }

        private ObservableCollection<Absence> absences;
        public ObservableCollection<Absence> Absences
        {
            get
            {
                if (selectedSubject == null)
                    return new ObservableCollection<Absence>();
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects().
                        FirstOrDefault(ss => (ss.SubjectId == selectedSubject.SubjectId && ss.StudentId == SmartSchoolBLL.User.CurrentUser.UserId));
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

        private ObservableCollection<TeachingMaterial> teachingMaterials;
        public ObservableCollection<TeachingMaterial> TeachingMaterials
        {
            get
            {
                if(selectedSubject == null)
                    return new ObservableCollection<TeachingMaterial>();
                var studentClass = SmartSchoolBLL.StudentClass.GetStudentClasses().FirstOrDefault(sc => sc.StudentId == SmartSchoolBLL.User.CurrentUser.UserId);
                var teacherClasses = SmartSchoolBLL.TeacherClass.GetTeacherClasses().Where(tc => tc.ClassId == studentClass.ClassId).ToList();
                var _teachingMaterials = SmartSchoolBLL.TeachingMaterial.GetTeachingMaterials().Where(tm => (teacherClasses.Any(tc => tc.TeacherClassId == tm.TeacherClassId) && tm.SubjectId == selectedSubject.SubjectId));
                teachingMaterials = new ObservableCollection<TeachingMaterial>(_teachingMaterials);
                return teachingMaterials;
            }
            private set
            {
                teachingMaterials = value;
                NotifyPropertyChanged(nameof(TeachingMaterials));
            }
        }
    }
}