using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherCrawler
{
    class LogOutputManager
    {
        bool _log_is_paused = false;

        public LogOutputManager(string indexNo, TextBox outputTB)
        {
            _log_is_paused = false;
            Output(indexNo, outputTB);
        }

        public async Task Output(string indexNo, TextBox outputTB)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\Log\\" + indexNo + ".log";
            FileStream _log_stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader _stream_reader = new StreamReader(_log_stream);

            string time_stamp = DateTime.Now.ToString();

            outputTB.AppendText("\r\nBegin Log Capture @ " + time_stamp + "\r\n\r\n");

            outputTB.Clear();

            while (_log_is_paused == false)
            {
                outputTB.AppendText(_stream_reader.ReadToEnd());
                outputTB.ScrollToCaret();
                await Task.Delay(50);
            }
        }

        public void StopOutput()
        {
            _log_is_paused = true;
        }

    }
}
