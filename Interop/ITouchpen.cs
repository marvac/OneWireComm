namespace OneWireComm.Interop
{
    public interface ITouchpen
    {
        string AdapterName { get; }
        bool Initialize(int port);
        string GetButtonSerial();
        byte[] GetDataBlock(int byteCount = 16);
        void WriteDataBlock(string hexString);
    }
}
