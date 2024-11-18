using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for popup.xaml
    /// </summary>
    public partial class popup : Window
    {
        public popup()
        {
            InitializeComponent();
        }
      
    private void Button_Click(object sender, RoutedEventArgs e)
        {       
                using (var context = new ToDoEntities())
            {       
               var entity = new Task { task_name = task_name_txt.Text, task_begin= task_start.DisplayDate ,task_deadline=task_end.DisplayDate};
         
                context.Tasks.Add(entity);
                context.SaveChanges();
            }

            // Close the popup and indicate success
            this.DialogResult = true;
            this.Close();
        }
    
    }
}

     

 
   
