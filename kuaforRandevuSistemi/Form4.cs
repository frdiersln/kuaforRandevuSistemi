using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace kuaforRandevuSistemi
{
    public partial class Form4 : Form
    {

        SqlConnection con = new SqlConnection("Server = .; Database = kuaforRandevu; Integrated Security = True");
        SqlCommand com1;
        SqlDataAdapter da1 = new SqlDataAdapter();
        DataSet ds1 = new DataSet();
        SqlDataAdapter da2 = new SqlDataAdapter();
        DataSet ds2 = new DataSet();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public Form4()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 750, Height, 20, 20));
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            randevularLoad();
            accsLoad();
        }

        private void randevularLoad()
        {
            string sorgu2 = "SELECT * FROM randevular";
            da1 = new SqlDataAdapter(sorgu2, con);


            if (con.State == ConnectionState.Closed)
                con.Open();

            ds1.Clear();
            da1.Fill(ds1);

            if (con.State == ConnectionState.Open)
                con.Close();

            dataGridView1.DataSource = ds1.Tables[0];
        }

        private void accsLoad()
        {
            string sorgu2 = "SELECT * FROM accs";
            da2 = new SqlDataAdapter(sorgu2, con);


            if (con.State == ConnectionState.Closed)
                con.Open();

            ds2.Clear();
            da2.Fill(ds2);

            if (con.State == ConnectionState.Open)
                con.Close();

            dataGridView2.DataSource = ds2.Tables[0];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string secilmisTarih;
                secilmisTarih = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                string sorgu = "DELETE FROM randevular WHERE Tarih='" + secilmisTarih + "'";
                com1 = new SqlCommand(sorgu, con);


                if (con.State == ConnectionState.Closed)
                    con.Open();

                com1.ExecuteNonQuery();

                if (con.State == ConnectionState.Open)
                    con.Close();

                randevularLoad();

            }
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                string telNo;
                telNo = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();

                string sorgu = "DELETE FROM accs WHERE tel='" + telNo + "'";
                com1 = new SqlCommand(sorgu, con);


                if (con.State == ConnectionState.Closed)
                    con.Open();

                com1.ExecuteNonQuery();

                if (con.State == ConnectionState.Open)
                    con.Close();

                accsLoad();

            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            string tel = textBox2.Text;
            string sifre = textBox1.Text;

            com1 = new SqlCommand("INSERT INTO accs (tel, sifre, admin) VALUES (@tel, @sifre, '1')", con);
            com1.Parameters.AddWithValue("@tel", tel);
            com1.Parameters.AddWithValue("@sifre", sifre);

            if (con.State == ConnectionState.Closed)
                con.Open();

            com1.ExecuteNonQuery();

            if (con.State == ConnectionState.Open)
                con.Close();

            accsLoad();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //form1'e geçiş (giriş yap)
            this.Hide();
            Form1 f1 = new Form1();
            f1.StartPosition = FormStartPosition.Manual;
            f1.Location = new Point(this.Left, this.Top);
            f1.Show();
        }
    }
}
