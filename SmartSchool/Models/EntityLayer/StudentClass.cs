namespace SmartSchool.Models.EntityLayer
{
    public class StudentClass : BasePropertyChanged
    {

        private int studentClassId;
        public int StudentClassId
        {
            get
            {
                return studentClassId;
            }
            set
            {
                studentClassId = value;
                NotifyPropertyChanged(nameof(StudentClassId));
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

        private int classId;

        public int ClassId
        {
            get
            {
                return classId;
            }
            set
            {
                classId = value;
                NotifyPropertyChanged(nameof(ClassId));
            }
        }
    }
}