using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class TeacherSubjectBLL
    {
        public ObservableCollection<TeacherSubject> TeacherSubjectList { get; set; }

        public TeacherSubjectDAL _teacherSubjectDAL = new TeacherSubjectDAL();

        public TeacherSubjectBLL()
        {
            TeacherSubjectList = _teacherSubjectDAL.GetAllTeacherSubject();
        }

        public void AddTeacherSubject(TeacherSubject teacherSubject)
        {
            _teacherSubjectDAL.AddTeacherSubject(teacherSubject);
            TeacherSubjectList = _teacherSubjectDAL.GetAllTeacherSubject();
        }

        public void DeleteTeacherSubject(TeacherSubject teacherSubject)
        {
            _teacherSubjectDAL.DeleteTeacherSubject(teacherSubject);
            TeacherSubjectList = _teacherSubjectDAL.GetAllTeacherSubject();
        }

        public ObservableCollection<TeacherSubject> GetTeacherSubjects()
        {
            return _teacherSubjectDAL.GetAllTeacherSubject();
        }
    }
}