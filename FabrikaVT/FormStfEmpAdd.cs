using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabrikaVT
{
    public partial class FormStfEmpAdd : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public FormStfEmpAdd()
        {
            InitializeComponent();
        }

        private void FormStfEmpAdd_Load(object sender, EventArgs e)
        {
            // Bağlantı nesnesini oluşturun ve bağlantıyı açın
            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            //string query = "SELECT employee_name as Ad,employee_lastname as Soyad,staff_id as id,employee_employee_adress,emp_birthday as DogumTarihi,emp_phone as TelefonNo FROM Staff";
            string query = "SELECT employee_name as Ad,employee_lastname as Soyad,employee_adress,emp_birthday as DogumTarihi,emp_phone as TelefonNo FROM Employee";

            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffAdd.DataSource = dataTable;

            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connection);
        }

        private void textBox_Telefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            int girilenAscıı = Convert.ToInt32(e.KeyChar);
            //sayılar ve backspace dışındaki kalvye girişlerinde uyarı veriyor.
            if (!(girilenAscıı >= 48 && girilenAscıı <= 57 || girilenAscıı == Convert.ToInt32(Keys.Back)))
            {
                e.Handled = true;  //hatalı klavye girişinin textBox'a eklenmesini engeller.
                MessageBox.Show("Telefon kısmına sadece sayı girilebilir.");
            }
        }

        private void textBox_Soyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox_Ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }
        private bool TextBoxesAreNotEmpty()
        {
            // Eğer herhangi bir textbox boşsa false döndür
            if (string.IsNullOrWhiteSpace(textBox_Ad.Text) ||
                string.IsNullOrWhiteSpace(textBox_Soyad.Text) ||
                
                string.IsNullOrWhiteSpace(textBox_Adres.Text) ||
                string.IsNullOrWhiteSpace(textBoxUserName.Text) ||
                string.IsNullOrWhiteSpace(textBoxPassword.Text) ||
                string.IsNullOrWhiteSpace(dateTimePicker_dgmTarih.Text) ||
                string.IsNullOrWhiteSpace(textBox_Telefon.Text))
            {
                return false;
            }

            // Tüm textbox'lar doluysa true döndür
            return true;
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (TextBoxesAreNotEmpty())
            {
                dataGridView_staffAdd.DataSource = null;

                dataGridView_staffAdd.Rows.Clear();
                using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Employee(employee_name,employee_lastname,employee_adress,emp_username,emp_password,emp_birthday,emp_phone) VALUES (@Param1, @Param2, @Param4, @Param5 ,@Param6,@Param7,@Param8)", connection))
                    {
                        command.Parameters.AddWithValue("@Param1", textBox_Ad.Text.First().ToString().ToUpper() + textBox_Ad.Text.Substring(1).ToLower());
                        command.Parameters.AddWithValue("@Param2", textBox_Soyad.Text.ToUpper());
                       
                        command.Parameters.AddWithValue("@Param4", textBox_Adres.Text);
                        command.Parameters.AddWithValue("@Param5", textBoxUserName.Text);
                        command.Parameters.AddWithValue("@Param6", textBoxPassword.Text);
                        DateTime selectedDate;
                        if (DateTime.TryParse(dateTimePicker_dgmTarih.Text, out selectedDate))
                        {
                            command.Parameters.AddWithValue("@Param7", selectedDate);
                        }
                        else
                        {
                            // Hata durumunda kullanıcıya bir hata mesajı göster
                            MessageBox.Show("Geçersiz tarih formatı. Lütfen doğru bir tarih girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        command.Parameters.AddWithValue("@Param8", textBox_Telefon.Text);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                // Veriyi çeken sorguyu oluşturun
                string query = "SELECT employee_name as Ad,employee_lastname as Soyad,employee_employee_adress,emp_birthday as DogumTarihi,emp_phone as TelefonNo FROM Employee";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffAdd.DataSource = dataTable;

                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connectionnew);
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
