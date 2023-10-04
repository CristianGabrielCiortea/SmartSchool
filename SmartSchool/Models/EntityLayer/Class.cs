namespace SmartSchool.Models.EntityLayer
{
    public class Class : BasePropertyChanged
    {
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

        private int formTeacherId;

        public int FormTeacherId
        {
            get
            {
                return formTeacherId;
            }
            set
            {
                formTeacherId = value;
                NotifyPropertyChanged(nameof(FormTeacherId));
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