using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SnowGL
{
    public partial class Form1 : Form
    {
        NotifyIcon ni1;
        public Form1()
        {
            InitializeComponent();
            ni1 = new NotifyIcon();
            try
            {
                ni1.Icon = new Icon("Snow.ico");
            }
            catch { }
            ni1.BalloonTipText = "Я тут!!!";

            
            ni1.Click += ni1_Click;
           // ni1.Click += ;
            len = 1024;
            ni1.Visible = true;
            ni1.ShowBalloonTip(1000);
            this.Visible = false;
            SharpGLForm f = new SharpGLForm(len);
            f.Show();
        }

        void ni1_Click(object sender, EventArgs e)
        {
            Close();
        }
        int len;
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            len = (int)Math.Pow(2, trackBar1.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ni1.Visible = true;
            ni1.ShowBalloonTip(1000);
            this.Visible = false;
            SharpGLForm f = new SharpGLForm(len);
            f.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Visible = false;
        }
    }
}
