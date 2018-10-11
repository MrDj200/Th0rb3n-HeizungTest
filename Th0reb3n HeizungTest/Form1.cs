using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Th0reb3n_HeizungTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string preurl = "http://192.168.1.7:8080/user/var";

            Dictionary<string, XmlDocument> werte = new Dictionary<string, XmlDocument>();
            Dictionary<string, string> adressen = new Dictionary<string, string>();
            adressen.Add("Gesamtverbrauch", "/40/10021/0/0/12016");
            adressen.Add("Inhalt", "/40/10021/0/0/12011");
            adressen.Add("AussenTemp", "/40/10241/0/0/12197");

            foreach (var item in adressen)
            {
                WebClient client = new WebClient();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(client.DownloadString(new Uri($"{preurl}{item.Value}")));

                werte.Add(item.Key, doc);
            }

            //File.WriteAllLines()

            textBox1.Text = werte["Gesamtverbrauch"].Value;
            textBox2.Text = werte["Inhalt"].Value;
            textBox3.Text = werte["AussenTemp"].Value;



        }
    }
}
