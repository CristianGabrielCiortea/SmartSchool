using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class SpecializationSubjectDAL
    {
        public ObservableCollection<SpecializationSubject> GetAllSpecializationSubject()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllSpecializationSubject", con);
                ObservableCollection<SpecializationSubject> result = new ObservableCollection<SpecializationSubject>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SpecializationSubject specializationSubject = new SpecializationSubject();
                    specializationSubject.SpecializationSubjectId = (int)(reader[0]);
                    specializationSubject.SubjectId = (int)(reader[1]);
                    specializationSubject.SpecializationId = (int)(reader[2]);
                    specializationSubject.HasSemesterExam = (bool)(reader[3]);

                    result.Add(specializationSubject);
                }
                reader.Close();
                return result;
            }
        }

        public void AddSpecializationSubject(SpecializationSubject specializationSubject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddSpecializationSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter subjectIdParam = new SqlParameter("@subjectId", specializationSubject.SubjectId);
                SqlParameter specializationIdParam = new SqlParameter("@specializationId", specializationSubject.SpecializationId);
                SqlParameter hasSemesterExamParam = new SqlParameter("@hasSemesterExam", specializationSubject.HasSemesterExam);

                cmd.Parameters.Add(subjectIdParam);
                cmd.Parameters.Add(specializationIdParam);
                cmd.Parameters.Add(hasSemesterExamParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSpecializationSubject(SpecializationSubject specializationSubject)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteSpecializationSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter specializationIdParam = new SqlParameter("@specializationId", specializationSubject.SpecializationId);
                SqlParameter subjectIdParam = new SqlParameter("@subjectId", specializationSubject.SubjectId);

                cmd.Parameters.Add(specializationIdParam);
                cmd.Parameters.Add(subjectIdParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}