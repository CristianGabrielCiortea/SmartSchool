using SmartSchool.Models.BusinessLogicLayer;
using SmartSchool.Models.EntityLayer;
using SmartSchool.ViewModels.Commands;
using SmartSchool.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SmartSchool.ViewModels
{
    public class TeacherViewModel : BasePropertyChanged
    {
        private ObservableCollection<Class> classes;

        public ObservableCollection<Class> Classes
        {
            get
            {
                var teacherClass = SmartSchoolBLL.TeacherClass.GetTeacherClasses().Where(tc => tc.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId).ToList();
                var _classes = SmartSchoolBLL.Class.GetClasses().Where(c => teacherClass.Any(tc => tc.ClassId == c.ClassId));
                classes = new ObservableCollection<Class>(_classes);
                return classes;
            }
            set
            {
                classes = value;
                NotifyPropertyChanged(nameof(Classes));
            }
        }

        private Class selectedClass;
        public Class SelectedClass
        {
            get
            {
                return selectedClass;
            }
            set
            {
                selectedClass = value;
                NotifyPropertyChanged(nameof(SelectedClass));
                NotifyPropertyChanged(nameof(SelectedClassStudents));
                NotifyPropertyChanged(nameof(HasExam));
                NotifyPropertyChanged(nameof(TeachingMaterials));
            }
        }

        private ObservableCollection<User> selectedClassStudents;
        public ObservableCollection<User> SelectedClassStudents
        {
            get
            {
                if (selectedClass == null)
                    return new ObservableCollection<User>();
                var studentClasses = SmartSchoolBLL.StudentClass.GetStudentClasses();
                var _students = SmartSchoolBLL.User.GetUsers().Where(u => studentClasses.Any(sc => (sc.ClassId == selectedClass.ClassId && sc.StudentId == u.UserId)));
                selectedClassStudents = new ObservableCollection<User>(_students);
                return selectedClassStudents;
            }
            set
            {
                selectedClassStudents = value;
                NotifyPropertyChanged(nameof(SelectedClassStudents));
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
                NotifyPropertyChanged(nameof(Grades));
            }
        }

        private ObservableCollection<Absence> absences;
        public ObservableCollection<Absence> Absences
        {
            get
            {
                if (selectedStudent == null)
                    return new ObservableCollection<Absence>();
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().
                    FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects().
                    FirstOrDefault(ss => (ss.SubjectId == teacherSubject.SubjectId && ss.StudentId == SelectedStudent.UserId));
                var _absences = SmartSchoolBLL.Absence.GetAbsences().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId);
                absences = new ObservableCollection<Absence>(_absences);
                return absences;
            }
            set
            {
                absences = value;
                NotifyPropertyChanged(nameof(Absences));
            }
        }

        private int absenceSemester;
        public int AbsenceSemester
        {
            get
            {
                return absenceSemester;
            }
            set
            {
                absenceSemester = value;
                NotifyPropertyChanged(nameof(AbsenceSemester));
            }
        }

        private Absence selectedAbsence;
        public Absence SelectedAbsence
        {
            get
            {
                return selectedAbsence;
            }
            set
            {
                selectedAbsence = value;
                NotifyPropertyChanged(nameof(SelectedAbsence));
            }
        }

        private ICommand addAbsenceCommand;
        public ICommand AddAbsenceCommand
        {
            get
            {
                if (addAbsenceCommand == null)
                {
                    addAbsenceCommand = new RelayCommand<Absence>(AddAbsence);
                }
                return addAbsenceCommand;
            }
        }

        private void AddAbsence(object param)
        {
            if (selectedClass == null)
                return;
            if (selectedStudent == null)
                return;
            if (absenceSemester == 1 || absenceSemester == 2)
            {
                Absence absence = new Absence();
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().
                        FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects().
                    FirstOrDefault(ss => (ss.SubjectId == teacherSubject.SubjectId && ss.StudentId == SelectedStudent.UserId));
                absence.StudentSubjectId = studentSubject.StudentSubjectId;
                absence.Semester = absenceSemester;
                absence.Date = DateTime.Now;
                absence.IsJustified = false;
                SmartSchoolBLL.Absence.AddAbsence(absence);
                NotifyPropertyChanged(nameof(Absences));
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
            if (selectedClass == null)
                return;
            if (selectedStudent == null)
                return;
            if (selectedAbsence == null)
                return;
            selectedAbsence.IsJustified = true;
            SmartSchoolBLL.Absence.ModifyAbsence(selectedAbsence);
            NotifyPropertyChanged(nameof(Absences));
        }

        private ObservableCollection<Grade> grades;
        public ObservableCollection<Grade> Grades
        {
            get
            {
                if (selectedStudent == null)
                    return new ObservableCollection<Grade>();
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().
                    FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects().
                    FirstOrDefault(ss => (ss.SubjectId == teacherSubject.SubjectId && ss.StudentId == SelectedStudent.UserId));
                var _grades = SmartSchoolBLL.Grade.GetGrades().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId);
                grades = new ObservableCollection<Grade>(_grades);
                return grades;
            }
            set
            {
                grades = value;
                NotifyPropertyChanged(nameof(Grades));
            }
        }

        private int gradeSemester;
        public int GradeSemester
        {
            get
            {
                return gradeSemester;
            }
            set
            {
                gradeSemester = value;
                NotifyPropertyChanged(nameof(GradeSemester));
            }
        }

        private int gradeValue;
        public int GradeValue
        {
            get
            {
                return gradeValue;
            }
            set
            {
                gradeValue = value;
                NotifyPropertyChanged(nameof(GradeValue));
            }
        }

        private Grade selectedGrade;
        public Grade SelectedGrade
        {
            get
            {
                return selectedGrade;
            }
            set
            {
                selectedGrade = value;
                NotifyPropertyChanged(nameof(SelectedGrade));
            }
        }

        private bool isExam;
        public bool IsExam
        {
            get
            {
                return isExam;
            }
            set
            {
                isExam = value;
                NotifyPropertyChanged(nameof(IsExam));
            }
        }

        private bool hasExam;

        public bool HasExam
        {
            get
            {
                if (selectedClass == null)
                    return false;
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().
                   FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var specializationSubject = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects().
                    FirstOrDefault(ss => (ss.SubjectId == teacherSubject.SubjectId && ss.SpecializationId == selectedClass.SpecializationId));
                if (specializationSubject.HasSemesterExam)
                    hasExam = true;
                else
                    hasExam = false;
                return hasExam;
            }
            set
            {
                hasExam = value;
                NotifyPropertyChanged(nameof(HasExam));
            }
        }

        private ICommand addGradeCommand;
        public ICommand AddGradeCommand
        {
            get
            {
                if (addGradeCommand == null)
                {
                    addGradeCommand = new RelayCommand<Grade>(AddGrade);
                }
                return addGradeCommand;
            }
        }

        private void AddGrade(object param)
        {
            if (selectedClass == null)
                return;
            if (selectedStudent == null)
                return;
            if (gradeValue < 1 || gradeValue > 10)
                return;
            if (gradeSemester == 1 || gradeSemester == 2)
            {
                Grade grade = new Grade();
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().
                        FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects().
                    FirstOrDefault(ss => (ss.SubjectId == teacherSubject.SubjectId && ss.StudentId == SelectedStudent.UserId));
                grade.StudentSubjectId = studentSubject.StudentSubjectId;
                grade.Date = DateTime.Now;
                grade.Semester = gradeSemester;
                grade.Value = gradeValue;
                grade.IsSemesterExamGrade = isExam;
                SmartSchoolBLL.Grade.AddGrade(grade);
                NotifyPropertyChanged(nameof(Grades));
            }
        }

        private ICommand deleteGradeCommand;
        public ICommand DeleteGradeCommand
        {
            get
            {
                if (deleteGradeCommand == null)
                {
                    deleteGradeCommand = new RelayCommand<Grade>(DeleteGrade);
                }
                return deleteGradeCommand;
            }
        }

        private void DeleteGrade(object param)
        {
            if (selectedClass == null)
                return;
            if (selectedStudent == null)
                return;
            if (selectedGrade == null)
                return;
            SmartSchoolBLL.Grade.DeleteGrade(selectedGrade);
            NotifyPropertyChanged(nameof(Grades));
        }

        private ObservableCollection<TeachingMaterial> teachingMaterials;
        public ObservableCollection<TeachingMaterial> TeachingMaterials
        {
            get
            {
                if (selectedClass == null)
                    return new ObservableCollection<TeachingMaterial>();
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().
                       FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var teacherClass = SmartSchoolBLL.TeacherClass.GetTeacherClasses().
                        Where(tc => tc.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId).ToList();
                var selectedTeacherClass = teacherClass.FirstOrDefault(tc => tc.ClassId == selectedClass.ClassId);
                var _teachingMaterials = SmartSchoolBLL.TeachingMaterial.GetTeachingMaterials().
                        Where(tm => (tm.TeacherClassId == selectedTeacherClass.TeacherClassId && tm.SubjectId == teacherSubject.SubjectId));
                teachingMaterials = new ObservableCollection<TeachingMaterial>(_teachingMaterials);
                return teachingMaterials;
            }
            set
            {
                teachingMaterials = value;
                NotifyPropertyChanged(nameof(TeachingMaterials));
            }
        }

        private TeachingMaterial selectedTeachingMaterial;
        public TeachingMaterial SelectedTeachingMaterial
        {
            get
            {
                return selectedTeachingMaterial;
            }
            set
            {
                selectedTeachingMaterial = value;
                NotifyPropertyChanged(nameof(SelectedTeachingMaterial));
                NotifyPropertyChanged(nameof(SelectedTeachingMaterialText));
            }
        }

        private string selectedTeachingMaterialText;

        public string SelectedTeachingMaterialText
        {
            get
            {
                if (selectedTeachingMaterial == null)
                    return string.Empty;
                selectedTeachingMaterialText = selectedTeachingMaterial.Text;
                return selectedTeachingMaterialText;
            }
            set
            {
                selectedTeachingMaterialText = value;
                NotifyPropertyChanged(nameof(SelectedTeachingMaterialText));
            }
        }

        private string newText;
        public string NewText
        {
            get { return newText; }
            set
            {
                newText = value;
                NotifyPropertyChanged(nameof(NewText));
            }
        }

        private ICommand addMaterialCommand;
        public ICommand AddMaterialCommand
        {
            get
            {
                if (addMaterialCommand == null)
                {
                    addMaterialCommand = new RelayCommand<TeachingMaterial>(AddTeachingMaterial);
                }
                return addMaterialCommand;
            }
        }

        private void AddTeachingMaterial(object param)
        {
            if (selectedClass == null)
                return;
            if (string.IsNullOrEmpty(newText))
                return;
            var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().
                       FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
            var teacherClass = SmartSchoolBLL.TeacherClass.GetTeacherClasses().
                    Where(tc => tc.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId).ToList();
            var selectedTeacherClass = teacherClass.FirstOrDefault(tc => tc.ClassId == selectedClass.ClassId);
            TeachingMaterial teachingMaterial = new TeachingMaterial();
            teachingMaterial.SubjectId = teacherSubject.SubjectId;
            teachingMaterial.TeacherClassId = selectedTeacherClass.TeacherClassId;
            teachingMaterial.Text = newText;
            newText = "";
            SmartSchoolBLL.TeachingMaterial.AddTeachingMaterial(teachingMaterial);
            NotifyPropertyChanged(nameof(TeachingMaterials));
            NotifyPropertyChanged(nameof(newText));
        }

        private ICommand deleteMaterialCommand;
        public ICommand DeleteMaterialCommand
        {
            get
            {
                if (deleteMaterialCommand == null)
                {
                    deleteMaterialCommand = new RelayCommand<TeachingMaterial>(DeleteTeachingMaterial);
                }
                return deleteMaterialCommand;
            }
        }

        private void DeleteTeachingMaterial(object param)
        {
            if (selectedClass == null)
                return;
            if (selectedTeachingMaterial == null)
                return;
            SmartSchoolBLL.TeachingMaterial.DeleteTeachingMaterial(selectedTeachingMaterial);
            NotifyPropertyChanged(nameof(TeachingMaterials));
        }

        private ICommand calculateAverageForFirstSemesterCommand;
        public ICommand CalculateAverageForFirstSemesterCommand
        {
            get
            {
                if (calculateAverageForFirstSemesterCommand == null)
                {
                    calculateAverageForFirstSemesterCommand = new RelayCommand<TeachingMaterial>(CalculateAverageForFirstSemester);
                }
                return calculateAverageForFirstSemesterCommand;
            }
        }

        private void CalculateAverageForFirstSemester(object param)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var student in selectedClassStudents)
            {
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects()
                    .FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                    .FirstOrDefault(ss => (ss.SubjectId == teacherSubject.SubjectId && ss.StudentId == student.UserId));
                var grades = SmartSchoolBLL.Grade.GetGrades().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId);

                if (hasExam)
                {
                    if (grades.Where(g => g.Semester == 1).Count() >= 4 && grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1)))
                    {
                        int examGrade = grades.FirstOrDefault(g => (g.IsSemesterExamGrade == true && g.Semester == 1)).Value;
                        double sum = grades.Where(g => (!g.IsSemesterExamGrade && g.Semester == 1)).Sum(g => g.Value);
                        double average = (sum / (grades.Where(g => g.Semester == 1).Count() - 1)) * 3 + examGrade;
                        double finalAverage = average / 4;

                        sb.AppendLine($"{student.FullName}: {finalAverage.ToString("F2")}");
                    }
                    else
                    {
                        sb.AppendLine($"{student.FullName}: Not enough grades");
                    }
                }
                else
                {
                    if (grades.Where(g => g.Semester == 1).Count() >= 3 && !grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1)))
                    {
                        double average = grades.Where(g => g.Semester == 1).Average(g => g.Value);
                        sb.AppendLine($"{student.FullName}: {average.ToString("F2")}");
                    }
                    else
                    {
                        sb.AppendLine($"{student.FullName}: Not enough grades");
                    }
                }
            }

            averageText = sb.ToString();
            NotifyPropertyChanged(nameof(AverageText));

        }

        private ICommand calculateAverageForYearCommand;
        public ICommand CalculateAverageForYearCommand
        {
            get
            {
                if (calculateAverageForYearCommand == null)
                {
                    calculateAverageForYearCommand = new RelayCommand<TeachingMaterial>(CalculateAverageForYear);
                }
                return calculateAverageForYearCommand;
            }
        }

        private void CalculateAverageForYear(object param)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var student in selectedClassStudents)
            {
                var teacherSubject = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects()
                    .FirstOrDefault(ts => ts.TeacherId == SmartSchoolBLL.User.CurrentUser.UserId);
                var studentSubject = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                    .FirstOrDefault(ss => (ss.SubjectId == teacherSubject.SubjectId && ss.StudentId == student.UserId));
                var grades = SmartSchoolBLL.Grade.GetGrades().Where(a => a.StudentSubjectId == studentSubject.StudentSubjectId);

                if (hasExam)
                {
                    if (grades.Where(g => g.Semester == 1).Count() >= 4 && grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1))
                        && grades.Where(g => g.Semester == 2).Count() >= 4 && grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 2)))
                    {
                        int examGrade1 = grades.FirstOrDefault(g => (g.IsSemesterExamGrade == true && g.Semester == 1)).Value;
                        double sum1 = grades.Where(g => (!g.IsSemesterExamGrade && g.Semester == 1)).Sum(g => g.Value);
                        double average1 = (sum1 / (grades.Where(g => g.Semester == 1).Count() - 1)) * 3 + examGrade1;
                        double finalAverage1 = average1 / 4;

                        int examGrade2 = grades.FirstOrDefault(g => (g.IsSemesterExamGrade == true && g.Semester == 2)).Value;
                        double sum2 = grades.Where(g => (!g.IsSemesterExamGrade && g.Semester == 2)).Sum(g => g.Value);
                        double average2 = (sum2 / (grades.Where(g => g.Semester == 2).Count() - 1)) * 3 + examGrade2;
                        double finalAverage2 = average2 / 4;

                        double finalAverage = (finalAverage1 + finalAverage2) / 2;

                        sb.AppendLine($"{student.FullName}: {finalAverage.ToString("F2")}");
                    }
                    else
                    {
                        sb.AppendLine($"{student.FullName}: Not enough grades");
                    }
                }
                else
                {
                    if (grades.Where(g => g.Semester == 1).Count() >= 3 && !grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 1))
                        && grades.Where(g => g.Semester == 2).Count() >= 3 && !grades.Any(g => (g.IsSemesterExamGrade == true && g.Semester == 2)))
                    {
                        double average1 = grades.Where(g => g.Semester == 1).Average(g => g.Value);
                        double average2 = grades.Where(g => g.Semester == 2).Average(g => g.Value);

                        double finalAverage = (average1 + average2) / 2;

                        sb.AppendLine($"{student.FullName}: {finalAverage.ToString("F2")}");
                    }
                    else
                    {
                        sb.AppendLine($"{student.FullName}: Not enough grades");
                    }
                }
            }
            averageText = sb.ToString();
            NotifyPropertyChanged(nameof(AverageText));
        }

        private string averageText;
        public string AverageText
        {
            get
            {
                return averageText;
            }
            set
            {
                averageText = value;
                NotifyPropertyChanged(nameof(AverageText));
            }
        }

        private ICommand openFormTeacherViewCommand;
        public ICommand OpenFormTeacherViewCommand
        {
            get
            {
                if (openFormTeacherViewCommand == null)
                {
                    openFormTeacherViewCommand = new RelayCommand<TeacherSubject>(OpenFormTeacherView);
                }
                return openFormTeacherViewCommand;
            }
        }

        private void OpenFormTeacherView(object param)
        {
            if (SmartSchoolBLL.Class.GetClasses().Any(c => c.FormTeacherId == SmartSchoolBLL.User.CurrentUser.UserId))
            {
                FormTeacherView formTeacherView = new FormTeacherView();
                formTeacherView.Show();
            }
        }
    }
}