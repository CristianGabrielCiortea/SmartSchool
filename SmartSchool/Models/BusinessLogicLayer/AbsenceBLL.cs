using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class AbsenceBLL
    {
        private AbsenceDAL _absenceDAL = new AbsenceDAL();

        public void AddAbsence(Absence absence)
        {
            _absenceDAL.AddAbsence(absence);
        }

        public void DeleteAbsence(Absence absence)
        {
            _absenceDAL.DeleteAbsence(absence);
        }

        public void ModifyAbsence(Absence absence)
        {
            _absenceDAL.ModifyAbsence(absence);
        }

        public ObservableCollection<Absence> GetAbsences()
        {
            return _absenceDAL.GetAllAbsences();
        }
    }
}