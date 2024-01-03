using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using thithunew;

namespace de2_TranVanLong_0194866
{
    public partial class TranVanLong_0194866 : Form
    {
        public TranVanLong_0194866()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void quảnLýLớpQuảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qlylopquanly f1= new qlylopquanly();   
            f1.TopLevel = false;
            f1.FormBorderStyle = FormBorderStyle.None;
            f1.Size = panel1.Size;
            panel1.Controls.Clear();
            panel1.Controls.Add(f1);
            f1.Show();
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quanlysinhvien f2 = new quanlysinhvien();
            f2.TopLevel = false;
            f2.FormBorderStyle = FormBorderStyle.None;
            f2.Size = panel1.Size;
            panel1.Controls.Clear();
            panel1.Controls.Add(f2);
            f2.Show();
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            if (panel1.Controls.Count > 0)
            {
                var sub_ctrl = panel1.Controls[0];
                sub_ctrl.Width = panel1.Width;
                sub_ctrl.Height = panel1.Height;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DangNhap == false)
            {
                FormLogin loginForm = new FormLogin();
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    Close();
                    return;
                }
            }
        }

        

        private void btnout_Click_1(object sender, EventArgs e)
        {
            Properties.Settings.Default.DangNhap = false;
            Properties.Settings.Default.Save();
            FormLogin loginForm = new FormLogin();
            if (loginForm.ShowDialog() != DialogResult.OK)
            {
                Close();
                return;
            }
        }
    }
}
