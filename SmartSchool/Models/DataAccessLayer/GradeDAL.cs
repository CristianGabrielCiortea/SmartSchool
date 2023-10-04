using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using SmartSchool.Models.EntityLayer;
using System;

namespace SmartSchool.Models.DataAccessLayer
{
    internal class GradeDAL
    {
        public ObservableCollection<Grade> GetAllGrades()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllGrades", con);
                ObservableCollection<Grade> result = new ObservableCollection<Grade>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Grade grade = new Grade();
                    grade.GradeId = (int)(reader[0]);
                    grade.StudentSubjectId = (int)(reader[1]);
                    grade.Value = (int)(reader[2]);
                    grade.Date = (DateTime)(reader[3]);
                    grade.IsSemesterExamGrade = (bool)(reader[4]);
                    grade.Semester = (int)(reader[5]);

                    result.Add(grade);
                }
                reader.Close();
                return result;
            }
        }

        public void AddGrade(Grade grade)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddGrade", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter studentSubjectIdParam = new SqlParameter("@studentSubjectId", grade.StudentSubjectId);
                SqlParameter valueParam = new SqlParameter("@value", grade.Value);
                SqlParameter dateParam = new SqlParameter("@date", grade.Date);
                SqlParameter isSemesterExamGradeParam = new SqlParameter("@isSemesterExamGrade", grade.IsSemesterExamGrade);
                SqlParameter semesterParam = new SqlParameter("@semester", grade.Semester);

                cmd.Parameters.Add(studentSubjectIdParam);
                cmd.Parameters.Add(valueParam);
                cmd.Parameters.Add(dateParam);
                cmd.Parameters.Add(isSemesterExamGradeParam);
                cmd.Parameters.Add(semesterParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteGrade(Grade grade)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteGrade", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter gradeIdParam = new SqlParameter("@gradeId", grade.GradeId);

                cmd.Parameters.Add(gradeIdParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //public void ModifyGrade(Grade grade)
        //{
        //    using (SqlConnection con = DALHelper.Connection)
        //    {
        //        SqlCommand cmd = new SqlCommand("ModifyGrade", con);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        SqlParameter gradeIdParam = new SqlParameter("@gradeId", grade.GradeId);
        //        SqlParameter studentSubjectIdParam = new SqlParameter("@studentSubjectId", grade.StudentSubjectId);
        //        SqlParameter valueParam = new SqlParameter("@value", grade.Value);
        //        SqlParameter dateParam = new SqlParameter("@date", grade.Date);
        //        SqlParameter isSemesterExamGradeParam = new SqlParameter("@isSemesterExamGrade", grade.IsSemesterExamGrade);
        //        SqlParameter semesterParam = new SqlParameter("@semester", grade.Semester);

        //        cmd.Parameters.Add(gradeIdParam);
        //        cmd.Parameters.Add(studentSubjectIdParam);
        //        cmd.Parameters.Add(valueParam);
        //        cmd.Parameters.Add(dateParam);
        //        cmd.Parameters.Add(isSemesterExamGradeParam);
        //        cmd.Parameters.Add(semesterParam);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }
        //}
    }
}