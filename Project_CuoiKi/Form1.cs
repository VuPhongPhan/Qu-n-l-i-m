using Project_CuoiKirovider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CuoiKi
{
    public partial class Form1 : Form
    {
        DataProvider data = new DataProvider();
        OpenFileDialog ofd = new OpenFileDialog();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void load()
        {
            string qr = "select MaMon, TenMon, SoTinChi, DiemThi from tblDSDiem";
            dtgv.DataSource = data.ExecQuery(qr);
        }
        private void resert()
        {
            txtMon.Text = "";
            txtTenMon.Text = "";
            txtTinchi.Text = "";
            txtDiemthi.Text = "";
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var id = dtgv.SelectedCells[0].Value.ToString();
                string qr = "Delete from tblDSDiem where MaMon = '" + id + "'";
                data.ExecQuery(qr);
                load();
                resert();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }

        

        private void btnIn_Click(object sender, EventArgs e)
        {
            Excel excel = new Excel();
            DataTable dt = (DataTable)dtgv.DataSource;
            excel.Export(dt, "Danh sach", "DANH SÁCH");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {

        }

        private void dtgv_Click(object sender, EventArgs e)
        {
            try
            {
                 txtMon.Text = dtgv.CurrentRow.Cells[0].Value.ToString();
                 txtTenMon.Text = dtgv.CurrentRow.Cells[1].Value.ToString();
                 txtTinchi.Text = dtgv.CurrentRow.Cells[2].Value.ToString();
                 txtDiemthi.Text = dtgv.CurrentRow.Cells[3].Value.ToString();
            }
            catch
            {
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (checkMA() == 1)
            {
                if (txtMon.TextLength == 0 || txtTenMon.TextLength == 0 || txtTinchi.TextLength == 0 || txtDiemthi.TextLength == 0)
                {
                    MessageBox.Show("Bạn chưa nhập đủ thông tin", "Thông báo");
                }
                else
                {
                    string qr = "Insert into tblDSDiem  values('" + txtMa.Text +"','" + txtMon.Text + "',N'" + txtTenMon.Text + "'," + txtTinchi.Text + "," + txtDiemthi.Text + ")";
                    dtgv.DataSource = data.ExecQuery(qr);
                    load();
                    resert();
                }
            }


            int checkMA()
            {
                int check = 1;
                for (int i = 0; i < dtgv.RowCount - 1; i++)
                {
                    if (txtMon.Text.Equals(dtgv.Rows[i].Cells[0].Value.ToString().Trim()))
                    {
                        check = 0;
                        MessageBox.Show("Mã hàng này đã có", "Thông báo");
                        break;
                    }
                }
                return check;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string qr = "update tblDSDiem set TenMon = N'" + txtTenMon.Text + "', SoTinChi = " + txtTinchi.Text + ", DiemThi = " + txtDiemthi.Text + " where MaMon = '" + txtMon.Text + "'";
            dtgv.DataSource = data.ExecQuery(qr);
            load();
            resert();
        }

        private void txtSoluong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDiemthi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
        }

        private void txtTinchi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnThongkee_Click(object sender, EventArgs e)
        {
            int tc = 0;
            double tongd = 0;
            double diemtb = 0;
            double td = 0;
            for (int i = 0; i < dtgv.RowCount - 1; i++)
            {
                tc += Convert.ToInt32(dtgv.Rows[i].Cells[2].Value.ToString());
                tongd += Convert.ToDouble(dtgv.Rows[i].Cells[3].Value.ToString());
                txtTongTc.Text = tc.ToString();
                txtTongDiem.Text = tongd.ToString();
                td += Convert.ToInt32(dtgv.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(dtgv.Rows[i].Cells[3].Value.ToString());
                diemtb = (double) td  / tc;
                txtDiemTB.Text = diemtb.ToString();
            }
           
        }
    }
}
