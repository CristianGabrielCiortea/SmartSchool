namespace SmartSchool.Models.EntityLayer
{
    public class TeacherClass : BasePropertyChanged
    {
        private int teacherClassId;
        public int TeacherClassId
        {
            get
            {
                return teacherClassId;
            }
            set
            {
                teacherClassId = value;
                NotifyPropertyChanged(nameof(TeacherClassId));
            }
        }

        private int teacherId;
        public int TeacherId
        {
            get
            {
                return teacherId;
            }
            set
            {
                teacherId = value;
                NotifyPropertyChanged(nameof(TeacherId));
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