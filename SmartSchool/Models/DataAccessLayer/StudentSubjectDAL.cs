using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class StudentSubjectDAL
    {
        public ObservableCollection<StudentSubject> GetAllStudentSubject()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllStudentSubject", con);
                ObservableCollection<StudentSubject> result = new ObservableCollection<StudentSubject>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudentSubject studentSubject = new StudentSubject();
                    studentSubject.StudentSubjectId = (int)(reader[0]);
                    studentSubject.StudentId = (int)(reader[1]);
                    studentSubject.SubjectId = (int)(reader[2]);
                    studentSubject.IsFirstSemesterEnded = (bool)(reader[3]);
                    studentSubject.IsSecondSemesterEnded = (bool)(reader[4]);

                    result.Add(studentSubject);
                }
                reader.Close();
                return result;
            }
        }

        public void AddStudentSubject(StudentSubject studentSubject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddStudentSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter studentIdParam = new SqlParameter("@studentID", studentSubject.StudentId);
                SqlParameter subjectIdParam = new SqlParameter("@subjectID", studentSubject.SubjectId);
                SqlParameter isFirstSemesterEndedParam = new SqlParameter("@isFirstSemesterEnded", false);
                SqlParameter isSecondSemesterEndedParam = new SqlParameter("@isSecondSemesterEnded", false);

                cmd.Parameters.Add(studentIdParam);
                cmd.Parameters.Add(subjectIdParam);
                cmd.Parameters.Add(isFirstSemesterEndedParam);
                cmd.Parameters.Add(isSecondSemesterEndedParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteStudentSubject(StudentSubject studentSubject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteStudentSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter studentIdParam = new SqlParameter("@studentId", studentSubject.StudentId);
                SqlParameter subjectIdParam = new SqlParameter("@subjectId", studentSubject.SubjectId);


                cmd.Parameters.Add(studentIdParam);
                cmd.Parameters.Add(subjectIdParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifyStudentSubject(StudentSubject studentSubject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyStudentSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter studentIdParam = new SqlParameter("@studentId", studentSubject.StudentId);
                SqlParameter subjectIdParam = new SqlParameter("@subjectId", studentSubject.SubjectId);
                SqlParameter isFirstSemesterEndedParam = new SqlParameter("@isFirstSemesterEnded", studentSubject.IsFirstSemesterEnded);
                SqlParameter isSecondSemesterEndedParam = new SqlParameter("@isSecondSemesterEnded", studentSubject.IsSecondSemesterEnded);

                cmd.Parameters.Add(studentIdParam);
                cmd.Parameters.Add(subjectIdParam);
                cmd.Parameters.Add(isFirstSemesterEndedParam);
                cmd.Parameters.Add(isSecondSemesterEndedParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}