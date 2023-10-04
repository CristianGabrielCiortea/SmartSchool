using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class TeacherClassDAL
    {
        public ObservableCollection<TeacherClass> GetAllTeacherClass()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllTeacherClass", con);
                ObservableCollection<TeacherClass> result = new ObservableCollection<TeacherClass>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeacherClass teacherClass = new TeacherClass();
                    teacherClass.TeacherClassId = (int)(reader[0]);
                    teacherClass.TeacherId = (int)(reader[1]);
                    teacherClass.ClassId = (int)(reader[2]);

                    result.Add(teacherClass);
                }
                reader.Close();
                return result;
            }
        }

        public void AddTeacherClass(TeacherClass teacherClass)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddTeacherClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter teacherID = new SqlParameter("@teacherId", teacherClass.TeacherId);
                SqlParameter classID = new SqlParameter("@classId", teacherClass.ClassId);  

                cmd.Parameters.Add(teacherID);
                cmd.Parameters.Add(classID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTeacherClass(TeacherClass teacherClass)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteTeacherClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramTeacherId = new SqlParameter("@teacherId", teacherClass.TeacherId);
                SqlParameter paramClassId = new SqlParameter("@classId", teacherClass.ClassId);

                cmd.Parameters.Add(paramTeacherId);
                cmd.Parameters.Add(paramClassId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}