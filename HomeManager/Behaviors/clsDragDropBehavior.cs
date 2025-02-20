using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HomeManager.Behaviors
{
    public class clsDragDropBehavior
    {
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached("DropCommand", typeof(ICommand), typeof(clsDragDropBehavior), new PropertyMetadata(null, OnDropCommandChanged));

        public static ICommand GetDropCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DropCommandProperty);
        }

        public static void SetDropCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DropCommandProperty, value);
        }

        private static void OnDropCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                if (e.NewValue != null)
                {
                    element.Drop += Element_Drop;
                }
                else
                {
                    element.Drop -= Element_Drop;
                }
            }
        }

        private static void Element_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var command = GetDropCommand(sender as DependencyObject);
                if (command != null && command.CanExecute(files))
                {
                    command.Execute(files);
                }
            }
        }
    }
}