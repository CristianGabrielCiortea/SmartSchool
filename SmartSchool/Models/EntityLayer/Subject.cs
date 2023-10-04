namespace SmartSchool.Models.EntityLayer
{
    public class Subject : BasePropertyChanged
    {
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

        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
    }
}