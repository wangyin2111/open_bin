using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace open_bin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Mytext = "";
            int file_len;
            int read_len;
            byte[] binchar = new byte[] { };

            FileStream Myfile = new FileStream("test.bin", FileMode.Open, FileAccess.Read);
            BinaryReader binreader = new BinaryReader(Myfile);

            file_len = (int)Myfile.Length;//获取bin文件长度

            while (file_len > 0)
            {
                if (file_len / 256 > 0)//一次读取256字节
                    read_len = 256;
                else                   //不足256字节按实际长度读取
                    read_len = file_len % 256;

                binchar = binreader.ReadBytes(read_len);

                foreach (byte j in binchar)
                {
                    Mytext += j.ToString("X2");
                    Mytext += " ";
                }

                file_len -= read_len;
             }
            textBox1.Text = Mytext;
            binreader.Close();
        }

        private void openfilebutton_Click(object sender, EventArgs e)
        {
            string Mytext = "";
            int file_len = 1024;
            int read_len;
            Stream myStream = null;
            byte[] binchar = new byte[] { };
            BinaryReader binreader;

            OpenFileDialog file1 = new OpenFileDialog();
            file1.Filter = "bin|*.bin";

            if(file1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = file1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            while (file_len > 0)
                            {
                                if (file_len / 256 > 0)//一次读取256字节
                                    read_len = 256;
                                else                   //不足256字节按实际长度读取
                                    read_len = file_len % 256;

                                //binchar = binreader.ReadBytes(read_len);

                                foreach (byte j in binchar)
                                {
                                    Mytext += j.ToString("X2");
                                    Mytext += " ";
                                }

                                file_len -= read_len;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
