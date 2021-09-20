using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneWireComm
{
    public class ConsoleLogger : ILogger
    {
        public void Debug(string text) => WriteText(text, ConsoleColor.White);

        public void Warning(string text) => WriteText(text, ConsoleColor.DarkYellow);

        public void Success(string text) => WriteText(text, ConsoleColor.Green);

        public void Error(string text) => WriteText(text, ConsoleColor.Red);

        public void Error(Exception ex) => WriteText(ex?.Message, ConsoleColor.Red);

        private void WriteText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
