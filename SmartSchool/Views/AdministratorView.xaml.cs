using System.Windows;
using System.Windows.Controls;

namespace SmartSchool.Views
{
    /// <summary>
    /// Interaction logic for AdministratorView.xaml
    /// </summary>
    public partial class AdministratorView : Window
    {
        public AdministratorView()
        {
            InitializeComponent();
        }

        private void UsersDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "UserId" || e.PropertyName == "SpecializationId" 
                || e.PropertyName == "SubjectId" || e.PropertyName == "ClassId")
            {
                var column = e.Column as DataGridTextColumn;
                if (column != null)
                {
                    column.IsReadOnly = true;
                }
            }
        }
    }
}