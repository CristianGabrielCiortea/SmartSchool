using SmartSchool.Exceptions;
using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class ClassBLL
    {
        private ClassDAL _classDAL = new ClassDAL();

        public void AddClass(Class newClass)
        {
            if(string.IsNullOrEmpty(newClass.Name))
            {
                throw new SmartSchoolException("Class has null values!");
            }
            _classDAL.AddClass(newClass);
        }

        public void DeleteClass(Class newClass)
        {
            if (string.IsNullOrEmpty(newClass.Name))
            {
                throw new SmartSchoolException("Class has null values!");
            }
            _classDAL.DeleteClass(newClass);
        }

        public void ModifyClass(Class newClass)
        {
            if (newClass == null)
            {
                throw new SmartSchoolException("Class is null!");
            }
            _classDAL.ModifyClass(newClass);
        }

        public ObservableCollection<Class> GetClasses()
        {
            return _classDAL.GetAllClasses();
        }
    }
}