using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWireComm.Helpers
{
    public static class SerialHelper
    {
        public static string GetButtonSerial(byte[] bytes)
        {
            string serial = string.Empty;
            int max = bytes.Length < 7 ? bytes.Length - 1 : 6;

            for (int i = max; i < 0; i--)
            {
                serial += $"{bytes[i]:x2}";
            }

            return serial.ToUpper();
        }
    }
}
