using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class StudentSubjectBLL
    {
        public ObservableCollection<StudentSubject> StudentSubjectList { get; set; }

        public StudentSubjectDAL _studentSubjectDAL = new StudentSubjectDAL();

        public StudentSubjectBLL()
        {
            StudentSubjectList = _studentSubjectDAL.GetAllStudentSubject();
        }

        public void AddStudentSubject(StudentSubject studentSubject)
        {
            _studentSubjectDAL.AddStudentSubject(studentSubject);
            StudentSubjectList = _studentSubjectDAL.GetAllStudentSubject();
        }

        public void DeleteStudentSubject(StudentSubject studentSubject)
        {
            _studentSubjectDAL.DeleteStudentSubject(studentSubject);
            StudentSubjectList = _studentSubjectDAL.GetAllStudentSubject();
        }

        public void ModifyStudentSubject(StudentSubject studentSubject)
        {
            _studentSubjectDAL.ModifyStudentSubject(studentSubject);
            StudentSubjectList = _studentSubjectDAL.GetAllStudentSubject(); ;
        }

        public ObservableCollection<StudentSubject> GetStudentSubjects()
        {
            return _studentSubjectDAL.GetAllStudentSubject();
        }
    }
}