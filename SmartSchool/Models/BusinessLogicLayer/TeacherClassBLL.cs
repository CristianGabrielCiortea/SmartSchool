using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class TeacherClassBLL
    {
        public ObservableCollection<TeacherClass> TeacherClassList { get; set; }

        public TeacherClassDAL _teacherClassDAL = new TeacherClassDAL();

        public TeacherClassBLL()
        {
            TeacherClassList = _teacherClassDAL.GetAllTeacherClass();
        }

        public void AddTeacherClass(TeacherClass teacherClass)
        {
            _teacherClassDAL.AddTeacherClass(teacherClass);
            TeacherClassList.Add(teacherClass);
        }
        public void DeleteTeacherClass(TeacherClass teacherClass)
        {
            _teacherClassDAL.DeleteTeacherClass(teacherClass);
            TeacherClassList.Remove(teacherClass);
        }

        public ObservableCollection<TeacherClass> GetTeacherClasses()
        {
            return _teacherClassDAL.GetAllTeacherClass();
        }
    }
}