using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MSProject = Microsoft.Office.Interop.MSProject;

namespace ProjectList.Forms
{
    /// <summary>
    /// Логика взаимодействия для UCListPeriod.xaml
    /// </summary>
    public partial class UCListPeriod : System.Windows.Controls.UserControl
    {
        public Form Form;
        public MSProject.Task _task;
        public UCListPeriod()
        {
            InitializeComponent();

            ListPeriodTask listPeriodTask = new ListPeriodTask();
            
            DataContext = listPeriodTask;

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ListPeriodTask listPeriodTask) listPeriodTask.UpLoad(_task);
        }
    }
}
