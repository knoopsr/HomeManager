using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace HomeManager.Services
{
    public static class clsWindowService
    {
        /// <summary>
        /// Positions the overlay window relative to the owner window, aligning it to the right
        /// with a specific size ratio and padding. Automatically adjusts for normal, minimized,
        /// and maximized states of the owner window.
        /// </summary>
        /// <remarks>
        /// <para><b>Positioning Formula:</b></para>
        /// <para>- Height = ownerHeight - 25px</para>
        /// <para>- Width = ownerWidth * 0.40</para>
        /// <para>- Left = ownerLeft + ownerWidth - Width - 25px</para>
        /// <para>- Top = ownerTop + 25px</para>
        /// <para>When the owner window is maximized, screen working area is used.</para>
        /// </remarks>
        public static void HandleWindowOverlay(Window overlayWindow, Window ownerWindow)
        {
            // Both Windows must exist
            if (ownerWindow == null || overlayWindow == null) return;

            // Close Window if ownerWindow doesn't exist
            if (!ownerWindow.IsLoaded)
            {
                overlayWindow.Close();
                return;
            }

            // Define padding & ratio
            const double padding = 25.0f;
            const double ratio = 0.40f;

            // Calculate the overlayWindow based on the ownerWindow
            // Under normal circumstances
            if (overlayWindow.WindowState == WindowState.Normal ||
                overlayWindow.WindowState == WindowState.Minimized)
            {
                overlayWindow.Height = ownerWindow.Height - padding;
                overlayWindow.Width = ownerWindow.Width * ratio;
                overlayWindow.Left = ownerWindow.Left + ownerWindow.Width - overlayWindow.Width - padding;
                overlayWindow.Top = ownerWindow.Top + padding;
            }

            // Calculate the overlayWindow based on the ownerScreen by getting a handle
            // If the window is maximized
            if (ownerWindow.WindowState == WindowState.Maximized)
            {
                Screen ownerScreen = Screen.FromHandle(
                    new System.Windows.Interop.WindowInteropHelper(ownerWindow).Handle);

                overlayWindow.Height = ownerScreen.WorkingArea.Height - padding;
                overlayWindow.Width = ownerWindow.Width * ratio;
                overlayWindow.Left = ownerScreen.WorkingArea.Left + ownerScreen.WorkingArea.Width - overlayWindow.Width - padding;
                overlayWindow.Top = ownerScreen.WorkingArea.Top + padding;
            }
        }
    }
}
