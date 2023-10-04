using SmartSchool.Exceptions;
using SmartSchool.Models.DataAccessLayer;
using SmartSchool.Models.EntityLayer;
using SmartSchool.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SmartSchool.Models.BusinessLogicLayer
{
    public class UserBLL
    {
        public ObservableCollection<User> UserList { get; set; }

        private UserDAL _userDAL = new UserDAL();

        public User CurrentUser { get; set; }

        public UserBLL() 
        {
            UserList = _userDAL.GetAllUsers();
        }

        public void AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
                throw new SmartSchoolException("User has null values!");

            _userDAL.AddUser(user);
            UserList = _userDAL.GetAllUsers();
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new SmartSchoolException("User is null!");

            _userDAL.DeleteUser(user);
            UserList = _userDAL.GetAllUsers();
        }

        public void ModifyUser(User user)
        {
            if (user == null)
                throw new SmartSchoolException("User is null!");

            _userDAL.ModifyUser(user);
        }

        public ObservableCollection<User> GetUsers()
        {   
            return _userDAL.GetAllUsers();
        }

        public void Login(User _user)
        {
            foreach(User user in UserList)
            {
                if(user.Username == _user.Username && user.Password == _user.Password)
                {
                    CurrentUser = user;
                    switch (user.Role)
                    {
                        case Enums.Role.Elev:
                            StudentView studentView = new StudentView();
                            studentView.Show();
                            break;
                        case Enums.Role.Profesor:
                            TeacherView teacher = new TeacherView();
                            teacher.Show();
                            break;
                        case Enums.Role.Diriginte:
                            TeacherView teacher1 = new TeacherView();
                            teacher1.Show();
                            break;
                        case Enums.Role.Administrator:
                            AdministratorView administratorView = new AdministratorView();
                            administratorView.Show();
                            break;
                    }
                    Window activeWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Name == "LogInWindow");
                    activeWindow?.Close();
                }
            }
        }

    }
}