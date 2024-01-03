using de2_TranVanLong_0194866;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace thithunew
{
    public partial class qlylopquanly : Form
    {
        quanlycosodulieuDataContext db = new quanlycosodulieuDataContext();
        private string selectedMLOP;
        

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void quanlylopquanly_Load(object sender, EventArgs e)
        {
            var lop = db.tbl_lopquanlies.ToList();
            dataGridView1.DataSource = lop;

            dataGridView1.CellClick += dataGridView1_CellClick;


            //dataGridView1.DataSource = db.tbl_lopquanlies.Select(d=>d);
            //c2 
            /*dataGridView1.DataSource = from u in db.tbl_lopquanlies
                                           //where u.lqlma.CompareTo("2") == 1
                                       orderby u.lqlma descending //descending or ascending laf sapws xeeps nguowcj laij
                                       select u;*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void LoadData()
        {
            var lop = db.tbl_lopquanlies.ToList();
            dataGridView1.DataSource = lop;
        }
        private void btnxoalop_Click(object sender, EventArgs e)
        {
            var selectedClass = db.tbl_lopquanlies.SingleOrDefault(c => c.lqlma == txtlqlma.Text);
            if (selectedClass != null)
            {
                MessageBox.Show("xóa thành công");
                db.tbl_lopquanlies.DeleteOnSubmit(selectedClass);
                db.SubmitChanges();
                LoadData();
            }
        }

        private void btnthemlop_Click(object sender, EventArgs e)
        {
            tbl_lopquanly newLop = new tbl_lopquanly();
            newLop.lqlma = txtlqlma.Text;
            newLop.lqlten = txtlqlten.Text;
            newLop.lqlkhoahoc = int.Parse(txtlqlkhoahoc.Text);
            db.tbl_lopquanlies.InsertOnSubmit(newLop);
            db.SubmitChanges();
            MessageBox.Show("Thêm thành công!");
            LoadData();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn một ô (cell) hay không
            if (e.RowIndex >= 0)
            {
                selectedMLOP = dataGridView1.Rows[e.RowIndex].Cells["lqlma"].Value.ToString();

                string maLOP = dataGridView1.Rows[e.RowIndex].Cells["lqlma"].Value?.ToString() ?? "";
                string tenLOP = dataGridView1.Rows[e.RowIndex].Cells["lqlten"].Value?.ToString() ?? "";

                // Kiểm tra xem cột "lqlkhoahoc" có tồn tại không
                int khoaHocColumnIndex = dataGridView1.Columns["lqlkhoahoc"]?.Index ?? -1;

                if (khoaHocColumnIndex != -1 && e.RowIndex < dataGridView1.Rows.Count)
                {
                    object khoaHocValue = dataGridView1.Rows[e.RowIndex].Cells[khoaHocColumnIndex].Value;

                    if (khoaHocValue != null)
                    {
                        // Giữ giá trị thực tế từ dữ liệu
                        txtlqlkhoahoc.Text = khoaHocValue.ToString();
                    }
                    else
                    {
                        // Xử lý khi giá trị là null (thực hiện xử lý tùy thuộc vào yêu cầu của bạn)
                        txtlqlkhoahoc.Text = "";
                    }
                }
                else
                {
                    // Xử lý khi không tìm thấy cột "lqlkhoahoc"
                    txtlqlkhoahoc.Text = ""; // hoặc thực hiện xử lý khác tùy thuộc vào yêu cầu của bạn
                }

                // Gán giá trị cho các control khác
                txtlqlma.Text = maLOP;
                txtlqlten.Text = tenLOP;
            }


        }

        private void btnsualop_Click(object sender, EventArgs e)
        {
            var selectedClass = db.tbl_lopquanlies.SingleOrDefault(c => c.lqlma == txtlqlma.Text);
            if (selectedClass != null)
            {
                selectedClass.lqlma = txtlqlma.Text;
                selectedClass.lqlten = txtlqlten.Text;
                selectedClass.lqlkhoahoc = int.Parse(txtlqlkhoahoc.Text);
                db.SubmitChanges();
                MessageBox.Show("sửa thành công!");
                LoadData();
            }
        }

        private void txttimlop_TextChanged(object sender, EventArgs e)
        {
            string searchText = txttimlop.Text.Trim();
            var searchResults = from c in db.tbl_lopquanlies
                                where c.lqlma.Contains(searchText) || c.lqlten.Contains(searchText)
                                select c;
            dataGridView1.DataSource = searchResults;
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            txtlqlma.Clear();
            txtlqlten.Clear();
            txtlqlkhoahoc.Clear();
        }

        private void txttimlop_TextChanged_1(object sender, EventArgs e)
        {
            TimKiemLop();
        }
        private void TimKiemLop()
        {
            string tenLopCanTim = txttimlop.Text.Trim();

            if (!string.IsNullOrEmpty(tenLopCanTim))
            {
                // Thực hiện truy vấn tìm kiếm lop theo tên
                var lopList = db.tbl_lopquanlis.Where(lop => lop.lqlten.Contains(tenLopCanTim) || lop.lqlma.Contains(tenLopCanTim)).ToList();

                if (lopList.Any())
                {
                    // Hiển thị kết quả trong DataGridView
                    dataGridView1.DataSource = lopList;
                }
                /*else
                {
                    MessageBox.Show("Không tìm thấy sinh viên có tên này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }*/
            }
            else
            {
                // Nếu không có ký tự nào, hiển thị toàn bộ danh sách
                var allLopList = db.tbl_lopquanlis.ToList();
                dataGridView1.DataSource = allLopList;
            }
        }
    }
}
