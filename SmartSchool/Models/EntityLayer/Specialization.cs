namespace SmartSchool.Models.EntityLayer
{
    public class Specialization : BasePropertyChanged
    {
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