using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneWireComm.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand _initializeCommand;
        public ICommand InitializeCommand => _initializeCommand ?? (_initializeCommand = new CommandHandler(() => InitializeTouchpen(), () => CanExecute));
        public bool CanExecute => true;

        public void InitializeTouchpen()
        {

        }
    }
}
