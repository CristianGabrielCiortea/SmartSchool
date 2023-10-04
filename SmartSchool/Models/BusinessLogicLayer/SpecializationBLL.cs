using SmartSchool.Exceptions;
using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class SpecializationBLL
    {
        public ObservableCollection<Specialization> SpecializationList { get; set; }

        private SpecializationDAL _specializationDAL = new SpecializationDAL();

        public SpecializationBLL()
        {
            SpecializationList = _specializationDAL.GetAllSpecializations();
        }

        public void AddSpecialization(Specialization specialization)
        {
            if (string.IsNullOrEmpty(specialization.Name))
            {
                throw new SmartSchoolException("Specialization name cannot be null!");
            }
            _specializationDAL.AddSpecialization(specialization);
            SpecializationList = _specializationDAL.GetAllSpecializations();
        }

        public void DeleteSpecialization(Specialization specialization)
        {
            if (specialization == null)
                throw new SmartSchoolException("Specialization is null!");

            _specializationDAL.DeleteSpecialization(specialization);
            SpecializationList = _specializationDAL.GetAllSpecializations();
        }

        public void ModifySpecialization(Specialization specialization)
        {
            if (specialization == null)
                throw new SmartSchoolException("Specialization is null!");

            _specializationDAL.ModifySpecialization(specialization);
        }

        public ObservableCollection<Specialization> GetSpecialization()
        {
            return _specializationDAL.GetAllSpecializations();
        }
    }
}
