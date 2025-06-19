using Microsoft.Office.Core;
using Microsoft.Office.Interop.MSProject;
using ProjectList.MVVM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using MSProject = Microsoft.Office.Interop.MSProject;

namespace ProjectList
{

    internal class ListPeriodTask: MyINotifyPropertyChanged
    {
        private DateTime minDate;
        private DateTime maxDate;
        
        private TaskPeriod selectedtaskPeriod;

        public TaskPeriod SelectedTaskPeriod 
        { 
            get { return selectedtaskPeriod; }
            set 
            { 
                selectedtaskPeriod = value; 
                OnPropertyChanged(nameof(SelectedTaskPeriod));
            }
        }

        public ObservableCollection<TaskPeriod> TaskPeriods { get; set; }

        private MSProject.Task task; 

        public ListPeriodTask() 
        {
            minDate = DateTime.MaxValue;
            maxDate = DateTime.MinValue;

            TaskPeriods = new ObservableCollection<TaskPeriod>();
        }

        public void UpLoad(MSProject.Task _task) 
        {
            task = _task;

            TaskPeriods.Clear();

            if (task != null)
                foreach (SplitPart part in (MSProject.SplitParts)task.SplitParts)
                {
                    DateTime start = (DateTime)part.Start;
                    DateTime finish = (DateTime)part.Finish;

                    if (minDate > start) minDate = start;
                    if (maxDate < finish) maxDate = finish;

                    TaskPeriods.Add(new TaskPeriod(start, finish));
                }


        }


        private RelayCommand saveCommand;

        public RelayCommand SaveCommand => saveCommand ??= new RelayCommand(obj => SaveTaskPeriod());

        private void SaveTaskPeriod() 
        {
            if (task != null)
            {

                foreach (TaskPeriod period in TaskPeriods)
                {
                    if (minDate > period.DataStart) minDate = period.DataStart;
                    if (maxDate < period.DataStop) maxDate = period.DataStop;
                }

                // Сначала удалить
                for (int i = task.SplitParts.Count; i >= 1; i--)
                {
                    task.SplitParts[i].Delete();
                }

                task.Start = minDate;
                task.Finish = maxDate;


                // Затем новые создать
                foreach (TaskPeriod newPeriod in TaskPeriods)
                {
                    //task.SplitParts.Add(newPeriod.DataStart, newPeriod.DataStop);
                    task.Split(newPeriod.DataStart, newPeriod.DataStop);
                }
                task.ConstraintType = 0;
                task.ConstraintDate = "НД";

            }

        }
    }

    internal class TaskPeriod: MyINotifyPropertyChanged
    {
        private DateTime dataStart;

        private DateTime dataStop;

        public DateTime DataStart 
        {   get { return dataStart; }
            set 
            { 
                dataStart = value;
                OnPropertyChanged(nameof(dataStart));
            } 
        }
        public DateTime DataStop
        {
            get {  return dataStop; }
            set 
            { 
                dataStop = value; 
                OnPropertyChanged(nameof(dataStop));
            } 
        }

        public TaskPeriod(DateTime start, DateTime stop) 
        { 
            DataStart = start;
            DataStop = stop;
        }

    }
}
