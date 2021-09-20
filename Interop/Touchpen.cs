using System;
using DalSemi.OneWire;
using DalSemi.OneWire.Adapter;

namespace OneWireComm
{
    public class Touchpen
    {
        private PortAdapter _adapter = null;
        public string DeviceSerial { get; set; }
        private int _errorCount = 0;
        public bool IsInitialized => DeviceSerial != null && _adapter != null;

        public bool Initialize(int port)
        {
            try
            {
                DeviceSerial = null;
                string adapterName = "{DS9490}"; //blue adapter labeled DS9490R# on the back
                string portName = $"USB{port}";

                try
                {
                    //throws an exception if the touchpen is not found
                    _adapter = AccessProvider.GetAdapter(adapterName, portName);
                }
                catch (Exception ex)
                {
                    Logger.Debug("Touchpen initialize failed");
                    Logger.Debug(ex.Message);
                    return false;
                }

                if (_adapter != null)
                {
                    _ = _adapter.BeginExclusive(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return false;
        }

        public string GetButtonSerial()
        {
            string serial = null;
            try
            {
                if (_adapter != null)
                {
                    byte[] address = new byte[8];
                    if (_adapter.GetFirstDevice(address, 0))
                    {
                        _errorCount = 0;
                        do
                        {
                            if (_adapter.GetFirstDevice(address, 0))
                            {
                                string converted = $"{address[6]:x2}{address[5]:x2}{address[4]:x2}{address[3]:x2}{address[2]:x2}{address[1]:x2}".ToUpper();

                                if (DeviceSerial == null)
                                {
                                    //set device name to the first scan
                                    DeviceSerial = converted;
                                    return null;
                                }

                                if (converted != DeviceSerial)
                                {
                                    serial = converted;
                                }
                            }
                        }
                        while (_adapter.GetNextDevice(address, 0));
                    }
                    else
                    {
                        _errorCount++;
                        if (_errorCount > 3)
                        {
                            DeviceSerial = null;
                            _errorCount = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(ex.Message);
            }

            return serial;
        }

        public byte[] GetDataBlock()
        {
            return null;
        }
    }
}
