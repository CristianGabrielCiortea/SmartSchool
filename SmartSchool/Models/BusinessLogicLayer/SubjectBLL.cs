using SmartSchool.Exceptions;
using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class SubjectBLL
    {
        public ObservableCollection<Subject> SubjectList { get; set; }

        private SubjectDAL _subjectDAL = new SubjectDAL();

        public SubjectBLL() 
        {
            SubjectList = _subjectDAL.GetAllSubjects();
        }

        public void AddSubject(Subject subject)
        {
            if(string.IsNullOrEmpty(subject.Name))
            {
                throw new SmartSchoolException("Subject name cannot be null!");
            }
            _subjectDAL.AddSubject(subject);
            SubjectList = _subjectDAL.GetAllSubjects();
        }

        public void DeleteSubject(Subject subject)
        {
            if (subject == null)
                throw new SmartSchoolException("Subject is null!");

            _subjectDAL.DeleteSubject(subject);
            SubjectList = _subjectDAL.GetAllSubjects();
        }

        public void ModifySubject(Subject subject)
        {
            if (subject == null)
                throw new SmartSchoolException("Subject is null!");

            _subjectDAL.ModifySubject(subject);
        }

        public ObservableCollection<Subject> GetSubjects()
        {
            return _subjectDAL.GetAllSubjects();
        }
    }
}