using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace HomeManager.Helpers
{
    public static class clsPasswordHelper
    {
        public static readonly DependencyProperty BoundPassword =
      DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(clsPasswordHelper),
          new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundPasswordChanged));

        public static readonly DependencyProperty Attach =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(clsPasswordHelper), new PropertyMetadata(false, AttachChanged));

        public static bool GetAttach(DependencyObject obj) => (bool)obj.GetValue(Attach);
        public static void SetAttach(DependencyObject obj, bool value) => obj.SetValue(Attach, value);

        public static string GetBoundPassword(DependencyObject obj) => (string)obj.GetValue(BoundPassword);
        public static void SetBoundPassword(DependencyObject obj, string value) => obj.SetValue(BoundPassword, value);

        private static void AttachChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                if ((bool)e.OldValue)
                {
                    passwordBox.PasswordChanged -= PasswordChanged;
                }
                if ((bool)e.NewValue)
                {
                    passwordBox.PasswordChanged += PasswordChanged;
                }
            }
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordChanged;
                if (passwordBox.Password != (string)e.NewValue)
                {
                    passwordBox.Password = (string)e.NewValue;
                }
                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetBoundPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
