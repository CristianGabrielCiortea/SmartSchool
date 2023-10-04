using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class StudentClassDAL
    {
        public ObservableCollection<StudentClass> GetAllStudentClass()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllStudentClass", con);
                ObservableCollection<StudentClass> result = new ObservableCollection<StudentClass>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    StudentClass studentClass = new StudentClass();
                    studentClass.StudentClassId = (int)(reader[0]);
                    studentClass.StudentId = (int)(reader[1]);
                    studentClass.ClassId = (int)(reader[2]);

                    result.Add(studentClass);
                }
                reader.Close();
                return result;
            }
        }

        public void AddStudentClass(StudentClass studentClass)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddStudentClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter teacherID = new SqlParameter("@studentId", studentClass.StudentId);
                SqlParameter classID = new SqlParameter("@classId", studentClass.ClassId);

                cmd.Parameters.Add(teacherID);
                cmd.Parameters.Add(classID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteStudentClass(StudentClass studentClass)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteStudentClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter classID = new SqlParameter("@classId", studentClass.ClassId);
                SqlParameter studentID = new SqlParameter("@studentId", studentClass.StudentId);


                cmd.Parameters.Add(classID);
                cmd.Parameters.Add(studentID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}