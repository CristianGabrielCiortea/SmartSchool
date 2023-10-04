using SmartSchool.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class AbsenceDAL
    {
        public ObservableCollection<Absence> GetAllAbsences()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllAbsences", con);
                ObservableCollection<Absence> result = new ObservableCollection<Absence>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Absence absence = new Absence();
                    absence.AbsenceID = (int)(reader[0]);
                    absence.StudentSubjectId = (int)(reader[1]);
                    absence.Date = (DateTime)(reader[2]);
                    absence.IsJustified = (bool)(reader[3]);
                    absence.Semester = (int)(reader[4]);

                    result.Add(absence);
                }
                reader.Close();
                return result;
            }
        }

        public void AddAbsence(Absence absence)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddAbsence", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter studentSubjectIdParam = new SqlParameter("@studentSubjectId", absence.StudentSubjectId);
                SqlParameter dateParam = new SqlParameter("@date", absence.Date);
                SqlParameter isJustifiedParam = new SqlParameter("@isJustified", absence.IsJustified);
                SqlParameter semesterParam = new SqlParameter("@semester", absence.Semester);

                cmd.Parameters.Add(studentSubjectIdParam);
                cmd.Parameters.Add(dateParam);
                cmd.Parameters.Add(isJustifiedParam);
                cmd.Parameters.Add(semesterParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAbsence(Absence absence)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteAbsence", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter absenceIdParam = new SqlParameter("@gradeId", absence.AbsenceID);

                cmd.Parameters.Add(absenceIdParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifyAbsence(Absence absence)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyAbsence", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter absenceIdParam = new SqlParameter("@absenceId", absence.AbsenceID);
                SqlParameter isJustifiedParam = new SqlParameter("@isJustified", absence.IsJustified);

                cmd.Parameters.Add(absenceIdParam);
                cmd.Parameters.Add(isJustifiedParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}