using System;
using System.IO;
using System.Text;

namespace ComInterface
{
    public class LogProviderFile : LogProviderBase, IDisposable
    {
        public void Dispose()
        {
            if (_sw != null)
            {
                _sw.Flush();
                _sw.Close();
                _sw = null;
            }
        }

        private StreamWriter _sw = null;

        public override void WriteLine(string portName, string str)
        {
            if (_sw == null)
            {
                string dir = Path.GetDirectoryName(CurDir);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string logPath = Path.Combine(dir, string.Format("{0}_{1:yyyyMMdd_HHmmss}.log", portName, DateTime.Now));
              //  Encoding enc = Encoding.GetEncoding("shift-jis");
                _sw = new StreamWriter(logPath, true);
            }
            _sw.WriteLine(LogFmt(portName, str));
            _sw.Flush();
        }

    }
}
