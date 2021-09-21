using OneWireComm.ViewModels;
using System;

namespace OneWireComm.Models
{
    public class HistoryItem : VisualElementBase
    {
        private string _serial;

        public string Serial
        {
            get { return _serial; }
            set { _serial = value; OnPropertyChanged(); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        private DateTime _eventTime = DateTime.Now;

        public DateTime EventTime
        {
            get { return _eventTime; }
            set { _eventTime = value; OnPropertyChanged(); }
        }

    }
}
