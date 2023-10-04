using SmartSchool.Enums;
using SmartSchool.Exceptions;
using SmartSchool.Models.BusinessLogicLayer;
using SmartSchool.Models.EntityLayer;
using SmartSchool.ViewModels.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartSchool.ViewModels
{
    public class AdministratorViewModel : BasePropertyChanged
    {
        private bool isClassesButtonChecked;
        public bool IsClassesButtonChecked
        {
            get { return isClassesButtonChecked; }
            set
            {
                isClassesButtonChecked = value;
                NotifyPropertyChanged(nameof(IsClassesButtonChecked));
            }
        }

        private bool isUsersButtonChecked;
        public bool IsUsersButtonChecked
        {
            get { return isUsersButtonChecked; }
            set
            {
                isUsersButtonChecked = value;
                NotifyPropertyChanged(nameof(IsUsersButtonChecked));
            }
        }

        private ObservableCollection<User> users;
        public ObservableCollection<User> Users
        {
            get
            {
                users = SmartSchoolBLL.User.GetUsers();
                return users;
            }
            set
            {
                users = value;
                NotifyPropertyChanged(nameof(Users));
            }
        }

        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                if (selectedUser != value)
                {
                    selectedUser = value;
                    NotifyPropertyChanged(nameof(SelectedUser));
                }
            }
        }

        private ICommand deleteUserCommand;

        public ICommand DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                {
                    deleteUserCommand = new RelayCommand<User>(DeleteUser);
                }
                return deleteUserCommand;
            }
        }

        private void DeleteUser(object param)
        {
            var studentClasses = SmartSchoolBLL.StudentClass.GetStudentClasses().Where(sc => sc.StudentId == SelectedUser.UserId).ToList();
            foreach (var studentClass in studentClasses)
            {
                SmartSchoolBLL.StudentClass.DeleteStudentClass(studentClass);
            }

            var studentSubjects = SmartSchoolBLL.StudentSubject.GetStudentSubjects().Where(ss => ss.StudentId == SelectedUser.UserId).ToList();
            foreach (var studentSubject in studentSubjects)
            {
                SmartSchoolBLL.StudentSubject.DeleteStudentSubject(studentSubject);
            }

            var teacherClasses = SmartSchoolBLL.TeacherClass.GetTeacherClasses().Where(tc => tc.TeacherId == SelectedUser.UserId).ToList();
            foreach (var teacherClass in teacherClasses)
            {
                SmartSchoolBLL.TeacherClass.DeleteTeacherClass(teacherClass);
            }

            var teacherSubjects = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects().Where(ts => ts.TeacherId == SelectedUser.UserId).ToList();
            foreach (var teacherSubject in teacherSubjects)
            {
                SmartSchoolBLL.TeacherSubject.DeleteTeacherSubject(teacherSubject);
            }

            SmartSchoolBLL.User.DeleteUser(SelectedUser);
            users.Remove(SelectedUser);
        }

        private ICommand editUserCommand;

        public ICommand EditUserCommand
        {
            get
            {
                if (editUserCommand == null)
                {
                    editUserCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(EditUser);
                }
                return editUserCommand;
            }
        }

        private void EditUser(DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is TextBox editedTextBox)
            {
                var newValue = editedTextBox.Text;
                var user = e.Row.DataContext as User;

                switch (e.Column.Header.ToString())
                {
                    case "FirstName":
                        user.FirstName = newValue;
                        break;
                    case "LastName":
                        user.LastName = newValue;
                        break;
                    case "Username":
                        if (SmartSchoolBLL.User.UserList.Where(u => u.Username == user.Username).Any())
                        {
                            editedTextBox.Text = user.Username;
                            return;
                        }
                        user.Username = newValue;
                        break;
                    case "Password":
                        user.Password = newValue;
                        break;
                }

                SmartSchoolBLL.User.ModifyUser(user);
            }
            else if (e.EditingElement is ComboBox editedComboBox)
            {
                var newValue = editedComboBox.SelectedItem;
                var user = e.Row.DataContext as User;

                if (e.Column.Header.ToString() == "Role")
                {
                    user.Role = (Role)Enum.Parse(typeof(Role), newValue.ToString());
                    SmartSchoolBLL.User.ModifyUser(user);
                }
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                NotifyPropertyChanged(nameof(FirstName));
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                NotifyPropertyChanged(nameof(LastName));
            }
        }

        private Role role;
        public Role Role
        {
            get { return role; }
            set
            {
                role = value;
                NotifyPropertyChanged(nameof(Role));
            }
        }

        private ICommand addUserCommand;
        public ICommand AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                {
                    addUserCommand = new RelayCommand<User>(AddUser);
                }
                return addUserCommand;
            }
        }

        private void AddUser(object param)
        {
            User newUser = new User();
            newUser.Username = username;
            newUser.Password = password;
            newUser.FirstName = firstName;
            newUser.LastName = lastName;
            newUser.Role = role;
            username = "";
            password = "";
            firstName = "";
            lastName = "";
            role = Role.Elev;
            NotifyPropertyChanged(nameof(Username));
            NotifyPropertyChanged(nameof(Password));
            NotifyPropertyChanged(nameof(Role));
            NotifyPropertyChanged(nameof(FirstName));
            NotifyPropertyChanged(nameof(LastName));
            if (SmartSchoolBLL.User.UserList.Where(u => u.Username == username).Any())
            {
                return;
            }
            try
            {
                SmartSchoolBLL.User.AddUser(newUser);
            }
            catch (SmartSchoolException ex)
            {
                return;
            }
            NotifyPropertyChanged(nameof(Users));
            NotifyPropertyChanged(nameof(Students));
            NotifyPropertyChanged(nameof(Teachers));
        }

        private bool isSpecializationButtonChecked;
        public bool IsSpecializationButtonChecked
        {
            get { return isSpecializationButtonChecked; }
            set
            {
                isSpecializationButtonChecked = value;
                NotifyPropertyChanged(nameof(IsSpecializationButtonChecked));
            }
        }

        private ObservableCollection<Specialization> specializations;
        public ObservableCollection<Specialization> Specializations
        {
            get
            {
                specializations = SmartSchoolBLL.Specialization.GetSpecialization();
                return specializations;
            }
            set
            {
                specializations = value;
                NotifyPropertyChanged(nameof(Specializations));
            }
        }

        private Specialization selectedSpecialization;

        public Specialization SelectedSpecialization
        {
            get { return selectedSpecialization; }
            set
            {
                if (selectedSpecialization != value)
                {
                    selectedSpecialization = value;
                    NotifyPropertyChanged(nameof(SelectedSpecialization));
                    NotifyPropertyChanged(nameof(SelectedSpecializationSubjects));
                }
            }
        }

        private ICommand editSpecializationCommand;

        public ICommand EditSpecializationCommand
        {
            get
            {
                if (editSpecializationCommand == null)
                {
                    editSpecializationCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(EditSpecialization);
                }
                return editSpecializationCommand;
            }
        }

        private void EditSpecialization(DataGridCellEditEndingEventArgs e)
        {
            var editedTextBox = e.EditingElement as TextBox;
            var newValue = editedTextBox.Text;
            var specialization = e.Row.DataContext as Specialization;
            specialization.Name = newValue;

            SmartSchoolBLL.Specialization.ModifySpecialization(specialization);
        }

        private string specializationName;
        public string SpecializationName
        {
            get { return specializationName; }
            set
            {
                specializationName = value;
                NotifyPropertyChanged(nameof(SpecializationName));
            }
        }

        private ICommand addSpecializationCommand;
        public ICommand AddSpecializationCommand
        {
            get
            {
                if (addSpecializationCommand == null)
                {
                    addSpecializationCommand = new RelayCommand<Specialization>(AddSpecialization);
                }
                return addSpecializationCommand;
            }
        }

        private void AddSpecialization(object param)
        {
            Specialization newSpecialization = new Specialization();
            newSpecialization.Name = specializationName;
            specializationName = "";
            NotifyPropertyChanged(nameof(SpecializationName));
            if (SmartSchoolBLL.Specialization.SpecializationList.Where(u => u.Name == specializationName).Any())
            {
                return;
            }
            try
            {
                SmartSchoolBLL.Specialization.AddSpecialization(newSpecialization);
            }
            catch (SmartSchoolException ex)
            {
                return;
            }
            NotifyPropertyChanged(nameof(Specializations));
        }

        private ICommand deleteSpecializationCommand;
        public ICommand DeleteSpecializationCommand
        {
            get
            {
                if (deleteSpecializationCommand == null)
                {
                    deleteSpecializationCommand = new RelayCommand<Subject>(DeleteSpecialization);
                }
                return deleteSpecializationCommand;
            }
        }

        private void DeleteSpecialization(object param)
        {
            // Delete specialization subjects
            var specializationSubjects = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects()
                .Where(ss => ss.SpecializationId == SelectedSpecialization.SpecializationId)
                .ToList();

            foreach (var specializationSubject in specializationSubjects)
            {
                SmartSchoolBLL.SpecializationSubject.DeleteSpecializationSubject(specializationSubject);
            }

            // Get all classes with the selected specialization
            var classesWithSpecialization = SmartSchoolBLL.Class.GetClasses()
                .Where(c => c.SpecializationId == SelectedSpecialization.SpecializationId)
                .ToList();

            // Delete teacher classes
            var teacherClasses = SmartSchoolBLL.TeacherClass.GetTeacherClasses()
                .Where(tc => classesWithSpecialization.Any(c => c.ClassId == tc.ClassId))
                .ToList();

            foreach (var teacherClass in teacherClasses)
            {
                SmartSchoolBLL.TeacherClass.DeleteTeacherClass(teacherClass);
            }

            // Delete student classes
            var studentClasses = SmartSchoolBLL.StudentClass.GetStudentClasses()
                .Where(sc => classesWithSpecialization.Any(c => c.ClassId == sc.ClassId))
                .ToList();

            foreach (var studentClass in studentClasses)
            {
                SmartSchoolBLL.StudentClass.DeleteStudentClass(studentClass);
            }

            // Delete student subjects
            var studentSubjects = SmartSchoolBLL.StudentSubject.GetStudentSubjects();

            // Find the student subjects associated with the classes of the deleted specialization
            var studentSubjectsToDelete = studentSubjects
                .Where(ss => studentClasses.Any(sc => sc.StudentId == ss.StudentId))
                .ToList();

            foreach (var studentSubject in studentSubjectsToDelete)
            {
                SmartSchoolBLL.StudentSubject.DeleteStudentSubject(studentSubject);
            }

            foreach(var _class in  classesWithSpecialization)
            {
                SmartSchoolBLL.Class.DeleteClass(_class);
            }

            // Delete the specialization itself
            SmartSchoolBLL.Specialization.DeleteSpecialization(SelectedSpecialization);
            subjects.Remove(SelectedSubject);
            NotifyPropertyChanged(nameof(Specializations));
            NotifyPropertyChanged(nameof(Classes));
        }

        private bool isSubjectButtonChecked;
        public bool IsSubjectButtonChecked
        {
            get { return isSubjectButtonChecked; }
            set
            {
                isSubjectButtonChecked = value;
                NotifyPropertyChanged(nameof(IsSubjectButtonChecked));
            }
        }

        private ObservableCollection<Subject> subjects;
        public ObservableCollection<Subject> Subjects
        {
            get
            {
                subjects = SmartSchoolBLL.Subject.GetSubjects();
                return subjects;
            }
            set
            {
                subjects = value;
                NotifyPropertyChanged(nameof(Subjects));
            }
        }

        private Subject selectedSubject;

        public Subject SelectedSubject
        {
            get { return selectedSubject; }
            set
            {
                if (selectedSubject != value)
                {
                    selectedSubject = value;
                    NotifyPropertyChanged(nameof(SelectedSubject));
                    NotifyPropertyChanged(nameof(SelectedSubjectTeachers));
                }
            }
        }

        private ICommand editSubjectCommand;

        public ICommand EditSubjectCommand
        {
            get
            {
                if (editSubjectCommand == null)
                {
                    editSubjectCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(EditSubject);
                }
                return editSubjectCommand;
            }
        }

        private void EditSubject(DataGridCellEditEndingEventArgs e)
        {
            var editedTextBox = e.EditingElement as TextBox;
            var newValue = editedTextBox.Text;
            var subject = e.Row.DataContext as Subject;
            subject.Name = newValue;

            SmartSchoolBLL.Subject.ModifySubject(subject);
        }

        private string subjectName;
        public string SubjectName
        {
            get { return subjectName; }
            set
            {
                subjectName = value;
                NotifyPropertyChanged(nameof(SubjectName));
            }
        }

        private ICommand addSubjectCommand;
        public ICommand AddSubjectCommand
        {
            get
            {
                if (addSubjectCommand == null)
                {
                    addSubjectCommand = new RelayCommand<Subject>(AddSubject);
                }
                return addSubjectCommand;
            }
        }

        private void AddSubject(object param)
        {
            Subject newSubject = new Subject();
            newSubject.Name = subjectName;
            subjectName = "";
            NotifyPropertyChanged(nameof(SubjectName));
            if (SmartSchoolBLL.Subject.SubjectList.Any(u => u.Name == subjectName))
            {
                return;
            }

            try
            {
                SmartSchoolBLL.Subject.AddSubject(newSubject);
            }
            catch (SmartSchoolException ex)
            {
                return;
            }

            NotifyPropertyChanged(nameof(Subjects));
        }

        private ICommand deleteSubjectCommand;
        public ICommand DeleteSubjectCommand
        {
            get
            {
                if (deleteSubjectCommand == null)
                {
                    deleteSubjectCommand = new RelayCommand<Subject>(DeleteSubject);
                }
                return deleteSubjectCommand;
            }
        }

        private void DeleteSubject(object param)
        {
            // Delete specialization subjects
            var specializationSubjects = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects()
                .Where(ss => ss.SubjectId == SelectedSubject.SubjectId)
                .ToList();

            foreach (var specializationSubject in specializationSubjects)
            {
                SmartSchoolBLL.SpecializationSubject.DeleteSpecializationSubject(specializationSubject);
            }

            // Delete teacher subjects
            var teacherSubjects = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects()
                .Where(ts => ts.SubjectId == SelectedSubject.SubjectId)
                .ToList();

            foreach (var teacherSubject in teacherSubjects)
            {
                SmartSchoolBLL.TeacherSubject.DeleteTeacherSubject(teacherSubject);
            }

            // Delete student subjects
            var studentSubjects = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                .Where(ss => ss.SubjectId == SelectedSubject.SubjectId)
                .ToList();

            foreach (var studentSubject in studentSubjects)
            {
                SmartSchoolBLL.StudentSubject.DeleteStudentSubject(studentSubject);
            }

            // Delete the subject itself
            SmartSchoolBLL.Subject.DeleteSubject(SelectedSubject);
            subjects.Remove(SelectedSubject);
            NotifyPropertyChanged(nameof(Subjects));
            NotifyPropertyChanged(nameof(SelectedClassSubjects));
        }

        private ObservableCollection<Class> classes;

        public ObservableCollection<Class> Classes
        {
            get
            {
                classes = SmartSchoolBLL.Class.GetClasses();
                return classes;
            }
            set
            {
                classes = value;
                NotifyPropertyChanged(nameof(Classes));
            }
        }

        private ObservableCollection<User> formTeachers;

        public ObservableCollection<User> FormTeachers
        {
            get
            {
                var assignedFormTeachers = SmartSchoolBLL.Class.GetClasses()
            .Where(c => c.FormTeacherId != null)
            .Select(c => c.FormTeacherId)
            .ToList();

                var result = SmartSchoolBLL.User.GetUsers()
                    .Where(u => u.Role == Role.Diriginte && !assignedFormTeachers.Contains(u.UserId))
                    .ToList();

                formTeachers = new ObservableCollection<User>(result);
                return formTeachers;
            }
            set
            {
                formTeachers = value;
                NotifyPropertyChanged(nameof(FormTeachers));
            }
        }


        private ObservableCollection<User> teachers;

        public ObservableCollection<User> Teachers
        {
            get
            {
                var teacherSubjectList = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects();
                var _teachers = SmartSchoolBLL.User.GetUsers()
                     .Where(u => (u.Role == Role.Profesor || u.Role == Role.Diriginte) && !teacherSubjectList.Any(sc => sc.TeacherId == u.UserId))
                     .ToList();
                teachers = new ObservableCollection<User>(_teachers);
                return teachers;
            }
            set
            {
                teachers = value;
                NotifyPropertyChanged(nameof(Teachers));
            }
        }

        private ObservableCollection<User> students;

        public ObservableCollection<User> Students
        {
            get
            {
                var studentClassList = SmartSchoolBLL.StudentClass.GetStudentClasses();
                var _students = SmartSchoolBLL.User.GetUsers()
                    .Where(u => u.Role == Role.Elev && !studentClassList.Any(sc => sc.StudentId == u.UserId))
                    .ToList();
                students = new ObservableCollection<User>(_students);
                return students;
            }
            set
            {
                students = value;
                NotifyPropertyChanged(nameof(Students));
            }
        }

        private int selectedStudentId;

        public int SelectedStudentId
        {
            get { return selectedStudentId; }
            set
            {
                selectedStudentId = value;
                NotifyPropertyChanged(nameof(SelectedStudentId));
            }
        }

        private int selectedTeacherId;

        public int SelectedTeacherId
        {
            get { return selectedTeacherId; }
            set
            {
                selectedTeacherId = value;
                NotifyPropertyChanged(nameof(SelectedTeacherId));
            }
        }

        private ObservableCollection<User> selectedClassStudents;

        public ObservableCollection<User> SelectedClassStudents
        {
            get
            {
                if (selectedClass == null)
                {
                    return new ObservableCollection<User>();
                }
                var studentClassList = SmartSchoolBLL.StudentClass.GetStudentClasses();
                var _students = SmartSchoolBLL.User.GetUsers()
                    .Where(u => u.Role == Role.Elev && studentClassList.Any(sc => sc.ClassId == selectedClass.ClassId && sc.StudentId == u.UserId))
                    .ToList();
                selectedClassStudents = new ObservableCollection<User>(_students);
                return selectedClassStudents;
            }
            set
            {
                selectedClassStudents = value;
                NotifyPropertyChanged(nameof(SelectedClassStudents));
            }
        }

        private ObservableCollection<User> selectedSubjectTeachers;

        public ObservableCollection<User> SelectedSubjectTeachers
        {
            get
            {
                if (selectedSubject == null)
                {
                    return new ObservableCollection<User>();
                }
                var subjectTeacherList = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects();
                var _teachers = SmartSchoolBLL.User.GetUsers()
                    .Where(u => (u.Role == Role.Profesor || u.Role == Role.Diriginte) && subjectTeacherList.Any(sc => sc.SubjectId == selectedSubject.SubjectId && sc.TeacherId == u.UserId))
                    .ToList();
                selectedSubjectTeachers = new ObservableCollection<User>(_teachers);
                return selectedSubjectTeachers;
            }
            set
            {
                selectedSubjectTeachers = value;
                NotifyPropertyChanged(nameof(SelectedSubjectTeachers));
            }
        }

        private Class selectedClass;

        public Class SelectedClass
        {
            get { return selectedClass; }
            set
            {
                if (selectedClass != value)
                {
                    selectedClass = value;
                    NotifyPropertyChanged(nameof(SelectedClass));
                    NotifyPropertyChanged(nameof(SelectedClassStudents));
                    NotifyPropertyChanged(nameof(SelectedClassSubjects));
                }
            }
        }

        private ICommand editClassCommand;

        public ICommand EditClassCommand
        {
            get
            {
                if (editClassCommand == null)
                {
                    editClassCommand = new RelayCommand<DataGridCellEditEndingEventArgs>(EditClass);
                }
                return editClassCommand;
            }
        }

        private void EditClass(DataGridCellEditEndingEventArgs e)
        {
            var editedTextBox = e.EditingElement as TextBox;
            var newValue = editedTextBox.Text;
            var _class = e.Row.DataContext as Class;
            switch (e.Column.Header.ToString())
            {
                case "Name":
                    _class.Name = newValue;
                    break;
                case "SpecializationId":
                    _class.SpecializationId = Int32.Parse(newValue);
                    break;
                case "FormTeacherId":
                    int formTeacherId;
                    if (Int32.TryParse(newValue, out formTeacherId))
                    {
                        var formTeacher = SmartSchoolBLL.User.GetUsers()
                            .FirstOrDefault(u => u.UserId == formTeacherId && u.Role == Role.Diriginte);
                        var existingFormTeacherClass = SmartSchoolBLL.Class.GetClasses()
                            .FirstOrDefault(c => c.FormTeacherId == formTeacherId && c.ClassId != _class.ClassId);
                        if (formTeacher != null && existingFormTeacherClass == null)
                        {
                            _class.FormTeacherId = formTeacherId;
                        }
                        else
                        {
                            editedTextBox.Text = _class.FormTeacherId.ToString();
                            return;
                        }
                    }
                    break;
            }

            SmartSchoolBLL.Class.ModifyClass(_class);
        }

        private string className;
        public string ClassName
        {
            get { return className; }
            set
            {
                className = value;
                NotifyPropertyChanged(nameof(ClassName));
            }
        }

        private int selectedSpecializationId;

        public int SelectedSpecializationId
        {
            get { return selectedSpecializationId; }
            set
            {
                selectedSpecializationId = value;
                NotifyPropertyChanged(nameof(SelectedSpecializationId));
            }
        }

        private int selectedFormTeacherId;

        public int SelectedFormTeacherId
        {
            get { return selectedFormTeacherId; }
            set
            {
                selectedFormTeacherId = value;
                NotifyPropertyChanged(nameof(SelectedFormTeacherId));
            }
        }

        private ICommand addClassCommand;
        public ICommand AddClassCommand
        {
            get
            {
                if (addClassCommand == null)
                {
                    addClassCommand = new RelayCommand<Class>(AddClass);
                }
                return addClassCommand;
            }
        }

        private void AddClass(object param)
        {
            Class newClass = new Class();
            newClass.Name = className;
            newClass.FormTeacherId = selectedFormTeacherId;
            newClass.SpecializationId = selectedSpecializationId;
            className = "";
            NotifyPropertyChanged(nameof(ClassName));
            if (SmartSchoolBLL.Class.GetClasses().Where(u => u.Name == className).Any())
            {
                return;
            }
            try
            {
                SmartSchoolBLL.Class.AddClass(newClass);
            }
            catch (SmartSchoolException ex)
            {
                return;
            }
            NotifyPropertyChanged(nameof(Classes));
            NotifyPropertyChanged(nameof(FormTeachers));
        }

        private ICommand deleteClassCommand;
        public ICommand DeleteClassCommand
        {
            get
            {
                if (deleteClassCommand == null)
                {
                    deleteClassCommand = new RelayCommand<Class>(DeleteClass);
                }
                return deleteClassCommand;
            }
        }

        private void DeleteClass(object param)
        {
            var teacherClasses = SmartSchoolBLL.TeacherClass.GetTeacherClasses()
                .Where(tc => tc.ClassId == SelectedClass.ClassId)
                .ToList();

            foreach (var teacherClass in teacherClasses)
            {
                SmartSchoolBLL.TeacherClass.DeleteTeacherClass(teacherClass);
            }

            var studentClasses = SmartSchoolBLL.StudentClass.GetStudentClasses()
                .Where(sc => sc.ClassId == SelectedClass.ClassId)
                .ToList();

            foreach (var studentClass in studentClasses)
            {
                SmartSchoolBLL.StudentClass.DeleteStudentClass(studentClass);

                // Delete student subjects for the students in the class
                var studentSubjectsToDelete = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                    .Where(ss => ss.StudentId == studentClass.StudentId)
                    .ToList();

                foreach (var studentSubject in studentSubjectsToDelete)
                {
                    SmartSchoolBLL.StudentSubject.DeleteStudentSubject(studentSubject);
                }
            }

            SmartSchoolBLL.Class.DeleteClass(SelectedClass);
            classes.Remove(SelectedClass);
        }

        private ICommand addStudentToClassCommand;

        public ICommand AddStudentToClassCommand
        {
            get
            {
                if (addStudentToClassCommand == null)
                {
                    addStudentToClassCommand = new RelayCommand<StudentClass>(AddStudentToClass);
                }
                return addStudentToClassCommand;
            }
        }

        public void AddStudentToClass(object param)
        {
            if (selectedClass == null)
                return;
            if (selectedStudentId == 0)
                return;

            StudentClass studentClass = new StudentClass();
            studentClass.StudentId = selectedStudentId;
            studentClass.ClassId = selectedClass.ClassId;
            SmartSchoolBLL.StudentClass.AddStudentClass(studentClass);

            var specializationSubjects = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects()
                .Where(ss => ss.SpecializationId == selectedClass.SpecializationId)
                .ToList();

            foreach (var specializationSubject in specializationSubjects)
            {
                StudentSubject studentSubject = new StudentSubject();
                studentSubject.StudentId = selectedStudentId;
                studentSubject.SubjectId = specializationSubject.SubjectId;
                SmartSchoolBLL.StudentSubject.AddStudentSubject(studentSubject);
            }

            NotifyPropertyChanged(nameof(SelectedClassStudents));
            NotifyPropertyChanged(nameof(Students));
        }

        private User selectedStudentFromClass;

        public User SelectedStudentFromClass
        {
            get { return selectedStudentFromClass; }
            set
            {
                if (selectedStudentFromClass != value)
                {
                    selectedStudentFromClass = value;
                    NotifyPropertyChanged(nameof(SelectedStudentFromClass));
                }
            }
        }

        private ICommand deleteStudentFromClassCommand;

        public ICommand DeleteStudentFromClassCommand
        {
            get
            {
                if (deleteStudentFromClassCommand == null)
                {
                    deleteStudentFromClassCommand = new RelayCommand<StudentClass>(DeleteStudentFromClass);
                }
                return deleteStudentFromClassCommand;
            }
        }

        private void DeleteStudentFromClass(object param)
        {
            if (selectedStudentFromClass == null)
                return;
            StudentClass studentClass = new StudentClass();
            studentClass.StudentId = selectedStudentFromClass.UserId;
            studentClass.ClassId = selectedClass.ClassId;
            SmartSchoolBLL.StudentClass.DeleteStudentClass(studentClass);
            NotifyPropertyChanged(nameof(SelectedClassStudents));
            NotifyPropertyChanged(nameof(Students));
        }

        private ICommand addTeacherToSubjectCommand;

        public ICommand AddTeacherToSubjectCommand
        {
            get
            {
                if (addTeacherToSubjectCommand == null)
                {
                    addTeacherToSubjectCommand = new RelayCommand<TeacherSubject>(AddTeacherToSubject);
                }
                return addTeacherToSubjectCommand;
            }
        }

        public void AddTeacherToSubject(object param)
        {
            if (selectedSubject == null)
                return;
            if (selectedTeacherId == 0)
                return;
            TeacherSubject teacherSubject = new TeacherSubject();
            teacherSubject.TeacherId = selectedTeacherId;
            teacherSubject.SubjectId = selectedSubject.SubjectId;
            SmartSchoolBLL.TeacherSubject.AddTeacherSubject(teacherSubject);
            NotifyPropertyChanged(nameof(SelectedSubjectTeachers));
            NotifyPropertyChanged(nameof(Teachers));
        }

        private User selectedTeacherFromSubject;

        public User SelectedTeacherFromSubject
        {
            get { return selectedTeacherFromSubject; }
            set
            {
                if (selectedTeacherFromSubject != value)
                {
                    selectedTeacherFromSubject = value;
                    NotifyPropertyChanged(nameof(SelectedTeacherFromSubject));
                }
            }
        }

        public ICommand deleteTeacherFromSubjectCommand;

        public ICommand DeleteTeacherFromSubjectCommand
        {
            get
            {
                if (deleteTeacherFromSubjectCommand == null)
                {
                    deleteTeacherFromSubjectCommand = new RelayCommand<TeacherSubject>(DeleteTeacherFromSubject);
                }
                return deleteTeacherFromSubjectCommand;
            }
        }
        private void DeleteTeacherFromSubject(object param)
        {
            if (selectedTeacherFromSubject == null)
                return;

            TeacherSubject teacherSubject = new TeacherSubject();
            teacherSubject.TeacherId = selectedTeacherFromSubject.UserId;
            teacherSubject.SubjectId = selectedSubject.SubjectId;

            // Delete teacher classes for the teacher in the selected subject
            var teacherClassesToDelete = SmartSchoolBLL.TeacherClass.GetTeacherClasses()
                .Where(tc => tc.TeacherId == selectedTeacherFromSubject.UserId)
                .ToList();

            foreach (var teacherClass in teacherClassesToDelete)
            {
                SmartSchoolBLL.TeacherClass.DeleteTeacherClass(teacherClass);
            }

            SmartSchoolBLL.TeacherSubject.DeleteTeacherSubject(teacherSubject);
            NotifyPropertyChanged(nameof(SelectedSubjectTeachers));
            NotifyPropertyChanged(nameof(Teachers));
        }


        private int selectedSubjectId;
        public int SelectedSubjectId
        {
            get { return selectedSubjectId; }
            set
            {
                selectedSubjectId = value;
                NotifyPropertyChanged(nameof(SelectedSubjectId));
            }
        }

        private ICommand addSubjectToSpecializationCommand;
        public ICommand AddSubjectToSpecializationCommand
        {
            get
            {
                if (addSubjectToSpecializationCommand == null)
                {
                    addSubjectToSpecializationCommand = new RelayCommand<SpecializationSubject>(AddSubjectToSpecialization);
                }
                return addSubjectToSpecializationCommand;
            }
        }

        public void AddSubjectToSpecialization(object param)
        {
            if (selectedSpecialization == null)
                return;
            if (selectedSubjectId == 0)
                return;
            if (semesterExamFalse == false && semesterExamTrue == false)
                return;

            bool specializationSubjectExists = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects()
                .Any(ss => ss.SpecializationId == selectedSpecialization.SpecializationId && ss.SubjectId == selectedSubjectId);
            if (specializationSubjectExists)
                return;

            var classes = SmartSchoolBLL.Class.GetClasses()
                .Where(c => c.SpecializationId == selectedSpecialization.SpecializationId);

            foreach (var _class in classes)
            {
                var studentClasses = SmartSchoolBLL.StudentClass.GetStudentClasses()
                    .Where(sc => sc.ClassId == _class.ClassId);

                foreach (var studentClass in studentClasses)
                {
                    StudentSubject studentSubject = new StudentSubject();
                    studentSubject.StudentId = studentClass.StudentId;
                    studentSubject.SubjectId = selectedSubjectId;
                    SmartSchoolBLL.StudentSubject.AddStudentSubject(studentSubject);
                }
            }

            SpecializationSubject specializationSubject = new SpecializationSubject();
            specializationSubject.SpecializationId = selectedSpecialization.SpecializationId;
            specializationSubject.SubjectId = selectedSubjectId;
            if (semesterExamTrue)
                specializationSubject.HasSemesterExam = true;
            else
                specializationSubject.HasSemesterExam = false;
            SmartSchoolBLL.SpecializationSubject.AddSpecializationSubject(specializationSubject);
            NotifyPropertyChanged(nameof(SelectedSpecializationSubjects));
            NotifyPropertyChanged(nameof(SelectedClassSubjects));
        }


        public ICommand deleteSubjectFromSpecializationCommand;

        public ICommand DeleteSubjectFromSpecializationCommand
        {
            get
            {
                if (deleteSubjectFromSpecializationCommand == null)
                {
                    deleteSubjectFromSpecializationCommand = new RelayCommand<SpecializationSubject>(DeleteSubjectFromSpecialization);
                }
                return deleteSubjectFromSpecializationCommand;
            }
        }
        private void DeleteSubjectFromSpecialization(object param)
        {
            if (selectedSubjectFromSpecialization == null)
                return;

            SpecializationSubject specializationSubject = new SpecializationSubject();
            specializationSubject.SpecializationId = selectedSpecialization.SpecializationId;
            specializationSubject.SubjectId = selectedSubjectFromSpecialization.SubjectId;

            // Delete student subjects for the subject in the selected specialization
            var studentSubjectsToDelete = SmartSchoolBLL.StudentSubject.GetStudentSubjects()
                .Where(ss => ss.SubjectId == selectedSubjectFromSpecialization.SubjectId)
                .ToList();

            foreach (var studentSubject in studentSubjectsToDelete)
            {
                SmartSchoolBLL.StudentSubject.DeleteStudentSubject(studentSubject);
            }

            SmartSchoolBLL.SpecializationSubject.DeleteSpecializationSubject(specializationSubject);
            NotifyPropertyChanged(nameof(SelectedSpecializationSubjects));
            NotifyPropertyChanged(nameof(SelectedClassSubjects));
        }

        private Subject selectedSubjectFromSpecialization;

        public Subject SelectedSubjectFromSpecialization
        {
            get { return selectedSubjectFromSpecialization; }
            set
            {
                if (selectedSubjectFromSpecialization != value)
                {
                    selectedSubjectFromSpecialization = value;
                    NotifyPropertyChanged(nameof(SelectedSubjectFromSpecialization));
                }
            }
        }

        private ObservableCollection<Subject> selectedSpecializationSubjects;

        public ObservableCollection<Subject> SelectedSpecializationSubjects
        {
            get
            {
                if (selectedSpecialization == null)
                {
                    return new ObservableCollection<Subject>();
                }
                var specializationSubject = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects();
                var _subjects = SmartSchoolBLL.Subject.GetSubjects()
                                .Where(s => specializationSubject.Any(sc => sc.SpecializationId == selectedSpecialization.SpecializationId && sc.SubjectId == s.SubjectId))
                                .ToList();
                selectedSpecializationSubjects = new ObservableCollection<Subject>(_subjects);
                return selectedSpecializationSubjects;
            }
            set
            {
                selectedSpecializationSubjects = value;
                NotifyPropertyChanged(nameof(SelectedSpecializationSubjects));
            }
        }

        private ObservableCollection<Subject> selectedClassSubjects;

        public ObservableCollection<Subject> SelectedClassSubjects
        {
            get
            {
                if (selectedClass == null)
                {
                    return new ObservableCollection<Subject>();
                }
                var specializationSubject = SmartSchoolBLL.SpecializationSubject.GetSpecializationSubjects();
                var _subjects = SmartSchoolBLL.Subject.GetSubjects()
                                .Where(s => specializationSubject.Any(sc => sc.SpecializationId == selectedClass.SpecializationId && sc.SubjectId == s.SubjectId))
                                .ToList();
                selectedClassSubjects = new ObservableCollection<Subject>(_subjects);
                return selectedClassSubjects;
            }
            set
            {
                selectedClassSubjects = value;
                NotifyPropertyChanged(nameof(SelectedClassSubjects));
            }
        }

        private Subject selectedSubjectFromClass;

        public Subject SelectedSubjectFromClass
        {
            get { return selectedSubjectFromClass; }
            set
            {
                if (selectedSubjectFromClass != value)
                {
                    selectedSubjectFromClass = value;
                    NotifyPropertyChanged(nameof(SelectedSubjectFromClass));
                    NotifyPropertyChanged(nameof(SelectedSubjectFromClassesTeachers));
                    NotifyPropertyChanged(nameof(SelectedSubjectFromClassTeacherName));
                }
            }
        }

        private ObservableCollection<User> selectedSubjectFromClassesTeachers;

        public ObservableCollection<User> SelectedSubjectFromClassesTeachers
        {
            get
            {
                if (selectedSubjectFromClass == null)
                {
                    return new ObservableCollection<User>();
                }
                var subjectTeacherList = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects();
                var _teachers = SmartSchoolBLL.User.GetUsers()
                    .Where(u => (u.Role == Role.Profesor || u.Role == Role.Diriginte) && subjectTeacherList.Any(sc => sc.SubjectId == selectedSubjectFromClass.SubjectId && sc.TeacherId == u.UserId))
                    .ToList();
                selectedSubjectFromClassesTeachers = new ObservableCollection<User>(_teachers);
                return selectedSubjectFromClassesTeachers;
            }
            set
            {
                selectedSubjectFromClassesTeachers = value;
                NotifyPropertyChanged(nameof(SelectedSubjectFromClassesTeachers));
            }
        }

        private int selectedSubjectTeacherId;

        public int SelectedSubjectTeacherId
        {
            get { return selectedSubjectTeacherId; }
            set
            {
                selectedSubjectTeacherId = value;
                NotifyPropertyChanged(nameof(SelectedSubjectTeacherId));
            }
        }

        private ICommand editTeacherClassCommand;

        public ICommand EditTeacherClassCommand
        {
            get
            {
                if (editTeacherClassCommand == null)
                {
                    editTeacherClassCommand = new RelayCommand<TeacherClass>(EditTeacherClass);
                }
                return editTeacherClassCommand;
            }
        }
        private void EditTeacherClass(object param)
        {
            if (selectedSubjectTeacherId == 0)
                return;

            var teacherClasses = SmartSchoolBLL.TeacherClass.GetTeacherClasses();
            var teacherSubjects = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects();

            var existingTeacherSubject = teacherSubjects.FirstOrDefault(ts => ts.SubjectId == selectedSubjectFromClass.SubjectId &&
                teacherClasses.Any(tc => tc.TeacherId == ts.TeacherId && tc.ClassId == selectedClass.ClassId));


            if (existingTeacherSubject != null)
            {
                TeacherClass teacherClass = new TeacherClass();
                teacherClass.TeacherId = existingTeacherSubject.TeacherId;
                teacherClass.ClassId = selectedClass.ClassId;
                SmartSchoolBLL.TeacherClass.DeleteTeacherClass(teacherClass);
            }
            TeacherClass newTeacherClass = new TeacherClass();
            newTeacherClass.TeacherId = selectedSubjectTeacherId;
            newTeacherClass.ClassId = selectedClass.ClassId;
            SmartSchoolBLL.TeacherClass.AddTeacherClass(newTeacherClass);
            NotifyPropertyChanged(nameof(SelectedSubjectFromClassTeacherName));
        }

        private string selectedSubjectFromClassTeacherName;

        public string SelectedSubjectFromClassTeacherName
        {
            get
            {
                if (selectedSubjectFromClass == null)
                    return "";
                var teacherClasses = SmartSchoolBLL.TeacherClass.GetTeacherClasses();
                var teacherSubjects = SmartSchoolBLL.TeacherSubject.GetTeacherSubjects();

                var existingTeacherSubject = teacherSubjects.FirstOrDefault(ts => ts.SubjectId == selectedSubjectFromClass.SubjectId &&
                    teacherClasses.Any(tc => tc.TeacherId == ts.TeacherId && tc.ClassId == selectedClass.ClassId));

                if(existingTeacherSubject != null)
                {
                    var teacher = SmartSchoolBLL.User.GetUsers().FirstOrDefault(u => u.UserId == existingTeacherSubject.TeacherId);
                    selectedSubjectFromClassTeacherName = teacher.FullName;
                    return selectedSubjectFromClassTeacherName;
                }
                else
                {
                    return "";
                }
            }
            set
            {
                selectedSubjectFromClassTeacherName = value;
                NotifyPropertyChanged(nameof(SelectedSubjectFromClassTeacherName));
            }
        }

        private bool semesterExamTrue;

        public bool SemesterExamTrue
        {
            get
            {
                return semesterExamTrue;
            }
            set
            {
                semesterExamTrue = value;
                NotifyPropertyChanged(nameof(SemesterExamTrue));
            }
        }

        private bool semesterExamFalse;
        public bool SemesterExamFalse
        {
            get
            {
                return semesterExamFalse;
            }
            set
            {
                semesterExamFalse = value;
                NotifyPropertyChanged(nameof(semesterExamFalse));
            }
        }
    }
}