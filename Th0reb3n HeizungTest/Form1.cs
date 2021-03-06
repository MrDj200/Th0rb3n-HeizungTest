﻿using Newtonsoft.Json;
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
        Dictionary<string, string> adressen = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string preurl = "http://192.168.1.7:8080";
            adressen.Clear();
            //Dictionary<string, string> adressen = new Dictionary<string, string>();
            adressen.Add("Gesamtverbrauch", "/user/var/40/10021/0/0/12016");
            adressen.Add("Inhalt", "/user/var/40/10021/0/0/12011");
            adressen.Add("AussenTemp", "/user/var/40/10241/0/0/12197");

            
            

            foreach (var item in adressen)
            {
				WebClient client = new WebClient();
				client.DownloadStringCompleted += Client_DownloadStringCompleted;

                try
                {
                    Uri address = new Uri($"{preurl}{item.Value}");
                    client.DownloadStringAsync(address);
                }
                catch (WebException exp)
                {
                    label4.Text = $"Something went wrong!";
                    textBox4.Text = exp.Message;
                }
            }

            
            /*if (string.IsNullOrEmpty(werte["Gesamtverbrauch"].Value))
            {
                label4.Text = "Empty!";
            }*/

            

            //File.WriteAllLines()

            /*textBox1.Text = werte["Gesamtverbrauch"].Value;
            textBox2.Text = werte["Inhalt"].Value;
            textBox4.Text = werte["AussenTemp"].Value;*/
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(e.Result);

			textBox5.Text = e.Result;


			KeyValuePair<string, string> shit = adressen.Where(k => k.Value == doc["eta"]["value"].Attributes["uri"].Value).FirstOrDefault();

            TextBox tempBox = new TextBox();
            bool failed = false;

            switch (shit.Key)
            {
                case "Gesamtverbrauch":
                    tempBox = textBox1;
                    break;
                case "Inhalt":
                    tempBox = textBox2;
                    break;
                case "AussenTemp":
                    tempBox = textBox3;
                    break;
                default:
					label4.Text = "Oopsie woopsie exp made a fuckie wuckie! no u";
                    textBox5.Text = doc.OuterXml;
                    failed = true;

                    break;
            }

            if (!failed)
            {
                tempBox.Text = doc.InnerText;
            }
        }
    }
}
/*Informationen über das Aufrufen von JIT-Debuggen
anstelle dieses Dialogfelds finden Sie am Ende dieser Meldung.

************** Ausnahmetext **************
System.Collections.Generic.KeyNotFoundException: Der angegebene Schlüssel war nicht im Wörterbuch angegeben.
   bei System.Collections.Generic.Dictionary`2.get_Item(TKey key)
   bei Th0reb3n_HeizungTest.Form1.button1_Click(Object sender, EventArgs e)
   bei System.Windows.Forms.Control.OnClick(EventArgs e)
   bei System.Windows.Forms.Button.OnClick(EventArgs e)
   bei System.Windows.Forms.Button.OnMouseUp(MouseEventArgs mevent)
   bei System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)
   bei System.Windows.Forms.Control.WndProc(Message& m)
   bei System.Windows.Forms.ButtonBase.WndProc(Message& m)
   bei System.Windows.Forms.Button.WndProc(Message& m)
   bei System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
   bei System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
   bei System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)


************** Geladene Assemblys **************
mscorlib
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.3062.0 built by: NET472REL1.
    CodeBase: file:///C:/Windows/Microsoft.NET/Framework/v4.0.30319/mscorlib.dll.
----------------------------------------
Th0reb3n HeizungTest
    Assembly-Version: 1.0.0.0.
    Win32-Version: 1.0.0.0.
    CodeBase: file:///C:/Users/Thorben%20Renfordt/Downloads/Th0reb3n%20HeizungTest.exe.
----------------------------------------
System.Windows.Forms
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.3062.0 built by: NET472REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/System.Windows.Forms/v4.0_4.0.0.0__b77a5c561934e089/System.Windows.Forms.dll.
----------------------------------------
System
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.3062.0 built by: NET472REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/System/v4.0_4.0.0.0__b77a5c561934e089/System.dll.
----------------------------------------
System.Drawing
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.3062.0 built by: NET472REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/System.Drawing/v4.0_4.0.0.0__b03f5f7f11d50a3a/System.Drawing.dll.
----------------------------------------
System.Configuration
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.3062.0 built by: NET472REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/System.Configuration/v4.0_4.0.0.0__b03f5f7f11d50a3a/System.Configuration.dll.
----------------------------------------
System.Core
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.3081.0 built by: NET472REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/System.Core/v4.0_4.0.0.0__b77a5c561934e089/System.Core.dll.
----------------------------------------
System.Xml
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.3062.0 built by: NET472REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/System.Xml/v4.0_4.0.0.0__b77a5c561934e089/System.Xml.dll.
----------------------------------------
mscorlib.resources
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.2558.0 built by: NET471REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/mscorlib.resources/v4.0_4.0.0.0_de_b77a5c561934e089/mscorlib.resources.dll.
----------------------------------------
System.Windows.Forms.resources
    Assembly-Version: 4.0.0.0.
    Win32-Version: 4.7.2558.0 built by: NET471REL1.
    CodeBase: file:///C:/Windows/Microsoft.Net/assembly/GAC_MSIL/System.Windows.Forms.resources/v4.0_4.0.0.0_de_b77a5c561934e089/System.Windows.Forms.resources.dll.
----------------------------------------

************** JIT-Debuggen **************
Um das JIT-Debuggen (Just-In-Time) zu aktivieren, muss in der
Konfigurationsdatei der Anwendung oder des Computers
(machine.config) der jitDebugging-Wert im Abschnitt system.windows.forms festgelegt werden.
Die Anwendung muss mit aktiviertem Debuggen kompiliert werden.

Zum Beispiel:

<configuration>
    <system.windows.forms jitDebugging="true" />
</configuration>

Wenn das JIT-Debuggen aktiviert ist, werden alle nicht behandelten
Ausnahmen an den JIT-Debugger gesendet, der auf dem
Computer registriert ist, und nicht in diesem Dialogfeld behandelt.


    
<?xml version="1.0" encoding="utf-8"?><eta version="1.0" xmlns="http://www.eta.co.at/rest/v1"><value uri="/user/var/40/10241/0/0/12197" strValue="15,5" unit="Â°C" decPlaces="1" scaleFactor="10" advTextOffset="0">154</value></eta>
 */