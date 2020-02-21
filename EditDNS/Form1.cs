using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
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
        public static List<string> GetNicList() {
            NetworkInterface[] Nic = NetworkInterface.GetAllNetworkInterfaces();
            List<string> Nic_out = new List<string>();
            foreach (NetworkInterface item in Nic)
            {
                Nic_out.Add(item.Name);
            }
            
            return Nic_out;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var List_name = GetNicList();
            int count = List_name.Count;
            for (int i = 0; i < count; i++)
            {
                this.ExecuteAsAdmin("interface ip set dns "+List_name[i]+ " static 52.77.28.202");
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var List_name = GetNicList();
            int count = List_name.Count;
            for (int i = 0; i < count; i++)
            {
                this.ExecuteAsAdmin("interface ip set dns " + List_name[i] + " dhcp");
            }
        }
    }
}
