using SmartSchool.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace SmartSchool.Models.DataAccessLayer
{
    public class UserDAL
    {
        public ObservableCollection<User> GetAllUsers()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("GetAllUsers", con);
                ObservableCollection<User> result = new ObservableCollection<User>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User
                    {
                        UserId = (int)(reader[0]),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        LastName = reader.GetString(3),
                        FirstName = reader.GetString(4),
                        Role = (Enums.Role)(int)(reader[5])
                    };
                    result.Add(user);
                }
                reader.Close();
                return result;
            }
        }

        public void AddUser(User user)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramUsername = new SqlParameter("@username", user.Username);
                SqlParameter paramPassword = new SqlParameter("@password", user.Password);
                SqlParameter paramLastName = new SqlParameter("@lastName", user.LastName);
                SqlParameter paramFirstName = new SqlParameter("@firstName", user.FirstName);
                SqlParameter paramRole = new SqlParameter("@role", user.Role);

                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);
                cmd.Parameters.Add(paramLastName);
                cmd.Parameters.Add(paramFirstName);
                cmd.Parameters.Add(paramRole);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(User user)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@id", user.UserId);
                cmd.Parameters.Add(id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void ModifyUser(User user)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter id = new SqlParameter("@userID", user.UserId);
                SqlParameter paramUsername = new SqlParameter("@username", user.Username);
                SqlParameter paramPassword = new SqlParameter("@password", user.Password);
                SqlParameter paramLastName = new SqlParameter("@lastName", user.LastName);
                SqlParameter paramFirstName = new SqlParameter("@firstName", user.FirstName);
                SqlParameter paramRole = new SqlParameter("@role", user.Role);

                cmd.Parameters.Add(id);
                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);
                cmd.Parameters.Add(paramLastName);
                cmd.Parameters.Add(paramFirstName);
                cmd.Parameters.Add(paramRole);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
