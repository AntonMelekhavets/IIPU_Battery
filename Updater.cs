using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BatteryInfo
{
    public class Updater
    {
        private Thread UpdaterThread;
        private BatteryInfoForm Form;

        private bool isInterrupted;
        public bool IsInterrupted
        { set { isInterrupted = value; } }

        public Updater(BatteryInfoForm form)
        {
            Form = form;
            UpdaterThread = new Thread(this.InformationUpdaterThread);
            isInterrupted = false;
            UpdaterThread.Start();
        }

        private void InformationUpdaterThread()
        {
            const int Sleep_Time = 500;
            while (!isInterrupted)
            {
                Thread.Sleep(Sleep_Time);
                Battery.GetInstance().UpdateData();
                try
                { Form.Invoke((MethodInvoker)delegate { Form.UpdateFormData(); }); }
                catch
                { break; }
            }
        }
    }
}
