using HomeManager.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HomeManager.Services
{
    public class clsDialogService
    {
        private Window _dialogWindow;

        /// <summary>
        /// Toont een UserControl in een modaal venster.
        /// </summary>
        /// <param name="content">De UserControl die getoond moet worden.</param>
        /// <param name="title">De titel van het venster (optioneel).</param>
        public void ShowDialog(UserControl content, string title = "Dialog")
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            // Maak een nieuw venster
            _dialogWindow = new Window
            {
                //Title = title,
                //Content = content, // Stel de UserControl in als inhoud
                //SizeToContent = SizeToContent.WidthAndHeight, // Pas de grootte aan inhoud aan
                //WindowStartupLocation = WindowStartupLocation.CenterScreen, // Centreren
                //ResizeMode = ResizeMode.NoResize // Optioneel: voorkomen dat de gebruiker het venster kan schalen

                //AANPASSING THOMAS ZODAT HET SCHERM ALTIJD BETER PAST OOK MET DE ERRORHANDLING
                Title = title,
                Content = content,
                Width = 1000, // Stel een specifieke breedte in
                Height = 600, // Stel een specifieke hoogte in
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize
            };

            // Toon het venster als een modaal dialoogvenster
            _dialogWindow.Show();
        }

        /// <summary>
        /// Sluit het dialoogvenster.
        /// </summary>
        public void CloseDialog()
        {
            if (_dialogWindow != null)
            {
                _dialogWindow.Close();
                _dialogWindow = null;
            }
        }
    }
}

