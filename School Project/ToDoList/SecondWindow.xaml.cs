using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>
    /// 

    public partial class SecondWindow : Window
    {
        ToDoEntities ToDoEntities = new ToDoEntities();

        public SecondWindow()
        {
            InitializeComponent();
            //LoadData();
            this.DataContext = new MainViewModel();
            RefreshDataGrid();
        }
        public class MainViewModel
        {
            public ObservableCollection<Task> Tasks { get; set; }

            public MainViewModel()
            {
                LoadEmployees();
            }

            private void LoadEmployees()
            {
                using (var context = new ToDoEntities())
                {
                    Tasks = new ObservableCollection<Task>(context.Tasks.ToList());
                }
            }
        }
        private void DeleteFromDatabase(Task selectedItem)
        {
            using (var dbContext = new ToDoEntities())
            {
                // Attach the entity if necessary
                var entity = dbContext.Tasks.Find(selectedItem.task_id);
                if (entity != null)
                {
                    dbContext.Tasks.Remove(entity);
                    dbContext.SaveChanges();

                    // Optionally update the UI
                    //RefreshDataGrid();
                    MessageBox.Show("Item deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Item not found in the database.");
                }
            }
        }
        private void RefreshDataGrid()
        {
            using (var dbContext = new ToDoEntities())
            {
                display.ItemsSource = dbContext.Tasks.ToList();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)     // delete
        {
            var selectedItem = display.SelectedItem as Task; // Cast to your entity type
            if (selectedItem != null)
            {
                try
                {
                    DeleteFromDatabase(selectedItem);
                    RefreshDataGrid();
                    MessageBox.Show($"Deleted seussfully", "Done", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERROR {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw;
                }
               
            }
            else
            {
                MessageBox.Show("No item selected.");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //add
        {

            popup dialog = new popup();
            bool? result = dialog.ShowDialog();
            RefreshDataGrid();

        }
        private void UpdateDatabase(Task editedItem)
        {
            using (var dbContext = new ToDoEntities())
            {
                // Attach the entity if it is detached
                var entity = dbContext.Tasks.Find(editedItem.task_id);
                if (entity != null)
                {
                    dbContext.Entry(entity).CurrentValues.SetValues(editedItem);
                }
                else
                {
                    dbContext.Tasks.Add(editedItem); // For new entities
                }
                dbContext.SaveChanges();
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) // update
        {
            if (display.SelectedItem is Task selectedTask)
            {
                try
                {
                    UpdateDatabase(selectedTask);
                    RefreshDataGrid();
                    MessageBox.Show("updated Secussfully", "Done", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("select a task", "selection error", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }
    }
}