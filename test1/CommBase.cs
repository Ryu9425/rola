using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ComInterface
{
    /// <summary>
    /// 通信の基底クラス
    /// </summary>
    public class CommBase : System.IDisposable
    {
        protected string _PortName = "CommBase";

        /// <summary>
        /// ポート名を指定する
        /// </summary>
        public string PortName
        {
            set
            {
                _PortName = value;
            }
            get
            {
                return _PortName;
            }
        }

        protected LogProviderBase _log = new LogProviderBase();
        public LogProviderBase Log
        {
            set
            {
                _log = value;
            }
            get
            {
                return _log;
            }
        }

        public CommBase()
        {
            
        }

        public virtual void Open()
        {
            _log.WriteLine(_PortName, "Open");
        }

        public virtual void Close()
        {
            _log.WriteLine(_PortName, "Close");
        }

        public virtual bool IsOpened()
        {
            return true;
        }

        public void Dispose()
        {
            Close();
        }

        public virtual string ReadLineStr()
        {
            _log.WriteLine(_PortName, "Read : ");

            return string.Empty;
        }

        public virtual void WriteStr(string str)
        {
            byte[] buf = System.Text.Encoding.ASCII.GetBytes(str);
            string logStr = string.Format("{0} ({1})",
                                            str,
                                            string.Join(" ", buf.Select(x => string.Format("{0,0:X2}", x))));
            _log.WriteLine(_PortName, "Write : " + logStr);
        }

        public virtual void WriteBytes(byte[] src)
        {
            string log = string.Join(" ", src.Select(x => string.Format("{0,0:X2}", x)));
            _log.WriteLine(_PortName, "Write : " + log);
        }

        public virtual byte[] ReadBytes()
        {
            return new byte[0];
        }

        public virtual byte ReadByte(out bool isRecv)
        {
            isRecv = true;
            return 0x00;
        }
    }
}
