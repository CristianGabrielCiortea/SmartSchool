using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace SmartSchool.Helpers
{
    public class DataGridBehavior
    {
        public static readonly DependencyProperty CellEditEndingCommandProperty =
            DependencyProperty.RegisterAttached("CellEditEndingCommand", typeof(ICommand), typeof(DataGridBehavior), new PropertyMetadata(null, CellEditEndingCommandPropertyChanged));

        public DataGridBehavior()
        {

        }

        public static ICommand GetCellEditEndingCommand(DataGrid dataGrid)
        {
            return (ICommand)dataGrid.GetValue(CellEditEndingCommandProperty);
        }

        public static void SetCellEditEndingCommand(DataGrid dataGrid, ICommand value)
        {
            dataGrid.SetValue(CellEditEndingCommandProperty, value);
        }

        private static void CellEditEndingCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = d as DataGrid;

            if (dataGrid != null)
            {
                if (e.NewValue != null)
                {
                    dataGrid.CellEditEnding += DataGrid_CellEditEnding;
                }
                else
                {
                    dataGrid.CellEditEnding -= DataGrid_CellEditEnding;
                }
            }
        }

        private static void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var command = GetCellEditEndingCommand(dataGrid);

            if (command != null && command.CanExecute(e))
            {
                command.Execute(e);
            }
        }
    }
}