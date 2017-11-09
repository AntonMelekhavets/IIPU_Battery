using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Text;

namespace BatteryInfo
{
    class Screen
    {
        private static Screen instance;
        public int InitialTime { get; private set; }

        public static Screen GetInstance()
        {
            if (instance == null)
            {
                instance = new Screen();
                instance.InitialTime = instance.GetInitialTime();
            }
            return instance;
        }

        private int GetInitialTime()
        {
            var сmd = new Process();
            сmd.StartInfo.UseShellExecute = false;
            сmd.StartInfo.RedirectStandardOutput = true;
            сmd.StartInfo.StandardOutputEncoding = Encoding.GetEncoding(866);
            сmd.StartInfo.FileName = "cmd.exe";
            сmd.StartInfo.Arguments = "/c powercfg /q";
            сmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            сmd.Start();

            var hexSecString = сmd.StandardOutput.ReadToEnd().Split("GUID".ToCharArray()).
                Single(s => s.Contains("Отключать экран через")).
                Split("\r\n".ToCharArray())[12].Split(' ').Last();

            return Convert.ToInt32(hexSecString, 16) / 60;
        }

        public void SetSwitchingTime(int time)
        {
            var cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = "/c powercfg /x -monitor-timeout-dc " + time;
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.Start();
        }
    }
}
