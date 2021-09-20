using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWireComm
{
    public static class Logger
    {
        private static readonly ILogger _logger = new ConsoleLogger();

        public static void Debug(string text)
        {
            _logger.Debug(text);
        }

        public static void Warning(string text)
        {
            _logger.Warning(text);
        }

        public static void Success(string text)
        {
            _logger.Success(text);
        }

        public static void Error(string text)
        {
            _logger.Error(text);
        }

        public static void Error(Exception ex)
        {
            _logger.Error(ex);
        }
    }
}
