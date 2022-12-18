using System;

namespace ComInterface
{
    /// <summary>
    /// ログ出力の基底クラス
    /// </summary>
    public class LogProviderBase
    {
        public string CurDir { set; get; }

        public virtual void WriteLine(string portName, string str)
        {
            Console.WriteLine(LogFmt(portName, str));
        }

        /// <summary>
        /// ログ出力のフォーマット
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string LogFmt(string portName, string str)
        {
            return string.Format("{0:yyyy/MM/dd HH:mm:ss FFF} [{1}] {2}",
                                DateTime.Now,
                                portName,
                                str);
        }

    }
}
