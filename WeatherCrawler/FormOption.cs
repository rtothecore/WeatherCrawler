using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    public partial class FormOption : Form
    {
        DbIniManager diManager = null;

        public FormOption()
        {
            InitializeComponent();
        }

        public void SetDBINI(DbIniManager dimanager)
        {
            diManager = dimanager;
        }

        public string GetTextBoxIpAddress()
        {
            return textBoxIpAddress.Text;
        }

        public void SetTextBoxIpAddress(string text)
        {
            textBoxIpAddress.Text = text;
        }

        private void buttonOptionCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string GetTextBoxDBName()
        {
            return textBoxDBName.Text;
        }

        public void SetTextBoxDBName(string text)
        {
            textBoxDBName.Text = text;
        }

        public string GetTextBoxCollectionName()
        {
            return textBoxCollectionName.Text;
        }

        public void SetTextBoxCollectionName(string text)
        {
            textBoxCollectionName.Text = text;
        }

        public string GetTextBoxId()
        {
            return textBoxId.Text;
        }

        public void SetTextBoxId(string text)
        {
            textBoxId.Text = text;
        }

        public string GetTextBoxPw()
        {
            return textBoxPw.Text;
        }

        public void SetTextBoxPw(string text)
        {
            textBoxPw.Text = text;
        }

        private void buttonOptionOk_Click(object sender, EventArgs e)
        {
            diManager.DeleteIni();
            diManager.WriteIni(this);
        }
    }
}
