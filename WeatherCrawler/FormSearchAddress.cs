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
    public partial class FormSearchAddress : Form
    {
        public FormSearchAddress()
        {
            InitializeComponent();
        }

        private void buttonCancelAddress_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
