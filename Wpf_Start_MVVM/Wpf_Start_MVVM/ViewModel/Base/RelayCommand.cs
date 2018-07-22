using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Wpf_Start_MVVM.ViewModel.Base
{
    public class RelayCommand: ICommand
    {
        private readonly Action execute;
        private readonly Action<object> executeWithParameter;
        private readonly Func<bool> canExecute;
        private readonly Func<object, bool> canExecuteWithParameter;

        protected Func<Task> executeAwait;

        public string Libelle { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /*public static ICommand CreateCommande(Func<Task> execute)
        {
            var c = new CommandBase();

            c.executeAwait = execute;

            return c;
        }*/

        public RelayCommand()
        {
        }

        public RelayCommand(Action execute)
        {
            this.execute = execute;
        }

        public RelayCommand(Action<object> execute)
        {
            this.executeWithParameter = execute;
        }

        public RelayCommand(string libelle)
        {
            Libelle = libelle;
        }

        public RelayCommand(Action execute, string libelle)
        {
            this.execute = execute;
            Libelle = libelle;
        }

        public RelayCommand(Action<object> execute, string libelle)
        {
            executeWithParameter = execute;
            Libelle = libelle;
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            executeWithParameter = execute;
            canExecuteWithParameter = canExecute;
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute, string libelle)
        {
            executeWithParameter = execute;
            canExecuteWithParameter = canExecute;
            Libelle = libelle;
        }


        /// <summary>
        /// Initialise une nouvelle instance de la classe CommandBase
        /// </summary>
        /// <param name="execute">action à exécuter.</param>
        /// <param name="canExecute">statut de l'action à exécuter.</param>
        public RelayCommand(Action execute, Func<bool> canExecute, string libelle)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            Libelle = libelle;
        }

        public virtual void Execute(object parameter)
        {
            if (execute != null)
                execute();
            else if (executeAwait != null)
                executeAwait();
            else
            {
                if (executeWithParameter != null)
                    executeWithParameter(parameter);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecuteWithParameter != null)
                return canExecuteWithParameter(parameter);
            else
                return canExecute == null || canExecute();
        }

        public override string ToString()
        {
            return Libelle;
        }

        public static void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
