using HomeManager.Services;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

namespace HomeManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Handle UI thread exceptions
            clsExceptionService.InsertException(e.Exception);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex){ clsExceptionService.InsertException(ex); }
            else
            {
                // Handle unknown exception object type
                clsExceptionService.InsertException(new Exception("Unhandled exception object was not of type Exception."));
            }

            // Non-UI thread exceptions
            clsExceptionService.InsertException((Exception)e.ExceptionObject);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            // Background thread task exceptions
            clsExceptionService.InsertException(e.Exception);
            e.SetObserved();
        }
    }
}
