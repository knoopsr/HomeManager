using System.Windows;
using System.Windows.Controls;

namespace HomeManager.Helpers
{
    /// <summary>
    /// Helperklasse om binding van een <see cref="PasswordBox.Password"/> mogelijk te maken via attached properties.
    /// Ondersteunt TwoWay databinding met een string in je ViewModel.
    /// </summary>
    public static class clsPasswordHelper
    {
        /// <summary>
        /// Attached property voor het gebonden wachtwoord.
        /// Hiermee wordt de PasswordBox gekoppeld aan een string property in de ViewModel.
        /// </summary>
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(string),
                typeof(clsPasswordHelper),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnBoundPasswordChanged));

        /// <summary>
        /// Attached property om de binding te activeren of deactiveren.
        /// </summary>
        public static readonly DependencyProperty Attach =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(bool),
                typeof(clsPasswordHelper),
                new PropertyMetadata(false, AttachChanged));

        /// <summary>
        /// Haalt de waarde van de Attach-property op.
        /// </summary>
        public static bool GetAttach(DependencyObject obj) => (bool)obj.GetValue(Attach);

        /// <summary>
        /// Stelt de waarde in van de Attach-property.
        /// </summary>
        public static void SetAttach(DependencyObject obj, bool value) => obj.SetValue(Attach, value);

        /// <summary>
        /// Haalt het gebonden wachtwoord op.
        /// </summary>
        public static string GetBoundPassword(DependencyObject obj) => (string)obj.GetValue(BoundPassword);

        /// <summary>
        /// Stelt het gebonden wachtwoord in.
        /// </summary>
        public static void SetBoundPassword(DependencyObject obj, string value) => obj.SetValue(BoundPassword, value);

        /// <summary>
        /// Activeert of deactiveert het afhandelen van PasswordChanged events op basis van de Attach-property.
        /// </summary>
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

        /// <summary>
        /// Wordt aangeroepen wanneer de BoundPassword-property verandert en actualiseert de PasswordBox inhoud.
        /// </summary>
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

        /// <summary>
        /// Eventhandler die de BoundPassword bijwerkt wanneer het wachtwoord handmatig in de PasswordBox wordt aangepast.
        /// </summary>
        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetBoundPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
