using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class ClassDAL
    {
        public ObservableCollection<Class> GetAllClasses()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllClasses", con);
                ObservableCollection<Class> result = new ObservableCollection<Class>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Class newClass = new Class
                    {
                        ClassId = (int)(reader[0]),
                        SpecializationId = (int)(reader[1]),
                        FormTeacherId = (int)(reader[2]),
                        Name = reader.GetString(3),
                    };
                    result.Add(newClass);
                }
                reader.Close();
                return result;
            }
        }

        public void AddClass (Class newClass)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramSpecializationId = new SqlParameter("@specializationId", newClass.SpecializationId);
                SqlParameter paramFormTeacherId = new SqlParameter("@formTeacherId", newClass.FormTeacherId);
                SqlParameter paramName = new SqlParameter("@name", newClass.Name);

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramFormTeacherId);
                cmd.Parameters.Add(paramSpecializationId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteClass(Class newClass)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteCLass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@classId", newClass.ClassId);
                cmd.Parameters.Add(id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifyClass(Class newClass)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyClass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@classId", newClass.ClassId);
                SqlParameter paramName = new SqlParameter("@name", newClass.Name);
                SqlParameter paramSpecializationId = new SqlParameter("@specializationId", newClass.SpecializationId);
                SqlParameter paramFormTeacherId = new SqlParameter("@formTeacherId", newClass.FormTeacherId);

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramSpecializationId);
                cmd.Parameters.Add(paramFormTeacherId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}