using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class SubjectDAL
    {
        public ObservableCollection<Subject> GetAllSubjects()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllSubjects", con);
                ObservableCollection<Subject> result = new ObservableCollection<Subject>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Subject subject = new Subject();
                    subject.SubjectId = (int)(reader[0]);
                    subject.Name = reader.GetString(1);

                    result.Add(subject);
                }
                reader.Close();
                return result;
            }
        }

        public void AddSubject(Subject subject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@name", subject.Name);

                cmd.Parameters.Add(paramName);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSubject(Subject subject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteSubject", con);  
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@subjectId", subject.SubjectId);
                cmd.Parameters.Add(id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void ModifySubject(Subject subject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifySubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@subjectId", subject.SubjectId);
                SqlParameter paramName = new SqlParameter("@name", subject.Name);

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(paramName);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}