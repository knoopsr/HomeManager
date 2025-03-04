using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;
using HomeManager.Helpers;

namespace HomeManager.Behaviors
{
    public class clsDragDropBehavior : Behavior<UIElement>
    {
        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.Register(nameof(DropCommand), typeof(ICommand), typeof(clsDragDropBehavior));

        public ICommand DropCommand
        {
            get => (ICommand)GetValue(DropCommandProperty);
            set => SetValue(DropCommandProperty, value);
        }

        protected override void OnAttached()
        {
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += OnDrop;
            AssociatedObject.DragOver += OnDragOver;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Drop -= OnDrop;
            AssociatedObject.DragOver -= OnDragOver;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (DropCommand != null && DropCommand.CanExecute(e.Data))
            {
                DropCommand.Execute(e.Data);
            }
            e.Handled = true;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }
    }
}

