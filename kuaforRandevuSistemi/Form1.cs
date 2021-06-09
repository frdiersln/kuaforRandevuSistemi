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

// kayıtlı yönetici hesabı -> telefon = 5444263566, şifre = 4141
//kayıtlı müşteri hesabı telefon = 1234567890, şifre = 4321


namespace kuaforRandevuSistemi
{
    public partial class Form1 : Form
    {

        SqlConnection con = new SqlConnection("Server = .; Database = kuaforRandevu; Integrated Security = True");
        SqlCommand com1;
        SqlCommand com2;

        string tel,isim,cinsiyet,sifre,admin,girilenTel, girilenSifre;

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
        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 299, Height, 20, 20));
            
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
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            temizlik();
        }

        private void temizlik()
        {
            string sorgu = "SELECT Tarih FROM randevular";
            com1 = new SqlCommand(sorgu, con);

            if (con.State == ConnectionState.Closed)
                con.Open();

            SqlDataReader oku1 = com1.ExecuteReader();
            List<string> tamTarihler = new List<string>();

            while (oku1.Read())
            {

                tamTarihler.Add(oku1[0].ToString());
                
            }

            if (con.State == ConnectionState.Open)
                con.Close();

            if (con.State == ConnectionState.Closed)
                con.Open();

            foreach (var tamTarih in tamTarihler)
            {
                int yıl = int.Parse(tamTarih.Split(" ")[0].Split(".")[2]);
                int ay = int.Parse(tamTarih.Split(" ")[0].Split(".")[1]);
                int gun = int.Parse(tamTarih.Split(" ")[0].Split(".")[0]);
                int saat = int.Parse(tamTarih.Split(" ")[1].Split(":")[0]);

                if (yıl < DateTime.Now.Year)
                {
                    string sorgu1 = "DELETE FROM randevular WHERE Tarih='" + tamTarih + "'";
                    com2 = new SqlCommand(sorgu1, con);
                    com2.ExecuteNonQuery();

                }
                else if (yıl == DateTime.Now.Year)
                {
                    if (ay < DateTime.Now.Month)
                    {
                        string sorgu1 = "DELETE FROM randevular WHERE Tarih='" + tamTarih + "'";
                        com2 = new SqlCommand(sorgu1, con);
                        com2.ExecuteNonQuery();
                    }
                    else if (ay == DateTime.Now.Month)
                    {
                        if (gun < DateTime.Now.Day)
                        {
                            string sorgu1 = "DELETE FROM randevular WHERE Tarih='" + tamTarih + "'";
                            com2 = new SqlCommand(sorgu1, con);
                            com2.ExecuteNonQuery();
                        }
                        else if (gun == DateTime.Now.Day)
                        {
                            if (saat < DateTime.Now.Hour)
                            {
                                string sorgu1 = "DELETE FROM randevular WHERE Tarih='" + tamTarih + "'";
                                com2 = new SqlCommand(sorgu1, con);
                                com2.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            if (con.State == ConnectionState.Open)
                con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //eye button
            textBox1.PasswordChar = '\0';
            pictureBox5.Enabled = false;
            pictureBox5.Visible = false;

            pictureBox6.Enabled = true;
            pictureBox6.Visible = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //close eye button
            textBox1.PasswordChar = '*';
            pictureBox6.Enabled = false;
            pictureBox6.Visible = false;

            pictureBox5.Enabled = true;
            pictureBox5.Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
       
            
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            // GİRİŞ YAP BUTONU
            girilenTel = textBox2.Text;
            girilenSifre = textBox1.Text;
            string sorgu = "SELECT * FROM accs WHERE tel=@telefon";
            com1 = new SqlCommand(sorgu, con);
            com1.Parameters.AddWithValue("@telefon", girilenTel);

            if(con.State == ConnectionState.Closed)
            con.Open();

            SqlDataReader kontrol = com1.ExecuteReader();

            while (kontrol.Read())
            {
                tel = kontrol[0].ToString();
                isim = kontrol[1].ToString();
                cinsiyet = kontrol[2].ToString();
                sifre = kontrol[3].ToString();
                admin = kontrol[4].ToString();
            }

            if (girilenTel == tel && girilenSifre == sifre)
            {
                if (admin == "False" || admin == "")
                {
                    //form3'e geçiş yap (kullanıcı ekranı)
                    this.Hide();
                    Form3 f3 = new Form3();
                    f3._telNo = _telNo;
                    f3.StartPosition = FormStartPosition.Manual;
                    f3.Location = new Point(this.Left, this.Top);
                    f3.Show();
                    label6.Visible = false;
                }
                else if(admin == "True")
                {
                    //form4'e geçiş yap (yönetici ekranı)
                    this.Hide();
                    Form4 f4 = new Form4();
                    f4.StartPosition = FormStartPosition.Manual;
                    f4.Location = new Point(this.Left, this.Top);
                    f4.Show();
                    label6.Visible = false;
                }
            }
            else
            {
                label6.Visible = true;
            }


            if (con.State == ConnectionState.Open)
                con.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            //form2'ye geçiş yap (kayıt ol)
            this.Hide();
            Form2 f2 = new Form2();
            f2.StartPosition = FormStartPosition.Manual;
            f2.Location = new Point(this.Left, this.Top);
            f2.Show();
        }

        public string _telNo
        {
            get { return textBox2.Text; }
        }

    }

}
