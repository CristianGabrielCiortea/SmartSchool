using System;

namespace SmartSchool.Models.EntityLayer
{
    public class Absence : BasePropertyChanged
    {
        private int absenceID;
        public int AbsenceID
        {
            get
            {
                return absenceID;
            }
            set
            {
                absenceID = value;
                NotifyPropertyChanged(nameof(AbsenceID));
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

        private bool isJustified;

        public bool IsJustified
        {
            get
            {
                return isJustified;
            }
            set
            {
                isJustified = value;
                NotifyPropertyChanged(nameof(IsJustified));
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