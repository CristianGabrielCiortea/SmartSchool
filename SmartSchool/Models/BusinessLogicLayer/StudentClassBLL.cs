using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class StudentClassBLL
    {
        public ObservableCollection<StudentClass> StudentClassList { get; set; }

        public StudentClassDAL _studentClassDAL = new StudentClassDAL();

        public StudentClassBLL()
        {
            StudentClassList = _studentClassDAL.GetAllStudentClass();
        }

        public void AddStudentClass(StudentClass studentClass)
        {
            _studentClassDAL.AddStudentClass(studentClass);
            StudentClassList.Add(studentClass);
        }

        public void DeleteStudentClass(StudentClass studentClass)
        {
            _studentClassDAL.DeleteStudentClass(studentClass);
            StudentClassList.Remove(studentClass);
        }

        public ObservableCollection<StudentClass> GetStudentClasses()
        {
            return _studentClassDAL.GetAllStudentClass();
        }
    }
}