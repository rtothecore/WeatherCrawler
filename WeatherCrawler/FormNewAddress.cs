using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    public partial class FormNewAddress : Form
    {
        public FormNewAddress()
        {
            InitializeComponent();
        }

        private void buttonFindAddress_Click(object sender, EventArgs e)
        {
            FormSearchAddress dlgForSearchAddress = new FormSearchAddress();
            dlgForSearchAddress.ShowDialog();
        }

        private void buttonCancelAddAddress_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
