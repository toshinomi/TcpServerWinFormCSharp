using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpServerWinFormCSharp
{
    public partial class FormTcpServer : Form
    {
        private Thread m_thread;
        private TcpListener m_tcpListener;
        private string m_strIpAddress;
        private int m_nPort;
        private bool m_bEnd;

        public Thread Thread
        {
            set { m_thread = value; }
            get { return m_thread; }
        }

        public TcpListener TcpListener
        {
            set { m_tcpListener = value; }
            get { return m_tcpListener; }
        }

        public string IpAddress
        {
            set { m_strIpAddress = value; }
            get { return m_strIpAddress; }
        }

        public int Port
        {
            set { m_nPort = value; }
            get { return m_nPort; }
        }

        public bool End
        {
            set { m_bEnd = value; }
            get { return m_bEnd; }
        }

        public FormTcpServer()
        {
            InitializeComponent();

            Init();
        }

        ~FormTcpServer()
        {
        }

        public void Init()
        {
            m_thread = null;
            m_tcpListener = null;
            m_strIpAddress = "127.0.0.1";
            m_nPort = 9999;
            m_bEnd = false;

            textBoxIpAddress.Text = m_strIpAddress;
            textBoxPort.Text = m_nPort.ToString();
        }

        private void OnBtnClickStart(object sender, EventArgs e)
        {
            m_strIpAddress = textBoxIpAddress.Text.ToString();
            m_nPort = int.Parse(textBoxPort.Text.ToString());

            m_thread = new Thread(DataListener);
            m_thread.Start();
        }

        public void DataListener()
        {
            try
            {
                m_tcpListener = new TcpListener(IPAddress.Parse(m_strIpAddress), m_nPort);
                m_tcpListener.Start();

                Invoke(new Action<string>(SetTextStatus), "Server started.");

                TcpClient client = m_tcpListener.AcceptTcpClient();

                while (client.Connected)
                {
                    NetworkStream networkStream = client.GetStream();
                    byte[] aryData = new byte[4096];
                    int nSize = networkStream.Read(aryData, 0, aryData.Length);
                    string strGetText = System.Text.Encoding.Default.GetString(aryData, 0, nSize);
                    Invoke(new Action<string>(SetTextRcvData), strGetText);
                }
            }
            catch(Exception)
            {
                Invoke(new Action<string>(SetTextStatus), "Server terminated abnormally.");

                return;
            }
        }

        public void SetTextStatus(string _strText)
        {
            textBoxStatus.Text = _strText.ToString();

            return;
        }

        public void SetTextRcvData(string _strText)
        {
            textBoxRcvData.Text = _strText.ToString() + "\n";

            return;
        }

        public void OnBtnClickClear(object sender, EventArgs e)
        {
            textBoxRcvData.Text = "";

            return;
        }

        public void OnBtnClickStop(object sender, EventArgs e)
        {
            m_bEnd = true;
            m_tcpListener.Stop();

            return;
        }
    }
}