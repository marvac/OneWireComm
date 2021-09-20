using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalSemi.OneWire;
using DalSemi.OneWire.Adapter;

namespace OneWireComm
{
    public class Touchpen
    {
        private static PortAdapter _adapter = null;
        private static string _deviceName = null;
        private static int _errorCount = 0;
        public bool IsInitialized => _deviceName != null && _adapter != null;

        public bool Initialize()
        {
            try
            {
                _deviceName = null;
                string adapterName = "{DS9490}"; //blue adapter labeled DS9490R# on the back
                string portName = "USB1"; //this port should always work

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
                    _adapter.BeginExclusive(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }

            return false;
        }

        public string GetSerial()
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

                                if (_deviceName == null)
                                {
                                    //set device name to the first scan
                                    _deviceName = converted;
                                    return null;
                                }

                                if (converted != _deviceName)
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
                            _deviceName = null;
                            _errorCount = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(ex);
            }

            return serial;
        }

    }
}
