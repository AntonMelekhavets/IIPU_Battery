using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BatteryInfo
{
    public partial class BatteryInfoForm : Form
    {
        private Battery PCBattery;
        private Screen PCScreen;
        private Updater Updater;

        public BatteryInfoForm()
        {
            InitializeComponent();
            PCBattery = Battery.GetInstance();
            PCScreen = Screen.GetInstance();
            Updater = new Updater(this);
        }

        public void UpdateFormData()
        {
            chargeLevelLabel.Text = String.Format("{0:0.00}", PCBattery.Percent) + " %";
            if ((PCBattery.ConnectionType == PowerLineStatus.Online))
            {
                connectionTypeLabel.Text = "Сеть";
                remainingTimeLabel.Text = "Недоступно";
            }
            else
            {
                connectionTypeLabel.Text = "Аккумулятор";
                remainingTimeLabel.Text = (PCBattery.RemainingTime.ToString() + " мин");
            }
        }

        private void turnOffTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PCBattery.ConnectionType == PowerLineStatus.Offline)
            {
                PCScreen.SetSwitchingTime(int.Parse(turnOffTimeComboBox.Text));
            }
        }

        private void BatteryInfoForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Updater.IsInterrupted = true;
            PCScreen.SetSwitchingTime(PCScreen.InitialTime);
        }

        private void connectionTypeTextLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
