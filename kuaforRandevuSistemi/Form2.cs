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
    public partial class Form2 : Form
    {
        SqlConnection con = new SqlConnection("Server = .; Database = kuaforRandevu; Integrated Security = True");
        SqlCommand com1;
        string tel,
               isim,
               cinsiyet,
               sifre;

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
        public Form2()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, 299, Height, 20, 20));
            
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

        private void Form2_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

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
            //KAYIT OL

            {

                if (textBox2.Text.Length != 10)
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
                else
                {

                    if (Int32.TryParse(textBox2.Text, out int value))
                    {
                        tel = textBox2.Text;
                        label9.Visible = false;
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

            } //telefon numarası kontrolleri

            if(textBox3.Text.Contains("1") || textBox3.Text.Contains("2") || textBox3.Text.Contains("3") || textBox3.Text.Contains("4") || textBox3.Text.Contains("5") || textBox3.Text.Contains("6") || textBox3.Text.Contains("7") || textBox3.Text.Contains("8") || textBox3.Text.Contains("9") || textBox3.Text.Contains("0"))
            {
                if (label10.Visible == false)
                {
                    label10.Visible = true;
                }
                else
                {
                    label10.Left -= 3;
                    wait(26);
                    label10.Left += 3;
                }
            }
            else
            {
                isim = textBox3.Text;
                label10.Visible = false;
            }//isim kontrolleri

            if (cinsiyet == null)
            {
                if (label11.Visible == false)
                {
                    label11.Visible = true;
                }
                else
                {
                    label11.Left -= 3;
                    wait(26);
                    label11.Left += 3;
                }
            }
            else
            {
                label11.Visible = false;
            }//cinsiyet kontrolleri

            if (textBox1.Text.Length < 4)
            {
                if (label12.Visible == false)
                {
                    label12.Visible = true;
                }
                else
                {
                    label12.Left -= 3;
                    wait(26);
                    label12.Left += 3;
                }
            }
            else
            {
                sifre = textBox1.Text;
                label12.Visible = false;
            }//şifre kontrolleri

            if (!label9.Visible && !label10.Visible && !label11.Visible && !label12.Visible) 
            {
                com1 = new SqlCommand("INSERT INTO accs (tel, isim, cinsiyet, sifre) VALUES (@tel, @isim, @cinsiyet, @sifre)", con);
                com1.Parameters.AddWithValue("@tel", tel);
                com1.Parameters.AddWithValue("@isim", isim);
                com1.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                com1.Parameters.AddWithValue("@sifre", sifre);

                if (con.State == ConnectionState.Closed)
                    con.Open();

                com1.ExecuteNonQuery();

                if (con.State == ConnectionState.Open)
                    con.Close();

                this.Hide();
                Form1 f1 = new Form1();
                f1.StartPosition = FormStartPosition.Manual;
                f1.Location = new Point(this.Left, this.Top);
                f1.Show();
            } //database kayıt işlemleri

        }

        private void label5_Click_1(object sender, EventArgs e)
        {
            //form1'e geçiş (giriş yap)
            this.Hide();
            Form1 f1 = new Form1();
            f1.StartPosition = FormStartPosition.Manual;
            f1.Location = new Point(this.Left, this.Top);
            f1.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            //radioButtonErkek --> 11 tikli hali

            pictureBox12.Visible = false;
            pictureBox11.Visible = true;
            cinsiyet = "Erkek";

        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            //radioButtonKadın --> 12 tikli hali

            pictureBox11.Visible = false;
            pictureBox12.Visible = true;
            cinsiyet = "Kadın";

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

    }


}
