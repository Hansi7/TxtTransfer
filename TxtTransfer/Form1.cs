using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;

namespace TxtTransfer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ListView.CheckForIllegalCrossThreadCalls = false;
            listView1.Items.Clear();
            setC = setclipboard;
        }
        int n = 0;
        Action<string> setC;
        private void Listen()
        {
            System.Net.IPHostEntry myEntry = System.Net.Dns.GetHostEntry((System.Net.Dns.GetHostName()));

            IPAddress address = null;

            for (int i = 0; i < myEntry.AddressList.Length; i++)
            {
                if(myEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    address = myEntry.AddressList[i];
                }
            }

            System.Net.Sockets.TcpListener lister = new System.Net.Sockets.TcpListener(address,int.Parse(txt_port.Text));
            lister.Start(10);
            while (true)
            {
                var client = lister.AcceptTcpClient();
                System.Threading.ThreadPool.QueueUserWorkItem(process, client);

            }
        }
        private void process(object obj)
        {
            var client = obj as System.Net.Sockets.TcpClient;
            if (client != null )
            {
                var stream = client.GetStream();
                StreamReader sr = new StreamReader(stream,Encoding.Default);
                var str = sr.ReadToEnd();
                if (str.Trim()!=string.Empty)
                {
                    this.listView1.Items.Add(str);
                    if (checkBox1.Checked)
                    {
                        //Clipboard.SetText(str);
                        this.Invoke(setC, str);
                    }

                }
                
                client.Close();
            }
        }
        private void setclipboard(string str)
        {
            Clipboard.SetText(str);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count==1)
            {
                System.Windows.Forms.Clipboard.SetText(this.listView1.SelectedItems[0].Text);
                textBox1.Text = (n++).ToString();
            }
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            int po;
            if (int.TryParse(txt_port.Text,out po))
            {
                System.Threading.Thread th = new System.Threading.Thread(Listen);
                th.IsBackground = true;
                th.Start();
                btn_Start.Enabled = false;
                btn_Start.Text = "正在监听端口";
                txt_port.Enabled = false;
            }
            else
            {
                MessageBox.Show("端口号不正确");
            }
        }
    }
}
