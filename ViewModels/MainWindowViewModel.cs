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
        private Touchpen _touchpen;
        private bool _isInitialized;

        public bool IsInitialized
        {
            get { return _isInitialized; }
            set { _isInitialized = value; OnPropertyChanged(); }
        }

        public void InitializeTouchpen(int port)
        {
            _touchpen = new Touchpen();
           IsInitialized =_touchpen.Initialize(port);
        }

        public void ReadButton()
        {
            if (IsInitialized)
            {
                string serial = _touchpen.GetButtonSerial();
                byte[] data = _touchpen.GetDataBlock();
            }

        }
    }
}
