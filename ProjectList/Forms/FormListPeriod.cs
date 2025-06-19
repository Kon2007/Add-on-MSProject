using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MSProject = Microsoft.Office.Interop.MSProject;

namespace ProjectList.Forms
{
    public partial class FormListPeriod : Form
    {
        private readonly MSProject.Task _task;

        public FormListPeriod()
        {
        }

        public FormListPeriod(MSProject.Task task)
        {

            InitializeComponent();
            
            UCListPeriod uc = (UCListPeriod)elementHost1.Child;
            uc._task = task;
            uc.Form = this;


        }
        /*
                private void button1_Click(object sender, EventArgs e)
                {

                    returnPeriods = new List<TaskPeriod>();
                    for (int i = 0; i < (dataGridViewPeriod.Rows.Count - 1); i++)
                    {

                        TaskPeriod newPeriod = new TaskPeriod(DateTime.Parse(dataGridViewPeriod.Rows[i].Cells[0].Value.ToString()), 
                                DateTime.Parse(dataGridViewPeriod.Rows[i].Cells[1].Value.ToString()));

                        returnPeriods.Add(newPeriod);
                    }

                    this.Close();
                }
        */
    }
}
