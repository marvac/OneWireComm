using System;

namespace OneWireComm
{
    public interface ILogger
    {
        void Debug(string text);
        void Warning(string text);
        void Success(string text);
        void Error(string text);
        void Error(Exception ex);
    }
}
