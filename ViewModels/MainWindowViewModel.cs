using OneWireComm.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneWireComm.ViewModels
{
    public class MainWindowViewModel : VisualElementBase
    {
        public ObservableCollection<HistoryItem> HistoryItems { get; set; } = new ObservableCollection<HistoryItem>();
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
            IsInitialized = _touchpen.Initialize(port);
            if (!IsInitialized)
            {
                AddHistoryItem(string.Empty, $"Unable to initialize touchpen on port {port}");
            }
        }

        public void ReadButton()
        {
            if (IsInitialized)
            {
                string serial = _touchpen.GetButtonSerial();
                byte[] data = _touchpen.GetDataBlock();
            }

        }

        public void AddHistoryItem(string serial, string message)
        {
            HistoryItems.Insert(0, new HistoryItem
            {
                Serial = serial,
                Message = message
            });
        }
    }
}
