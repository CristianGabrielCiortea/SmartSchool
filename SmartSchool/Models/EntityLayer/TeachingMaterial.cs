namespace SmartSchool.Models.EntityLayer
{
    public class TeachingMaterial : BasePropertyChanged
    {
        private int teachingMaterialId;
        public int TeachingMaterialId
        {
            get
            {
                return teachingMaterialId;
            }
            set
            {
                teachingMaterialId = value;
                NotifyPropertyChanged(nameof(TeachingMaterialId));
            }
        }

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

        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                NotifyPropertyChanged(nameof(Text));
            }
        }
    }
}