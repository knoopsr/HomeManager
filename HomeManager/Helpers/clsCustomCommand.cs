using HomeManager.Model.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HomeManager.Helpers
{
    public class clsCustomCommand : ICommand
    {
        private Action<object?> _execute;
        private Predicate<object?> _canExecute;

        public clsCustomCommand(Action<object?> execute, Predicate<object?> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            bool b = _canExecute == null ? true : _canExecute(parameter);
            return b;
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);

            if (parameter == null)
            {
                // Als parameter null is, geef een melding
                MessageBox.Show("Parameter is null.");
                return;
            }
            var thistype = _execute.GetType();
            var thisproperties = thistype.GetProperties();

            string thisInfo = $"Type: {thistype.FullName}\n\nProperties:\n";

            foreach (var property in thisproperties)
            {
                var value = property.GetValue(_execute);
                thisInfo += $"{property.Name}: {value}\n";
            }
            thisInfo += clsLoginModel.Instance.AccountID;

            // Toon de informatie in een TextBox (als voorbeeld een MessageBox)
            var textBox = new System.Windows.Controls.TextBox
            {
                Text = thisInfo,
                VerticalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Auto,
                TextWrapping = System.Windows.TextWrapping.Wrap,
                Width = 400,
                Height = 300
            };

            // Maak een nieuw venster om de TextBox weer te geven
            var window = new System.Windows.Window
            {
                Title = "Parameter Info",
                Content = textBox,
                SizeToContent = System.Windows.SizeToContent.WidthAndHeight
            };
            window.ShowDialog();

        }

        public event EventHandler? CanExecuteChanged
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

    }

}
