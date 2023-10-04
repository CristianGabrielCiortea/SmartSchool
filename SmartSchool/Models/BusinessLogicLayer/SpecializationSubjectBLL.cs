using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class SpecializationSubjectBLL
    {
        public ObservableCollection<SpecializationSubject> SpecializationSubjectList { get; set; }

        private SpecializationSubjectDAL _specializationSubjectDAL = new SpecializationSubjectDAL();

        public SpecializationSubjectBLL()
        {
            SpecializationSubjectList = _specializationSubjectDAL.GetAllSpecializationSubject();
        }

        public void AddSpecializationSubject(SpecializationSubject specializationSubject)
        {
            _specializationSubjectDAL.AddSpecializationSubject(specializationSubject);
            SpecializationSubjectList = _specializationSubjectDAL.GetAllSpecializationSubject();
        }
        public void DeleteSpecializationSubject(SpecializationSubject specializationSubject)
        {
            _specializationSubjectDAL.DeleteSpecializationSubject(specializationSubject);
            SpecializationSubjectList = _specializationSubjectDAL.GetAllSpecializationSubject();
        }

        public ObservableCollection<SpecializationSubject> GetSpecializationSubjects()
        {
            return _specializationSubjectDAL.GetAllSpecializationSubject();
        }
    }
}