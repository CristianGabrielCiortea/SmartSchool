namespace SmartSchool.Models.EntityLayer
{
    public class TeacherSubject : BasePropertyChanged
    {
        private int teacherSubjectId;
        public int TeacherSubjectId
        {
            get
            {
                return teacherSubjectId;
            }
            set
            {
                teacherSubjectId = value;
                NotifyPropertyChanged(nameof(TeacherSubjectId));
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
    }
}