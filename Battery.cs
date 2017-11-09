using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace BatteryInfo
{
    class Battery
    {
        private static Battery instance;

        private double percent;
        private PowerLineStatus connectionType;
        private int remainingTime;

        public double Percent
        { get { return percent; } }
        public PowerLineStatus ConnectionType
        { get { return connectionType; } }
        public int RemainingTime
        { get { return remainingTime; } }

        public void UpdateData() {
            percent = SystemInformation.PowerStatus.BatteryLifePercent * 100;
            connectionType = SystemInformation.PowerStatus.PowerLineStatus;
            remainingTime = SystemInformation.PowerStatus.BatteryLifeRemaining / 60;
        }

        private Battery()
        {
            UpdateData();
        }

        public static Battery GetInstance()
        {
            if (instance == null)
                instance = new Battery();
            return instance;
        }
    }
}
