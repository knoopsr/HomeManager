using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace HomeManager.Agenda.Helpers
{
    public class ThumbDragBehavior : Behavior<Thumb>
    {
        public static readonly DependencyProperty DragCommandProperty =
            DependencyProperty.Register(
                nameof(DragCommand),
                typeof(ICommand),
                typeof(ThumbDragBehavior),
                new PropertyMetadata(null));

        public ICommand DragCommand
        {
            get => (ICommand)GetValue(DragCommandProperty);
            set => SetValue(DragCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.DragDelta += OnDragDelta;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.DragDelta -= OnDragDelta;
            base.OnDetaching();
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DragCommand?.CanExecute(e) == true)
            {
                DragCommand.Execute(e);
            }
        }
    }
}
