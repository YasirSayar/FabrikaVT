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
    public partial class FormStfEmpUpdate : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public FormStfEmpUpdate()
        {
            InitializeComponent();
        }

        private void FormStfEmpUpdate_Load(object sender, EventArgs e)
        {
            // Bağlantı nesnesini oluşturun ve bağlantıyı açın
            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            string query = "SELECT employee_id,employee_name as Ad,employee_lastname as Soyad,employee_adress,emp_birthday as DogumTarihi,emp_phone as TelefonNo,Product.p_title,Employee.product_id FROM Employee INNER JOIN Product ON Employee.product_id=Product.product_id";

            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffUpdate.DataSource = dataTable;

            // DataGridView'deki "id" sütununu gizle
            dataGridView_staffUpdate.Columns["employee_id"].Visible = false;
            


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

        private void textBox_Ad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void textBox_Soyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dataGridView_staffUpdate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Eğer tıklanan hücre satır başlığı ise işlemi gerçekleştirme
            if (e.RowIndex == -1)
                return;

            DataGridViewRow selectedRow = dataGridView_staffUpdate.Rows[e.RowIndex];

            // Örneğin, seçilen satırdaki belirli bir sütundan veri almak için:
            textBox_Ad.Text = selectedRow.Cells["Ad"].Value.ToString();
            textBox_Soyad.Text = selectedRow.Cells["Soyad"].Value.ToString();
            textBox_Email.Text = selectedRow.Cells["product_id"].Value.ToString();
            textBox_Adres.Text = selectedRow.Cells["employee_adress"].Value.ToString();
            dateTimePicker_dgmTarih.Text = selectedRow.Cells["DogumTarihi"].Value.ToString();
            textBox_Telefon.Text = selectedRow.Cells["TelefonNo"].Value.ToString();
            labelGizliid.Text = dataGridView_staffUpdate.Rows[e.RowIndex].Cells["employee_id"].Value.ToString();
        }
        private bool TextBoxesAreNotEmpty()
        {
            // Eğer herhangi bir textbox boşsa false döndür
            if (string.IsNullOrWhiteSpace(textBox_Ad.Text) ||
                string.IsNullOrWhiteSpace(textBox_Soyad.Text) ||
                string.IsNullOrWhiteSpace(textBox_Email.Text) ||
                string.IsNullOrWhiteSpace(textBox_Adres.Text) ||

                string.IsNullOrWhiteSpace(dateTimePicker_dgmTarih.Text) ||
                string.IsNullOrWhiteSpace(textBox_Telefon.Text))
            {
                return false;
            }

            // Tüm textbox'lar doluysa true döndür
            return true;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (TextBoxesAreNotEmpty())
            {
                dataGridView_staffUpdate.DataSource = null;

                dataGridView_staffUpdate.Rows.Clear();
                using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                {
                    using (SqlCommand command = new SqlCommand("UPDATE Employee SET employee_name = @Param1, employee_lastname = @Param2, product_id = @Param3, employee_adress = @Param4, emp_birthday = @Param5, emp_phone = @Param6 WHERE employee_id = @StaffId", connection))
                    {
                        command.Parameters.AddWithValue("@Param1", textBox_Ad.Text.First().ToString().ToUpper() + textBox_Ad.Text.Substring(1).ToLower());
                        command.Parameters.AddWithValue("@Param2", textBox_Soyad.Text.ToUpper());
                        command.Parameters.AddWithValue("@Param3", textBox_Email.Text);
                        command.Parameters.AddWithValue("@Param4", textBox_Adres.Text);

                        DateTime selectedDate;
                        if (DateTime.TryParse(dateTimePicker_dgmTarih.Text, out selectedDate))
                        {
                            command.Parameters.AddWithValue("@Param5", selectedDate);
                        }
                        else
                        {
                            // Hata durumunda kullanıcıya bir hata mesajı göster
                            MessageBox.Show("Geçersiz tarih formatı. Lütfen doğru bir tarih girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        command.Parameters.AddWithValue("@Param6", textBox_Telefon.Text);
                        command.Parameters.AddWithValue("@StaffId", labelGizliid.Text);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                // Veriyi çeken sorguyu oluşturun
                string query = "SELECT employee_id,employee_name as Ad,employee_lastname as Soyad,employee_adress,emp_birthday as DogumTarihi,emp_phone as TelefonNo,Product.p_title,Employee.product_id FROM Employee INNER JOIN Product ON Employee.product_id=Product.product_id";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffUpdate.DataSource = dataTable;
                dataGridView_staffUpdate.Columns["employee_id"].Visible = false;

                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connectionnew);
                MessageBox.Show("Güncelleme başarılı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonAra_Click(object sender, EventArgs e)
        {
            if (textBox_Ad.Text != "" || textBox_Soyad.Text != "" || textBox_Adres.Text != "" || textBox_Telefon.Text != "" || textBox_Email.Text != "")
            {
                try
                {

                    // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                    using (SqlConnection connection = baglantiObj.CreateConnection())
                    {
                        baglantiObj.OpenConnection(connection);
                        
                        string query = "SELECT employee_id,employee_name as Ad, employee_lastname as Soyad, product_id, employee_adress, emp_birthday as DogumTarihi, emp_phone as TelefonNo " +
                    "FROM Employee " +
                    "WHERE " +
                    "(employee_name LIKE @Param1 OR @Param1 IS NULL) AND " +
                    "(employee_lastname LIKE @Param2 OR @Param2 IS NULL) AND " +
                    "(product_id LIKE @Param3 OR @Param3 IS NULL) AND " +
                    "(employee_adress LIKE @Param4 OR @Param4 IS NULL) AND " +
                    "(emp_phone LIKE @Param5 OR @Param5 IS NULL)";


                        // SqlCommand'u oluşturun
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // TextBox'ları kullanarak parametre değerlerini ayarlayın
                            command.Parameters.AddWithValue("@Param1", string.IsNullOrEmpty(textBox_Ad.Text) ? (object)DBNull.Value : "%" + textBox_Ad.Text.First().ToString().ToUpper() + textBox_Ad.Text.Substring(1).ToLower() + "%");
                            command.Parameters.AddWithValue("@Param2", string.IsNullOrEmpty(textBox_Soyad.Text) ? (object)DBNull.Value : "%" + textBox_Soyad.Text.ToUpper() + "%");
                            command.Parameters.AddWithValue("@Param3", string.IsNullOrEmpty(textBox_Email.Text) ? (object)DBNull.Value : "%" + textBox_Email.Text + "%");
                            command.Parameters.AddWithValue("@Param4", string.IsNullOrEmpty(textBox_Adres.Text) ? (object)DBNull.Value : "%" + textBox_Adres.Text + "%");
                            command.Parameters.AddWithValue("@Param5", string.IsNullOrEmpty(textBox_Telefon.Text) ? (object)DBNull.Value : "%" + textBox_Telefon.Text + "%");

                            //MessageBox.Show(command.CommandText,"sorgu");
                            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'e veriyi yükleyin
                            dataGridView_staffUpdate.DataSource = dataTable;

                            // DataGridView'deki "id" sütununu gizle
                            dataGridView_staffUpdate.Columns["employee_id"].Visible = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hataaa: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }



            else
            {
                //MessageBox.Show("vsdavas", "dsafdsafv");
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connection = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connection);

                string query = "SELECT employee_id,employee_name as Ad,employee_lastname as Soyad,employee_adress,emp_birthday as DogumTarihi,emp_phone as TelefonNo,Product.p_title,Employee.product_id FROM Employee INNER JOIN Product ON Employee.product_id=Product.product_id";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffUpdate.DataSource = dataTable;

                // DataGridView'deki "id" sütununu gizle
                dataGridView_staffUpdate.Columns["employee_id"].Visible = false;


                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connection);
            }
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            textBox_Ad.Text = "";
            textBox_Soyad.Text = "";
            textBox_Adres.Text = "";
            textBox_Email.Text = "";
            textBox_Telefon.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = baglantiObj.CreateConnection())
                {
                    baglantiObj.OpenConnection(connection);

                    string query = "SELECT employee_id,employee_name as Ad, employee_lastname as Soyad, product_id, employee_adress, emp_birthday as DogumTarihi, emp_phone as TelefonNo " +
                                    "FROM Employee " +
                                    "WHERE " +
                                    "(emp_birthday = @Param6 OR @Param6 IS NULL)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        // dateTimePicker'dan seçili tarihi alın
                        DateTime selectedDate = dateTimePicker_dgmTarih.Value.Date;

                        // Parametreyi ekleyin
                        command.Parameters.AddWithValue("@Param6", selectedDate);

                        //MessageBox.Show(command.CommandText, "sorgu");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView_staffUpdate.DataSource = dataTable;
                        dataGridView_staffUpdate.Columns["employee_id"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
