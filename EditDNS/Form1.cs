using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditDNS
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        public bool Start_active = false;
        public void ExecuteAsAdmin(string caulenh)
        {
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            //proc.StartInfo.Verb = "runas";
            proc.StartInfo.FileName = "netsh.exe";
            proc.StartInfo.Arguments = caulenh;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();

        }
        public static NetworkInterface[] GetNicList() {
            NetworkInterface[] Nic = NetworkInterface.GetAllNetworkInterfaces();
            return Nic;
        }
        public static List<string> GetNicList1()
        {
            List<string> Nic_name = new List<string>();
            NetworkInterface[] Nic = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var item in Nic)
            {
                Nic_name.Add(item.Name);
            }
            return Nic_name;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var List_name = GetNicList();
            //for (int i = 0; i < count; i++)
            //{

            //    this.ExecuteAsAdmin("interface ip set dns "+List_name[i]+ " static 52.77.28.202");
            //}
            foreach (NetworkInterface item in List_name)
            {
                var Ip_DNS = item.NetworkInterfaceType;
                IPInterfaceProperties adapterProperties = item.GetIPProperties();
                IPAddressCollection dnsServers = adapterProperties.DnsAddresses;
                if (dnsServers.Count > 0)
                {
                    foreach (IPAddress dns in dnsServers)
                    {
                        if (dns.ToString() != "52.77.28.202")
                        {
                            this.ExecuteAsAdmin("interface ip set dns " + item.Name + " static 52.77.28.202");
                        }
                    }
                }
            }
            this.Start_active = true;
            button2.Enabled = true;
            button1.Enabled = false;
            label1.Text = "Kích hoạt thành công.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var List_name = GetNicList1();
            int count = List_name.Count;
            for (int i = 0; i < count; i++)
            {
                this.ExecuteAsAdmin("interface ip set dns " + List_name[i] + " dhcp");
            }
            this.Start_active = false;
            button1.Enabled = true;
            button2.Enabled = false;
            label1.Text = "Vô hiệu hóa thành công.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Close(object sender, FormClosedEventArgs e)
        {
            if (button1.Enabled == false)
            {
                var List_name = GetNicList1();
                int count = List_name.Count;
                for (int i = 0; i < count; i++)
                {
                    this.ExecuteAsAdmin("interface ip set dns " + List_name[i] + " dhcp");
                }
            }
        }

        private void Close_ing(object sender, FormClosingEventArgs e)
        {
            if (button1.Enabled == true)
            {
                var List_name = GetNicList1();
                int count = List_name.Count;
                for (int i = 0; i < count; i++)
                {
                    this.ExecuteAsAdmin("interface ip set dns " + List_name[i] + " dhcp");
                }
            }
        }
    }
}
