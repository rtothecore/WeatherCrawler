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
        string formedAddress = null;

        public FormNewAddress()
        {
            InitializeComponent();
        }

        // 주소 검색 버튼
        private void buttonFindAddress_Click(object sender, EventArgs e)
        {
            // 주소 검색창 열기
            FormSearchAddress dlgForSearchAddress = new FormSearchAddress();
            dlgForSearchAddress.ShowDialog();

            // 검색된 주소를 가져옴
            string selectedAddress = dlgForSearchAddress.GetSelectedAddress();
            if(null != selectedAddress)
            {
                string[] splitedAddress = selectedAddress.Split(' ');
                formedAddress = splitedAddress[0] + " " + splitedAddress[1] + " " + splitedAddress[2];
                textBoxNewAddress.Text = formedAddress;
            }
        }

        private void buttonCancelAddAddress_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 주소 추가 버튼
        private void buttonAddAddress_Click(object sender, EventArgs e)
        {
            // address.ini & fwjournal.ini에 동일한 주소 있는지 체크
            AddressIniManager aiManager = new AddressIniManager();
            FwjournalIniManager fiManager = new FwjournalIniManager();
            if (aiManager.IsExistSameAddress(formedAddress) || fiManager.IsExistSameAddress(formedAddress))
            {
                MessageBox.Show("이미 존재하는 주소입니다");
            }
            else
            {
                // address.ini에 주소 추가
                RunGetGPSAndConvertNxNyAndWriteAddr(formedAddress);
            }

            this.Close();
        }

        

        // address.ini에 새로운 주소 쓰기
        private async Task<bool> RunGetGPSAndConvertNxNyAndWriteAddr(string address)
        {
            HttpClientManager hcManager = new HttpClientManager();
            return await hcManager.RunGetGPSAndConvertNxNyAndWriteAddr("http://maps.googleapis.com/maps/api/geocode/json",
                                                        "?sensor=false&language=ko&address=",
                                                        address);
        }

        public string GetFormedAddress()
        {
            return formedAddress;
        }
    }
}
