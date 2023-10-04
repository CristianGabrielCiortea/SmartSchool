using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class SpecializationDAL
    {
        public ObservableCollection<Specialization> GetAllSpecializations()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllSpecializations", con);
                ObservableCollection<Specialization> result = new ObservableCollection<Specialization>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Specialization specialization = new Specialization();
                    specialization.SpecializationId = (int)(reader[0]);
                    specialization.Name = reader.GetString(1);

                    result.Add(specialization);
                }
                reader.Close();
                return result;
            }
        }

        public void AddSpecialization(Specialization specialization)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddSpecialization", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@name", specialization.Name);

                cmd.Parameters.Add(paramName);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSpecialization(Specialization specialization)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteSpecialization", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@specializationId", specialization.SpecializationId);
                cmd.Parameters.Add(id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void ModifySpecialization(Specialization specialization)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifySpecialization", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@specializationId", specialization.SpecializationId);
                SqlParameter paramName = new SqlParameter("@name", specialization.Name);

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(paramName);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
