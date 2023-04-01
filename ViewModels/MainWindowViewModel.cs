using OneWireComm.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace OneWireComm.ViewModels
{
    public class MainWindowViewModel : VisualElementBase
    {
        public ObservableCollection<HistoryItem> HistoryItems { get; set; } = new ObservableCollection<HistoryItem>();
        public ObservableCollection<int> AvailablePorts { get; set; } = new ObservableCollection<int>(Enumerable.Range(1, 10));

        private int _port;

        public int Port
        {
            get { return _port; }
            set { _port = value; OnPropertyChanged(); }
        }

        private bool _isInitialized;

        public bool IsInitialized
        {
            get { return _isInitialized; }
            set { _isInitialized = value; OnPropertyChanged(); }
        }

        private DS9490 _touchpen;

        public MainWindowViewModel()
        {
            Port = AvailablePorts.First();
        }

        public void InitializeTouchpen()
        {
            _touchpen = new DS9490();
            IsInitialized = _touchpen.Initialize(Port);
            if (!IsInitialized)
            {
                AddHistoryItem(string.Empty, $"Unable to initialize touchpen on port {Port}");
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
