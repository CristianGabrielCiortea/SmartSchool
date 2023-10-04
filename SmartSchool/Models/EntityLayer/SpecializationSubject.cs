namespace SmartSchool.Models.EntityLayer
{
    public class SpecializationSubject : BasePropertyChanged
    {

        private int specializationSubjectId;

        public int SpecializationSubjectId
        {
            get
            {
                return specializationSubjectId;
            }
            set
            {
                specializationSubjectId = value;
                NotifyPropertyChanged(nameof(SpecializationSubjectId));
            }
        }

        private int specializationId;

        public int SpecializationId
        {
            get
            {
                return specializationId;
            }
            set
            {
                specializationId = value;
                NotifyPropertyChanged(nameof(SpecializationId));
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

        private bool hasSemesterExam;

        public bool HasSemesterExam
        {
            get
            {
                return hasSemesterExam;
            }
            set
            {
                hasSemesterExam = value;
                NotifyPropertyChanged(nameof(HasSemesterExam));
            }
        }
    }
}