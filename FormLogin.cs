using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace de2_TranVanLong_0194866
{
    public partial class FormLogin : Form
    {
        quanlycosodulieuDataContext db = new quanlycosodulieuDataContext();
        public FormLogin()
        {
            InitializeComponent();
        }
        public string LoggedInUsername { get; private set; }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txttk.Focus();
        }
        
        
        private void btnhuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void btndangnhap_Click_1(object sender, EventArgs e)
        {
            string texttk = txttk.Text;
            string textmk = txtmk.Text;

            if (string.IsNullOrEmpty(texttk) || string.IsNullOrEmpty(textmk))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    using (var context = new quanlycosodulieuDataContext())
                    {
                        // Sử dụng LINQ để kiểm tra sự tồn tại của tài khoản
                        var taiKhoan = context.tbl_taikhoans
                            .FirstOrDefault(tk => tk.tktaikhoan == texttk && tk.tkmatkhau == textmk);

                        if (taiKhoan != null)
                        {
                            // Lưu thông tin đăng nhập vào lớp 
                            LoggedInUsername = texttk;
                            MessageBox.Show("Đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            Properties.Settings.Default.DangNhap = true;
                            Properties.Settings.Default.Save();



                            // Đóng FormLogin (nếu cần)
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Đăng nhập thất bại.", "ERROR", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "WARMING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnhuy_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
