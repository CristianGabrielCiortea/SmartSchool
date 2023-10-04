using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class GradeBLL
    {
        private GradeDAL _gradeDAL = new GradeDAL();

        public void AddGrade(Grade grade)
        {
            _gradeDAL.AddGrade(grade);
        }

        public void DeleteGrade(Grade grade)
        {
            _gradeDAL.DeleteGrade(grade);
        }

        //public void ModifyGrade(Grade grade)
        //{
        //    _gradeDAL.ModifyGrade(grade);
        //}

        public ObservableCollection<Grade> GetGrades()
        {
            return _gradeDAL.GetAllGrades();
        }
    }
}