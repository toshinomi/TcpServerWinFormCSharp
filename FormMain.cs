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
    /// <summary>
    /// MainFormのロジック
    /// </summary>
    public partial class FormMain : Form
    {
        private Thread m_thread;
        private TcpListener m_tcpListener;
        private string m_strIpAddress;
        private int m_nPort;
        private bool m_bEnd;

        /// <summary>
        /// スレッド
        /// </summary>
        public Thread Thread
        {
            set { m_thread = value; }
            get { return m_thread; }
        }

        /// <summary>
        /// TCPリスナー
        /// </summary>
        public TcpListener TcpListener
        {
            set { m_tcpListener = value; }
            get { return m_tcpListener; }
        }

        /// <summary>
        /// IPアドレス
        /// </summary>
        public string IpAddress
        {
            set { m_strIpAddress = value; }
            get { return m_strIpAddress; }
        }

        /// <summary>
        /// ポート
        /// </summary>
        public int Port
        {
            set { m_nPort = value; }
            get { return m_nPort; }
        }

        /// <summary>
        /// 終了
        /// </summary>
        public bool End
        {
            set { m_bEnd = value; }
            get { return m_bEnd; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            Init();
        }

        /// <summary>
        /// デスクトラクタ
        /// </summary>
        ~FormMain()
        {
        }

        /// <summary>
        /// 初期化
        /// </summary>
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

        /// <summary>
        /// スタートボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        private void OnBtnClickStart(object sender, EventArgs e)
        {
            m_strIpAddress = textBoxIpAddress.Text.ToString();
            m_nPort = int.Parse(textBoxPort.Text.ToString());

            m_thread = new Thread(DataListener);
            m_thread.Start();
        }

        /// <summary>
        /// データリスナー
        /// </summary>
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
                    if (m_bEnd)
                    {
                        break;
                    }

                    NetworkStream networkStream = client.GetStream();
                    byte[] aryData = new byte[4096];
                    int nSize = networkStream.Read(aryData, 0, aryData.Length);
                    string strGetText = System.Text.Encoding.Default.GetString(aryData, 0, nSize);
                    Invoke(new Action<string>(SetTextRcvData), strGetText);
                }
                Invoke(new Action<string>(SetTextStatus), "Server terminated.");
            }
            catch(Exception)
            {
                string strText = "Server terminated.";
                if (!m_bEnd)
                {
                    strText = "Server terminated abnormally.";
                }
                Invoke(new Action<string>(SetTextStatus), strText);

                return;
            }
        }

        /// <summary>
        /// ステータス表示のテキストボックス設定
        /// </summary>
        /// <param name="_strText">表示文字列</param>
        public void SetTextStatus(string _strText)
        {
            textBoxStatus.Text = _strText.ToString();

            return;
        }

        /// <summary>
        /// データ受信表示のテキストボックス設定
        /// </summary>
        /// <param name="_strText">表示文字列</param>
        public void SetTextRcvData(string _strText)
        {
            textBoxRcvData.Text = _strText.ToString() + "\n";

            return;
        }

        /// <summary>
        /// クリアボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        public void OnBtnClickClear(object sender, EventArgs e)
        {
            textBoxRcvData.Text = "";

            return;
        }

        /// <summary>
        /// ストップボタンのクリックイベント
        /// </summary>
        /// <param name="sender">オブジェクト</param>
        /// <param name="e">イベントのデータ</param>
        public void OnBtnClickStop(object sender, EventArgs e)
        {
            m_bEnd = true;
            m_tcpListener.Stop();

            return;
        }
    }
}