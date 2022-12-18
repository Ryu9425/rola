using System;
using System.Linq;
using System.IO.Ports;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using RJCP.IO.Ports;

namespace ComInterface
{
    /// <summary>
    /// シリアルの通信
    /// </summary>
    public class CommCdcDirect : CommCdc
    {

        private SerialPortStream _port;

        public CommCdcDirect()
            : base()
        {
            PortName = "COM1";
            _ComPort = "COM1";
            _log = new LogProviderFile();
            _log.CurDir = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Log");
        }

        public override void Open()
        {
            _log.WriteLine(PortName, "Open");

            if (IsOpened())
            {
                // open済み
                return;
            }

            string[] ports = SerialPort.GetPortNames();
            if (ports.Count() <= 0)
            {
                throw new ApplicationException("COM Port が見つかりません。");
            }
            bool existsPort = false;
            foreach (string s in ports)
            {
                if (s == ComPort)
                { 
                    existsPort = true;
                    break;
                }
            }
            if (!existsPort)
            {
                throw new ApplicationException("COM Port の指定が不正です。");
            }

            //m_SerialPort = new SerialPort(ComPort);
            //m_SerialPort.BaudRate = this.BaudRate;
            //m_SerialPort.DataBits = this.DataBits;
            //m_SerialPort.Parity = this.ParityType;
            //m_SerialPort.StopBits = this.StopBitType;
            //m_SerialPort.Encoding = System.Text.Encoding.GetEncoding("US-ASCII");
            //m_SerialPort.Open();

            _port = new SerialPortStream(ComPort);
            _port.OpenDirect();
        }

        public override void Close()
        {
            _log.WriteLine(PortName, "Close");
            if (_port != null)
            {
                _port.Close();
                _port = null;
            }
        }

        public override bool IsOpened()
        {
            if (_port != null)
            {
                // open済み
                return true;
            }
            return false;
        }

        public override string ReadLineStr()
        {
            string buf;
            buf = "";
            byte[] data;
            data = new byte[1];
            var sb = new System.Text.StringBuilder();
            while (_port.BytesToRead > 0 )        // 必ず0より大きくなる
            {
                data[0] = (byte)_port.ReadByte();   // -1 は来ないので範囲チェックの必要は無い
                sb.AppendFormat("{0:X2} ", data[0]);
                // ASCIIエンコード
                buf += System.Text.Encoding.ASCII.GetString(data);
                buf.Trim('\0');
            }

            if (sb.Length > 0)
            {
                _log.WriteLine(PortName, "Read " + sb.ToString());
            }

            // コマンドのデリミタはCRLF
            //if (!string.IsNullOrEmpty(buf))
            //{
            //    // データを受信した場合のみログに出力する
            //    //string buf = m_SerialPort.ReadExisting();
            //    _log.WriteLine(PortName, "Read " + buf);
            //}
            return buf;
        }

        public override void WriteStr(string str)
        {
            byte[] buf = System.Text.Encoding.ASCII.GetBytes(str);
            string logStr = string.Format("{0} ({1})",
                                            str,
                                            string.Join(" ", buf.Select(x => string.Format("{0,0:X2}", x))));
            _log.WriteLine(_PortName, "Write : " + logStr);
            _port.WriteLine(str);
        }

        public override void WriteBytes(byte[] src)
        {
            string log = string.Join(" ", src.Select(x => string.Format("{0,0:X2}", x)));
            _log.WriteLine(_PortName, "Write : " + log);
            _port.Write(src, 0, src.Length);
        }

        public override byte[] ReadBytes()
        {
            var buf = new List<byte>();

            while (_port.BytesToRead > 0)        // 必ず0より大きくなる
            {
                buf.Add((byte)_port.ReadByte());   // -1 は来ないので範囲チェックの必要は無い
            }

            // データを受信した場合のみログに出力する
            //string log = string.Join(" ", buf.Select(x => string.Format("{0,0:X2}", x)));
            //_log.WriteLine(PortName, "Read " + log);

            return buf.ToArray();
        }

        public override byte ReadByte(out bool isRecv)
        {
            byte buf;

            if (_port.BytesToRead <= 0)
            {
                isRecv = false;
                return 0;
            }

            int ret = _port.ReadByte();
            if (ret < 0)
            {
                isRecv = false;
                return 0;
            }
            buf = Convert.ToByte(ret);

            // データを受信した場合のみログに出力する
            string log = string.Format("{0,0:X2}", buf);
            _log.WriteLine(PortName, "ReadByte " + log);

            isRecv = true;
            return buf;
        }
    }
}
