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
        private string selectedAddress = null;

        public FormSearchAddress()
        {
            InitializeComponent();
        }

        private void buttonCancelAddress_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 검색 버튼
        private void buttonSearchAddress_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Go search:{0}", textBoxSearchAddressText.Text);

            RunGetSearchedAddress(textBoxSearchAddressText.Text);
        }
        
        private async Task RunGetSearchedAddress(string searchText)
        {
            // 검색된 주소 리스트 초기화
            if (0 < listBoxSeachedAddress.Items.Count)
                listBoxSeachedAddress.Items.Clear();

            // 주소 검색한 결과 리스트
            HttpClientManager hcManager = new HttpClientManager();
            var result = await hcManager.RunGetSearchedAddress("https://api.poesis.kr/post/search.php?", searchText);
            foreach(var resultItem in result.results)
            {
                listBoxSeachedAddress.Items.Add(resultItem.ko_common + " " + resultItem.ko_jibeon);
            }
        }

        // ListBox 리스트 선택 이벤트 함수
        private void listBoxSeachedAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < listBoxSeachedAddress.Items.Count; i++)
            {
                if (listBoxSeachedAddress.GetSelected(i))
                {
                    selectedAddress = listBoxSeachedAddress.Items[i].ToString();
                    Console.WriteLine(selectedAddress);
                }
            }
        }

        // 확인 버튼
        private void buttonSelectAddress_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public string GetSelectedAddress()
        {
            return selectedAddress;
        }

        // 검색어 입력폼에서 키를 눌렀을때 이벤트 함수
        private void textBoxSearchAddressText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                RunGetSearchedAddress(textBoxSearchAddressText.Text);
            }
        }

        // ListBox 리스트 더블 클릭 이벤트 함수
        private void listBoxSeachedAddress_DoubleClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
