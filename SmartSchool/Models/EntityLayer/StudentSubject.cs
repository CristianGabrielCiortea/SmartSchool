namespace SmartSchool.Models.EntityLayer
{
    public class StudentSubject : BasePropertyChanged
    {
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

        private int studentId;
        public int StudentId
        {
            get
            {
                return studentId;
            }
            set
            {
                studentId = value;
                NotifyPropertyChanged(nameof(StudentId));
            }
        }

        private int subjectId;

        public int SubjectId
        {
            get
            {
                return subjectId;
            }
            set
            {
                subjectId = value;
                NotifyPropertyChanged(nameof(SubjectId));
            }
        }

        private bool isFirstSemesterEnded;

        public bool IsFirstSemesterEnded
        {
            get
            {
                return isFirstSemesterEnded;
            }
            set
            {
                isFirstSemesterEnded = value;
                NotifyPropertyChanged(nameof(IsFirstSemesterEnded));
            }
        }

        private bool isSecondSemesterEnded;

        public bool IsSecondSemesterEnded
        {
            get
            {
                return isSecondSemesterEnded;
            }
            set
            {
                isSecondSemesterEnded = value;
                NotifyPropertyChanged(nameof(IsSecondSemesterEnded));
            }
        }
    }
}