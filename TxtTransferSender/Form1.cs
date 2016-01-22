using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TxtTransferSender
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        System.Net.IPEndPoint SendToIP = null;
        //send
        private void button1_Click(object sender, EventArgs e)
        {
            System.Net.Sockets.NetworkStream stream = null;
            System.Net.Sockets.TcpClient client = null;
            if (this.SendToIP==null)
            {
                this.SendToIP = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("192.168.0.2"), 6666);
            }

            try
            {
                if (SendToIP!=null)
                {
                    client = new System.Net.Sockets.TcpClient(SendToIP.Address.ToString(), SendToIP.Port);
                    if (!client.Connected)
                    {
                        return;
                    }
                    var txt = System.Windows.Forms.Clipboard.GetText(TextDataFormat.UnicodeText);
                    if (txt.Trim() == string.Empty)
                    {
                        client.Close();
                        return;
                    }
                    var buffer = Encoding.Default.GetBytes(txt);

                    stream = client.GetStream();
                    stream.Write(buffer, 0, (int)buffer.Length);
                    stream.Close();
                    client.Close();
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        //setting
        private void button2_Click(object sender, EventArgs e)
        {
            var se = new Setting();
            if (se.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SendToIP = se.IPEndPointA;
            }
        }
        //exit
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
