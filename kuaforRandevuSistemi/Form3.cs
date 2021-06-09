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
    public partial class Form3 : Form
    {

        SqlConnection con = new SqlConnection("Server = .; Database = kuaforRandevu; Integrated Security = True");
        SqlCommand com1;
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();

        string isim, cinsiyet;
        string sacTipi, kesim, boya, yıkama, sakal, aromaTerapi, yuzAgda, vucutAgda, manikur, pedikur, kas;
        string tarih, saat;

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
        public Form3()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 750, Height, 20, 20));

        }

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            pictureBox7.Visible = false;
            pictureBox26.Visible = false;
            pictureBox27.Visible = true;
            pictureBox6.Visible = true;
            sacTipi = "kısa";
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            pictureBox7.Visible = true;
            pictureBox26.Visible = true;
            pictureBox27.Visible = false;
            pictureBox6.Visible = false;
            sacTipi = "uzun";
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            if (pictureBox7.Visible || pictureBox6.Visible)
            {
                pictureBox9.Visible = true;
                pictureBox28.Visible = false;
                kesim = "1";
            }
            else
            {
                if (label9.Visible == false)
                {
                    label9.Visible = true;
                }
                else
                {
                    label9.Left -= 3;
                    wait(26);
                    label9.Left += 3;
                }
            }
            
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            if (pictureBox7.Visible || pictureBox6.Visible)
            {
                pictureBox10.Visible = true;
                pictureBox29.Visible = false;
                boya = "1";
            }
            else
            {
                if (label9.Visible == false)
                {
                    label9.Visible = true;
                }
                else
                {
                    label9.Left -= 3;
                    wait(26);
                    label9.Left += 3;
                }
            }
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            if (pictureBox7.Visible || pictureBox6.Visible)
            {
                pictureBox11.Visible = true;
                pictureBox30.Visible = false;
                yıkama = "1";
            }
            else
            {
                if (label9.Visible == false)
                {
                    label9.Visible = true;
                }
                else
                {
                    label9.Left -= 3;
                    wait(26);
                    label9.Left += 3;
                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            pictureBox29.Visible = true;
            pictureBox10.Visible = false;
            boya = "0";
            
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            pictureBox30.Visible = true;
            pictureBox11.Visible = false;
            yıkama = "0";
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            pictureBox12.Visible = false;
            pictureBox31.Visible = true;
            sakal = "0";
        }

        private void pictureBox32_Click(object sender, EventArgs e)
        {
            pictureBox13.Visible = true;
            pictureBox32.Visible = false;
            aromaTerapi = "1";
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            pictureBox13.Visible = false;
            pictureBox32.Visible = true;
            aromaTerapi = "0";
        }

        private void pictureBox33_Click(object sender, EventArgs e)
        {
            pictureBox15.Visible = true;
            pictureBox33.Visible = false;
            yuzAgda = "1";
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            pictureBox15.Visible = false;
            pictureBox33.Visible = true;
            yuzAgda = "0";
        }

        private void pictureBox34_Click(object sender, EventArgs e)
        {
            pictureBox14.Visible = true;
            pictureBox34.Visible = false;
            vucutAgda = "1";
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            pictureBox14.Visible = false;
            pictureBox34.Visible = true;
            vucutAgda = "0";
        }

        private void pictureBox35_Click(object sender, EventArgs e)
        {
            pictureBox18.Visible = true;
            pictureBox35.Visible = false;
            manikur = "1";
        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {
            pictureBox18.Visible = false;
            pictureBox35.Visible = true;
            manikur = "0";
        }

        private void pictureBox36_Click(object sender, EventArgs e)
        {
            pictureBox17.Visible = true;
            pictureBox36.Visible = false;
            pedikur = "1";
        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {
            pictureBox17.Visible = false;
            pictureBox36.Visible = true;
            pedikur = "0";
        }

        private void pictureBox37_Click(object sender, EventArgs e)
        {
            pictureBox16.Visible = true;
            pictureBox37.Visible = false;
            kas = "1";
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            pictureBox16.Visible = false;
            pictureBox37.Visible = true;
            kas = "0";
        }

        private void pictureBox23_Click(object sender, EventArgs e)
        {
            saat = "11:00";

            int x = pictureBox23.Location.X;
            int y = pictureBox23.Location.Y - 2;
            pictureBox19.Location = new Point(x, y);
            pictureBox19.Visible = true;
        }

        private void pictureBox24_Click(object sender, EventArgs e)
        {
            saat = "13:00";

            int x = pictureBox24.Location.X;
            int y = pictureBox24.Location.Y - 2;
            pictureBox19.Location = new Point(x, y);
            pictureBox19.Visible = true;
        }

        private void pictureBox21_Click(object sender, EventArgs e)
        {
            saat = "15:00";

            int x = pictureBox21.Location.X;
            int y = pictureBox21.Location.Y - 2;
            pictureBox19.Location = new Point(x, y);
            pictureBox19.Visible = true;
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            saat = "17:00";

            int x = pictureBox22.Location.X;
            int y = pictureBox22.Location.Y - 2;
            pictureBox19.Location = new Point(x, y);
            pictureBox19.Visible = true;
        }

        private void pictureBox38_Click(object sender, EventArgs e)
        {
            saat = "19:00";

            int x = pictureBox38.Location.X;
            int y = pictureBox38.Location.Y - 2;
            pictureBox19.Location = new Point(x, y);
            pictureBox19.Visible = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            saat = "09:00";
            
            int x = pictureBox20.Location.X;
            int y = pictureBox20.Location.Y - 2;
            pictureBox19.Location = new Point(x, y);
            pictureBox19.Visible = true;
            
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string telNo = label1.Text;

                string secilmisTarih;
                secilmisTarih = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                string sorgu = "DELETE FROM randevular WHERE TelefonNumarası='" + telNo + "' AND Tarih='" + secilmisTarih + "'";
                com1 = new SqlCommand(sorgu, con);


                if (con.State == ConnectionState.Closed)
                    con.Open();

                com1.ExecuteNonQuery();

                if (con.State == ConnectionState.Open)
                    con.Close();

                listele();

                dateTimePicker1_ValueChanged(sender, e);
            }
            
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (kesim == "1" || boya == "1" || yıkama == "1" || sakal == "1" || aromaTerapi == "1" || yuzAgda == "1" || vucutAgda == "1" || manikur == "1" || pedikur == "1" || kas == "1")
            {
                if (pictureBox19.Visible)
                {
                    string tarih1 = tarih + " " + saat;
                    string telNo = label1.Text; 
                    string sorgu = "INSERT randevular VALUES('" + tarih1 + "', '" + telNo + "', '" + isim + "', '" + cinsiyet + "', '" + sacTipi + "', '" + kesim + "', '" + boya + "', '" + yıkama + "', '" + sakal + "', '" + aromaTerapi + "', '" + yuzAgda + "', '" + vucutAgda + "', '" + manikur + "', '" + pedikur + "', '" + kas + "')";
                    com1 = new SqlCommand(sorgu, con);


                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    com1.ExecuteNonQuery();

                    if (con.State == ConnectionState.Open)
                        con.Close();

                    listele();

                    label7.Visible = false;
                    label5.Visible = false;
                    pictureBox19.Visible = false;
                }
                else
                {
                    label7.Visible = true;
                }
            }
            else
            {
                label5.Visible = true;
            }
        }

        private void pictureBox31_Click(object sender, EventArgs e)
        {
            pictureBox12.Visible = true;
            pictureBox31.Visible = false;
            sakal = "0";
        }

        private void pictureBox39_Click(object sender, EventArgs e)
        {
            //form1'e geçiş (giriş yap)
            this.Hide();
            Form1 f1 = new Form1();
            f1.StartPosition = FormStartPosition.Manual;
            f1.Location = new Point(this.Left, this.Top);
            f1.Show();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox19.Visible = false;

            saatKontrol();

            tarih = dateTimePicker1.Value.ToString().Split(" ")[0];

        }

        private void saatKontrol()
        {
            string secilenTarih0 = dateTimePicker1.Value.Date.ToString();
            string secilenTarih = secilenTarih0.Remove(secilenTarih0.Length - 9);
            List<string> doluSaatler = new List<string>();
            foreach (var i in doluSaatler)
            {
                doluSaatler.Remove(i);
            }

            string sorgu = "SELECT Tarih FROM randevular WHERE Tarih LIKE '" + secilenTarih + "%'";
            com1 = new SqlCommand(sorgu, con);

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataReader oku = com1.ExecuteReader();

            while (oku.Read())
            {

                doluSaatler.Add(oku[0].ToString().Split(" ")[1]);

            }

            if (con.State == ConnectionState.Open)
                con.Close();

            if (doluSaatler.Contains("09:00"))
            {
                pictureBox20.Visible = false;
                blok9.Visible = true;
            }
            else
            {
                pictureBox20.Visible = true;
                blok9.Visible = false;
            }

            if (doluSaatler.Contains("11:00"))
            {
                pictureBox23.Visible = false;
                blok11.Visible = true;
            }
            else
            {
                pictureBox23.Visible = true;
                blok11.Visible = false;
            }

            if (doluSaatler.Contains("13:00"))
            {
                pictureBox24.Visible = false;
                blok13.Visible = true;
            }
            else
            {
                pictureBox24.Visible = true;
                blok13.Visible = false;
            }

            if (doluSaatler.Contains("15:00"))
            {
                pictureBox21.Visible = false;
                blok15.Visible = true;
            }
            else
            {
                pictureBox21.Visible = true;
                blok15.Visible = false;
            }

            if (doluSaatler.Contains("17:00"))
            {
                pictureBox22.Visible = false;
                blok17.Visible = true;
            }
            else
            {
                pictureBox22.Visible = true;
                blok17.Visible = false;
            }

            if (doluSaatler.Contains("19:00"))
            {
                pictureBox38.Visible = false;
                blok19.Visible = true;
            }
            else
            {
                pictureBox38.Visible = true;
                blok19.Visible = false;
            }

        }

        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void Form3_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            DateTime now = DateTime.Now;
            dateTimePicker1.Value = now;
            dateTimePicker1.MinDate = now;
            dateTimePicker1.MaxDate = now.AddDays(30);
            dateTimePicker1_ValueChanged(sender, e);

            string telNo = label1.Text;

            string sorgu = "SELECT * FROM accs WHERE tel=@telefon";
            com1 = new SqlCommand(sorgu, con);
            com1.Parameters.AddWithValue("@telefon", telNo);

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataReader kontrol = com1.ExecuteReader();

            while (kontrol.Read())
            {

                isim = kontrol[1].ToString();
                cinsiyet = kontrol[2].ToString();

            }
            
            if (con.State == ConnectionState.Open)
                con.Close();

            label4.Text = isim.Split(" ")[0];

            if (cinsiyet == "Erkek")
            {
                label6.Text = "Bey";
                sakalBlok.Visible = false;
                

            }
            else
            {
                label6.Text = "Hanım";
                sakalBlok.Visible = true;
            }

            listele();

        }

        private void listele()
        {
            string telNo = label1.Text;

            if (cinsiyet == "Erkek")
            {

                string sorgu2 = "SELECT Tarih, SaçTipi, Kesim, Boya, Yıkama, Sakal, AromaTerapi, YüzAğda, VücutAğda, Manikür, Pedikür, Kaş FROM randevular WHERE TelefonNumarası='" + telNo + "'";
                da = new SqlDataAdapter(sorgu2, con);


                if (con.State == ConnectionState.Closed)
                    con.Open();

                ds.Clear();
                da.Fill(ds);

                if (con.State == ConnectionState.Open)
                    con.Close();

                dataGridView1.DataSource = ds.Tables[0];

            }
            else
            {

                string sorgu2 = "SELECT Tarih, SaçTipi, Kesim, Boya, Yıkama, AromaTerapi, YüzAğda, VücutAğda, Manikür, Pedikür, Kaş FROM randevular WHERE TelefonNumarası='" + telNo + "'";
                da = new SqlDataAdapter(sorgu2, con);
                

                if (con.State == ConnectionState.Closed)
                    con.Open();

                ds.Clear();
                da.Fill(ds); 

                if (con.State == ConnectionState.Open)
                    con.Close();

                dataGridView1.DataSource = ds.Tables[0]; 

            }

            saatKontrol();
        }

        public string _telNo
        {
            set { label1.Text = value; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pictureBox7.Visible = true;
            pictureBox26.Visible = true;
            pictureBox27.Visible = false;
            pictureBox6.Visible = false;
            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            
            pictureBox7.Visible = false;
            pictureBox26.Visible = false;
            pictureBox27.Visible = true;
            pictureBox6.Visible = true;
            
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
                pictureBox28.Visible = true;
                pictureBox9.Visible = false;
                kesim = "0";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

    } 
}