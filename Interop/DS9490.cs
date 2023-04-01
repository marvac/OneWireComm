using System;
using DalSemi.OneWire;
using DalSemi.OneWire.Adapter;
using OneWireComm.Helpers;
using OneWireComm.Interop;

namespace OneWireComm
{
    public class DS9490 : ITouchpen
    {
        private PortAdapter _adapter = null;
        public string AdapterName => "{DS9490}"; //blue adapter labeled DS9490R# on the back
        public bool IsInitialized => _adapter != null;

        public bool Initialize(int port)
        {
            try
            {
                string portName = $"USB{port}";

                try
                {
                    //throws an exception if the touchpen is not found
                    _adapter = AccessProvider.GetAdapter(AdapterName, portName);
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
            if (_adapter == null)
            {
                return null;
            }

            string serial = null;

            try
            {
                byte[] address = new byte[8];
                if (_adapter.GetFirstDevice(address, 0))
                {
                    serial = HexHelper.GetButtonSerial(address);
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(ex.Message);
            }

            return serial;
        }

        public byte[] GetDataBlock(int byteCount = 16)
        {
            byte[] data = new byte[byteCount];
            try
            {
                for (int i = 0; i < byteCount; i++)
                {
                    int val = _adapter.GetByte();
                    data[i] = BitConverter.GetBytes(val)[0];
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return data;
        }

        public void WriteDataBlock(string hexString)
        {
            byte[] bytes = HexHelper.GetBytesFromString(hexString);
            //todo...
        }
    }
}
