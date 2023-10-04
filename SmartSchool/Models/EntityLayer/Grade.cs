using System;

namespace SmartSchool.Models.EntityLayer
{
    public class Grade : BasePropertyChanged
    {
        private int gradeId;
        public int GradeId
        {
            get
            {
                return gradeId;
            }
            set
            {
                gradeId = value;
                NotifyPropertyChanged(nameof(GradeId));
            }
        }

        private int studentSubjectId;
        public int StudentSubjectId
        {
            get
            {
                return studentSubjectId;
            }
            set
            {
                studentSubjectId = value;
                NotifyPropertyChanged(nameof(StudentSubjectId));
            }
        }

        private int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                NotifyPropertyChanged(nameof(Value));
            }
        }

        private DateTime date;
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
                NotifyPropertyChanged(nameof(Date));
            }
        }

        private bool isSemesterExamGrade;
        public bool IsSemesterExamGrade
        {
            get
            {
                return isSemesterExamGrade;
            }
            set
            {
                isSemesterExamGrade = value;
                NotifyPropertyChanged(nameof(IsSemesterExamGrade));
            }
        }

        private int semester;
        public int Semester
        {
            get
            {
                return semester;
            }
            set
            {
                semester = value;
                NotifyPropertyChanged(nameof(Semester));
            }
        }
    }
}