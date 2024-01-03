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
    public partial class quanlysinhvien : Form
    {
        quanlycosodulieuDataContext db = new quanlycosodulieuDataContext();
        private string selectedMSSV;
        public quanlysinhvien()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnthemsv_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ các controls
                string maSV = txtsvma.Text.Trim();
                string tenSV = txtsvten.Text.Trim();
                DateTime ngaySinh = dtpngaysinh.Value;
                string gioiTinh = cbxgioitinh.SelectedItem.ToString();
                string queQuan = txtquequan.Text.Trim();
                string lqlma = txtlqlma.Text.Trim();

                // Chuyển giới tính thành số tương ứng
                byte? gioiTinhValue = (byte?)(gioiTinh == "Nam" ? 1 : 0);


                // Kiểm tra dữ liệu hợp lệ trước khi thêm
                if (string.IsNullOrEmpty(maSV) || string.IsNullOrEmpty(tenSV) || string.IsNullOrEmpty(queQuan) || string.IsNullOrEmpty(lqlma))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra xem sinh viên đã tồn tại chưa
                var existingSV = db.tbl_sinhviens.FirstOrDefault(sv => sv.svma == maSV);
                if (existingSV != null)
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng chọn mã sinh viên khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo đối tượng sinh viên mới
                tbl_sinhvien newSV = new tbl_sinhvien
                {
                    svma = maSV,
                    svten = tenSV,
                    svngaysinh = ngaySinh,
                    svgioitinh = gioiTinhValue,
                    svquequan = queQuan,
                    lqlma = lqlma
                };

                // Thêm sinh viên vào cơ sở dữ liệu
                db.tbl_sinhviens.InsertOnSubmit(newSV);
                db.SubmitChanges();

                // Hiển thị thông báo thành công
                MessageBox.Show("Thêm sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật DataGridView
                var svList = db.tbl_sinhviens.ToList();
                dataGridView1.DataSource = svList;

                // Xóa dữ liệu từ các controls sau khi thêm thành công
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputFields()
        {
            // Xóa dữ liệu từ các controls
            txtsvma.Clear();
            txtsvten.Clear();
            dtpngaysinh.Value = DateTime.Now;
            cbxgioitinh.SelectedIndex = 0; // Chọn giới tính Nam mặc định
            txtquequan.Clear();
            txtlqlma.Clear();
        }

        private void quanlysinhvien_Load(object sender, EventArgs e)
        {
            var sv = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = sv;

            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedMSSV = dataGridView1.Rows[e.RowIndex].Cells["svten"].Value.ToString();

                string maSV = dataGridView1.Rows[e.RowIndex].Cells["svma"].Value?.ToString() ?? "";
                string tenSV = dataGridView1.Rows[e.RowIndex].Cells["svten"].Value?.ToString() ?? "";
                string queQuan = dataGridView1.Rows[e.RowIndex].Cells["svquequan"].Value?.ToString() ?? "";
                string lqlma = dataGridView1.Rows[e.RowIndex].Cells["lqlma"].Value?.ToString() ?? "";
                int gioiTinhValue = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["svgioitinh"].Value);

                DateTime ngaySinh;
                if (DateTime.TryParse(dataGridView1.Rows[e.RowIndex].Cells["svngaysinh"].Value?.ToString(), out ngaySinh) && ngaySinh != DateTime.MinValue)
                {
                    dtpngaysinh.Value = ngaySinh;
                }
                else
                {
                    // Nếu không có thông tin ngày sinh, đặt giá trị mặc định cho DateTimePicker
                    dtpngaysinh.Value = DateTime.Now; // Hoặc bạn có thể để trống nếu muốn
                }

                // Gán giá trị cho các control, giữ trống nếu không có thông tin
                txtsvma.Text = maSV;
                txtsvten.Text = tenSV;
                txtquequan.Text = queQuan;
                txtlqlma.Text = lqlma;
                cbxgioitinh.SelectedIndex = (gioiTinhValue == 1) ? 0 : 1;
            }

        }
        private void LoadData()
        {
            var sv = db.tbl_sinhviens.ToList();
            dataGridView1.DataSource = sv;
        }

        private void btnxoasv_Click(object sender, EventArgs e)
        {
            var selectedClass = db.tbl_sinhviens.SingleOrDefault(c => c.svma == txtsvma.Text);
            if (selectedClass != null)
            {
                MessageBox.Show("xóa thành công");
                db.tbl_sinhviens.DeleteOnSubmit(selectedClass);
                db.SubmitChanges();
                LoadData();
            }

        }

        

        private void btnsuasv_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy giá trị từ các controls
                string maSV = txtsvma.Text.Trim();
                string tenSV = txtsvten.Text.Trim();
                DateTime ngaySinh = dtpngaysinh.Value;
                string gioiTinh = cbxgioitinh.SelectedItem?.ToString() ?? "";
                string queQuan = txtquequan.Text.Trim();
                string lqlma = txtlqlma.Text.Trim();

                // Kiểm tra xem sinh viên có tồn tại hay không
                var existingSV = db.tbl_sinhviens.FirstOrDefault(sv => sv.svma == maSV);
                if (existingSV == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên có mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Cập nhật thông tin sinh viên
                existingSV.svten = tenSV;
                existingSV.svquequan = queQuan;
                existingSV.lqlma = lqlma;

                // Nếu ngày sinh có giá trị thì cập nhật, ngược lại giữ nguyên giá trị hiện tại
                if (ngaySinh != DateTime.MinValue)
                {
                    existingSV.svngaysinh = ngaySinh;
                }

                // Nếu giới tính có giá trị thì cập nhật, ngược lại giữ nguyên giá trị hiện tại
                if (!string.IsNullOrEmpty(gioiTinh))
                {
                    byte gioiTinhValue;
                    if (gioiTinh.ToLower() == "nam")
                    {
                        gioiTinhValue = 1;
                    }
                    else if (gioiTinh.ToLower() == "nữ")
                    {
                        gioiTinhValue = 0;
                    }
                    else
                    {
                        // Handle the case where gioiTinh is neither "Nam" nor "Nữ"
                        MessageBox.Show("Giới tính không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Cập nhật giới tính trong cơ sở dữ liệu
                    existingSV.svgioitinh = gioiTinhValue;
                }

                // Thực hiện cập nhật trong cơ sở dữ liệu
                db.SubmitChanges();

                // Hiển thị thông báo thành công
                MessageBox.Show("Cập nhật thông tin sinh viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật DataGridView
                var svList = db.tbl_sinhviens.ToList();
                dataGridView1.DataSource = svList;

                // Xóa dữ liệu từ các controls sau khi cập nhật thành công
                ClearSuaInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void ClearSuaInputFields()
        {
            // Xóa dữ liệu từ các controls
            txtsvma.Clear();
            txtsvten.Clear();
            dtpngaysinh.Value = DateTime.Now;
            cbxgioitinh.SelectedIndex = 0; // Chọn giới tính Nam mặc định
            txtquequan.Clear();
            txtlqlma.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtsvma.Clear();
            txtsvten.Clear();
            txtquequan.Clear();
            txtlqlma.Clear();
        }

        private void txttimsv_TextChanged(object sender, EventArgs e)
        {
            TimKiemSinhVien();
        }
        private void TimKiemSinhVien()
        {
            string tenSVCanTim = txttimsv.Text.Trim();

            if (!string.IsNullOrEmpty(tenSVCanTim))
            {
                // Thực hiện truy vấn tìm kiếm sinh viên theo tên
                var svList = db.tbl_sinhviens.Where(sv => sv.svten.Contains(tenSVCanTim) || sv.svma.Contains(tenSVCanTim)).ToList();

                if (svList.Any())
                {
                    // Hiển thị kết quả trong DataGridView
                    dataGridView1.DataSource = svList;
                }
                /*else
                {
                    MessageBox.Show("Không tìm thấy sinh viên có tên này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
            }
            else
            {
                // Nếu không có ký tự nào, hiển thị toàn bộ danh sách
                var allSvList = db.tbl_sinhviens.ToList();
                dataGridView1.DataSource = allSvList;
            }
        }
    }
}
