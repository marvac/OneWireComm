using System.Globalization;
using System.Text.RegularExpressions;

namespace OneWireComm.Helpers
{
    public static class HexHelper
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

        public static byte[] GetBytesFromString(string input)
        {
            string sanitized = Regex.Replace(input.ToUpper(), "[^0-9A-F]", string.Empty);
            byte[] arr = new byte[sanitized.Length / 2];
            for (int i = 0; i < sanitized.Length; i += 2)
            {
                arr[i / 2] = byte.Parse(sanitized.Substring(i, 2), NumberStyles.AllowHexSpecifier);
            }

            return arr;
        }
    }
}
