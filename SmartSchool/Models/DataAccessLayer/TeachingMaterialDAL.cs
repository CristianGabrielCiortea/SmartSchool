using SmartSchool.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.Models.DataAccessLayer
{
    public class TeachingMaterialDAL
    {
        public ObservableCollection<TeachingMaterial> GetAllTeachingMaterials()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllTeachingMaterials", con);
                ObservableCollection<TeachingMaterial> result = new ObservableCollection<TeachingMaterial>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TeachingMaterial teachingMaterial = new TeachingMaterial();
                    teachingMaterial.TeachingMaterialId = (int)(reader[0]);
                    teachingMaterial.TeacherClassId = (int)(reader[1]);
                    teachingMaterial.SubjectId = (int)(reader[2]);
                    teachingMaterial.Text = (string)(reader[3]);

                    result.Add(teachingMaterial);
                }
                reader.Close();
                return result;
            }
        }

        public void AddTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddTeachingMaterial", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter teacherClassIdParam = new SqlParameter("@teacherClassId", teachingMaterial.TeacherClassId);
                SqlParameter subjectIdParam = new SqlParameter("@subjectID", teachingMaterial.SubjectId);
                SqlParameter textParam = new SqlParameter("@text", teachingMaterial.Text);

                cmd.Parameters.Add(teacherClassIdParam);
                cmd.Parameters.Add(subjectIdParam);
                cmd.Parameters.Add(textParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteTeachingMaterial", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter teachingMaterialIdParam = new SqlParameter("@teachingMaterialId", teachingMaterial.TeachingMaterialId);

                cmd.Parameters.Add(teachingMaterialIdParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifyTeachingMaterial(TeachingMaterial teachingMaterial)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyStudentSubject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter teachingMaterialIdParam = new SqlParameter("@teachingMaterialId", teachingMaterial.TeachingMaterialId);
                SqlParameter teacherClassIdParam = new SqlParameter("@teacherClassId", teachingMaterial.TeacherClassId);
                SqlParameter subjectIdParam = new SqlParameter("@subjectID", teachingMaterial.SubjectId);
                SqlParameter textParam = new SqlParameter("@text", teachingMaterial.Text);

                cmd.Parameters.Add(teachingMaterialIdParam);
                cmd.Parameters.Add(teacherClassIdParam);
                cmd.Parameters.Add(subjectIdParam);
                cmd.Parameters.Add(textParam);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}