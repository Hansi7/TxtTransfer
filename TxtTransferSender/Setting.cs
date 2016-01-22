using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace TxtTransferSender
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }
        public string IPstring { get; set; }
        public string PortString { get; set; }

        public System.Net.IPEndPoint IPEndPointA { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            this.IPstring = txtIP.Text;
            this.PortString = txtPort.Text;
            System.Net.IPAddress address;
            int port;
            try
            {
                if (int.TryParse(PortString,out port))
                {

                }
                else
                {
                    MessageBox.Show("端口不正确");
                }

                if (System.Net.IPAddress.TryParse(IPstring,out address))
                {
                    IPEndPointA = new System.Net.IPEndPoint(address, port);
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;

            }

            catch (Exception)
            {

                throw;
            }


        }





    }
}
