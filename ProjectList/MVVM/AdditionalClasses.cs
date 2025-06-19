using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjectList.MVVM
{
    internal class MyINotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }

    internal class AdditionalClasses
    {
    }

    /*  Базовый интерфейс
    public interface ICommand 
    { 
        event EventHandler CommandChanged;
        void Execute(object parameter);
        bool CanExecute(object parameter);
    }
    */

    public class RelayCommand : ICommand
    { 
        private Action<object> execute;

        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged 
        { 
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value;}
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter) 
        { 
            return canExecute == null || canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            execute(parameter);
        }

    }
}
