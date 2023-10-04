using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class TeacherSubjectDAL
    {
        public ObservableCollection<TeacherSubject> GetAllTeacherSubject()
        {
            using (SqlConnection con = DALHelper.Connection)
            {

                SqlCommand cmd = new SqlCommand("GetAllTeacherSubject", con);
                ObservableCollection<TeacherSubject> result = new ObservableCollection<TeacherSubject>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeacherSubject teacherSubj = new TeacherSubject();
                    teacherSubj.TeacherSubjectId = (int)(reader[0]);
                    teacherSubj.TeacherId = (int)(reader[1]);
                    teacherSubj.SubjectId = (int)(reader[2]);

                    result.Add(teacherSubj);
                }
                reader.Close();
                return result;
            }
        }
        public void AddTeacherSubject(TeacherSubject teacherSubject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddTeacherSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter teacherId = new SqlParameter("@teacherId", teacherSubject.TeacherId);
                SqlParameter subjectId = new SqlParameter("@subjectId", teacherSubject.SubjectId);

                cmd.Parameters.Add(teacherId);
                cmd.Parameters.Add(subjectId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTeacherSubject(TeacherSubject teacherSubject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteTeacherSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter teacherId = new SqlParameter("@teacherId", teacherSubject.TeacherId);
                SqlParameter subjectId = new SqlParameter("@subjectId", teacherSubject.SubjectId);

                cmd.Parameters.Add(teacherId);
                cmd.Parameters.Add(subjectId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}