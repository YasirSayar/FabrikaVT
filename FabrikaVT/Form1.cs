using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FabrikaVT
{
    public partial class Form1 : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");

        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = buttonGiris; /*Enter'a basınca Giriş tuşuna basma eventi gönderen kısım*/
            labelHosgeldiniz.TextAlign = ContentAlignment.MiddleCenter;
            pictureBox1.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"\\ekDosya\\Logo.png");
            this.BackgroundImage = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\ekDosya\\Form1Backgroundİmage.jpeg");
            panel1.BackColor = Color.FromArgb(190,Color.Black);
            labelKullanıcıAdı.BackColor = Color.FromArgb(0, Color.White);labelKullanıcıAdı.ForeColor = Color.White;
            labelSifre.BackColor = Color.FromArgb(0, Color.White); labelSifre.ForeColor = Color.White;
            checkBoxSifreyiGoster.BackColor= Color.FromArgb(0, Color.White); checkBoxSifreyiGoster.ForeColor = Color.White;

          
        }

        private void buttonGiris_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConn = baglantiObj.CreateConnection();
            string queryGiris = "SELECT f_name, l_name, 'Staff' AS TabloAdi, staff_id FROM Staff WHERE username=@username AND password=@password " +
                     "UNION ALL " +
                     "SELECT employee_name as f_name, employee_lastname as l_name, 'Employee' AS TabloAdi, employee_id FROM Employee WHERE emp_username=@emp_username AND emp_password=@emp_password";

            sqlConn.Open();
            using (SqlCommand command = new SqlCommand(queryGiris, sqlConn))
            {
                try
                {
                    // Parametreleri ekleyerek SQL sorgusunu güvenli hale getir
                    command.Parameters.AddWithValue("@username", textBoxUserName.Text);
                    command.Parameters.AddWithValue("@password", textBoxPassword.Text);
                    command.Parameters.AddWithValue("@emp_username", textBoxUserName.Text);
                    command.Parameters.AddWithValue("@emp_password", textBoxPassword.Text);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string fName = reader["f_name"].ToString();
                            string lName = reader["l_name"].ToString();
                            string tabloAdi = reader["TabloAdi"].ToString();
                            //SetStaffId(Convert.ToInt32(reader["staff_id"]));
                            // Label'lara atama
                           /* labelUsername.Text = "Username: " + fName;
                            labelFullName.Text = "Full Name: " + lName;
                            buttonGiris.Text = tabloAdi;*/


                            if(tabloAdi=="Staff")
                            {
                                this.Hide();
                                FormStaff formstaff = new FormStaff();
                                formstaff.Text = fName+" "+lName+"(Staff) ";
                                formstaff.SetStaffId(Convert.ToInt32(reader["staff_id"]));
                                formstaff.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                this.Hide();
                                FormEmployee formEmployee = new FormEmployee();
                                formEmployee.Text = fName + " " + lName + "(Employee) ";
                                formEmployee.SetCustomerId(Convert.ToInt32(reader["staff_id"]));
                                formEmployee.ShowDialog();
                                this.Close();
                            }
                        }
                        else
                        {
                            // Eğer eşleşen bir kayıt yoksa hata mesajını ayarla
                           
                            /* labelUsername.Text = "Invalid credentials";
                            labelFullName.Text = "";*/
                            if (textBoxPassword.Text=="" || textBoxUserName.Text=="")
                                MessageBox.Show("Kullanıcı adı ve şifre girmediniz ");
                            else
                            MessageBox.Show("Yanlış Kullanıcı adı veya şifre ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata durumunda işlemler
                    MessageBox.Show("Hata: " + ex.Message);
                }
                finally
                {
                    sqlConn.Close();
                }

            }
        }
        private int staffId;
        public void SetStaffId(int id)
        {
            staffId = id;
        }
        private void labelHosgeldiniz_Click(object sender, EventArgs e)
        {
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (checkBoxSifreyiGoster.Checked.ToString() != "True")
                textBoxPassword.PasswordChar = '*';
        }

        private void checkBoxSifreyiGoster_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxSifreyiGoster.Checked == true)
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
                textBoxPassword.PasswordChar = '*';

        }
    }
}
