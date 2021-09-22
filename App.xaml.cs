using OneWireComm.ViewModels;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;

namespace OneWireComm
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
            var mainWindow = new MainWindow(new MainWindowViewModel());
            mainWindow.Show();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Logger.Debug($"Error being thrown: {e}");
                //only triggers on non-UI threads
                if (e.ExceptionObject is Exception exception)
                {
                    if (exception is TaskCanceledException)
                    {
                        //we don't care about logging this
                        return;
                    }

                    Logger.Error($"{exception}{exception.StackTrace}");
                }

                Logger.Error($"CLR Terminating?: {e.IsTerminating}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Logger.Error($"{e.Exception}{e.Exception.StackTrace}");

            if (!Debugger.IsAttached)
            {
                //don't show this if we're debugging, because it's annoying when changing xaml
                _ = MessageBox.Show($"Error: {e.Exception}");
            }

            e.Handled = true;
        }

    }
}
