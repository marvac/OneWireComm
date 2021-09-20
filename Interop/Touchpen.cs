using System;
using DalSemi.OneWire;
using DalSemi.OneWire.Adapter;
using OneWireComm.Helpers;

namespace OneWireComm
{
    public class Touchpen
    {
        private PortAdapter _adapter = null;
        public bool IsInitialized => _adapter != null;

        public bool Initialize(int port)
        {
            try
            {
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
                        serial = SerialHelper.GetButtonSerial(address);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(ex.Message);
            }

            return serial;
        }

        public byte[] GetDataBlock(string serial)
        {
            if (_adapter != null && !string.IsNullOrEmpty(serial))
            {
                byte[] arr = new byte[16];
                _adapter.GetBlock(arr, 0, 16);
            }

            return null;
        }
    }
}
