using HomeManager.Services;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace HomeManager
{
    /// <summary>
    /// Interaction logic for the main application. This class handles global exception handling 
    /// for unhandled exceptions across UI and non-UI threads.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the application and subscribes to various global exception events.
        /// </summary>
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        /// <summary>
        /// Handles unhandled exceptions that occur on the UI thread.
        /// </summary>
        /// <param name="sender">The sender of the exception.</param>
        /// <param name="e">Event arguments containing the exception details.</param>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            clsExceptionService.InsertException(e.Exception);
            e.Handled = true;
        }

        /// <summary>
        /// Handles unhandled exceptions from non-UI threads.
        /// </summary>
        /// <param name="sender">The sender of the exception.</param>
        /// <param name="e">Event arguments containing the exception details.</param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                // Log the exception through the exception service
                clsExceptionService.InsertException(ex);
            }
            else
            {
                // Handle case where the exception object is not of type Exception
                clsExceptionService.InsertException(new Exception("Unhandled exception object was not of type Exception."));
            }

            // Log non-UI thread exception
            clsExceptionService.InsertException((Exception)e.ExceptionObject);
        }

        /// <summary>
        /// Handles unobserved exceptions from background tasks.
        /// </summary>
        /// <param name="sender">The sender of the exception.</param>
        /// <param name="e">Event arguments containing the exception details.</param>
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            // Background thread task exceptions
            clsExceptionService.InsertException(e.Exception);
            e.SetObserved();
        }
    }
}
