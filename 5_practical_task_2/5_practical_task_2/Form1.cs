using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5_practical_task_2
{
    public partial class Form1 : Form
    {
       private TcpListener listener = new TcpListener(IPAddress.Any, 2023);
        public string text;
        public string key;
        public string signature;
        public byte[] signedData;
        int counter;
        public List<string> alioalio = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { Thread receiverThread = new Thread(() =>
        {
            start();

        });
            receiverThread.Start();
        }
        public void start()
        {

            try
            {
                while (true)
                {
                    listener.Start();
                    using var client = listener.AcceptTcpClient();
                    using var stream = client.GetStream();
                    {

                        {
                            using MemoryStream ms = new MemoryStream();

                            var buffer = new byte[1024];
                            int bufferSize;
                            while ((bufferSize = stream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, bufferSize);

                            }
                            if (counter == 1)
                            {
                                signedData = ms.ToArray();
                            }
                            else if(counter == 2 || counter == 0)
                            {
                                alioalio.Add(Encoding.ASCII.GetString(ms.ToArray(), 0, (int)ms.Length));
                                if(counter == 2)
                                {
                                    if (SignatureVerify.VerifySignedHash(alioalio[0], signedData, alioalio[1]))
                                    {
                                        Invoke(new Action(() =>
                                        {
                                            label1.Text = "Verified";
                                            alioalio.Clear();
                                            counter = -1;
                                        }));
                                        
                                    }
                                    else
                                        
                                    {
                                        Invoke(new Action(() =>
                                        {
                                            label1.Text = "Signatures did not match";
                                            alioalio.Clear();
                                            counter = -1;
                                        }));
                                    }
                                }
                                
                            }
                            counter++;
                            }
                        }
                    }
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {

            }
        }

      
    }
}
