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

namespace FabrikaVT
{
    public partial class Form_staffDelete : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public Form_staffDelete()
        {
            InitializeComponent();
        }

        private void Form_staffDelete_Load(object sender, EventArgs e)
        {
            // Bağlantı nesnesini oluşturun ve bağlantıyı açın
            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            string query = "SELECT f_name as Ad,l_name as Soyad,staff_id as id,email,adress,s_birthday as DogumTarihi,s_phone as TelefonNo FROM Staff";

            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffdel.DataSource = dataTable;

            // DataGridView'deki "id" sütununu gizle
            dataGridView_staffdel.Columns["id"].Visible = false;


            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connection);


            buttonDelete.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\ekDosya\\delete.png");
            Image resizedimage = buttonDelete.Image.GetThumbnailImage(32, 32, null, IntPtr.Zero);
            //buttonUpdate.Image.GetThumbnailImage(16, 16, null, IntPtr.Zero);
            buttonDelete.Image = resizedimage;


        }

       

        private void dataGridView_staffdel_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Önceki seçili satırın rengini eski haline getir
            dataGridView_staffdel.Rows[e.RowIndex].DefaultCellStyle.BackColor = dataGridView_staffdel.DefaultCellStyle.BackColor;

            // Şu anki seçili satırın rengini değiştir
            dataGridView_staffdel.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
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

        private void dataGridView_staffdel_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            dataGridView_staffdel.DataSource = null;

            dataGridView_staffdel.Rows.Clear();
            using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM Staff WHERE  staff_id = @StaffId", connection))
                {

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
            string query = "SELECT f_name as Ad,l_name as Soyad,staff_id as id,email,adress,s_birthday as DogumTarihi,s_phone as TelefonNo FROM Staff";

            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffdel.DataSource = dataTable;

            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connectionnew);
            MessageBox.Show("Silme başarılı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView_staffdel_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            // DatagridView'de bir hücreye tıklandığında satırın rengini değiştirmek için bu olayı kullanabilirsiniz
            if (e.RowIndex >= 0)
            {
                // Tıklanan satırı seçili yap
                dataGridView_staffdel.Rows[e.RowIndex].Selected = true;

                // Tıklanan satırın rengini değiştir (RowPrePaint olayını tetikleyecektir)
                dataGridView_staffdel.InvalidateRow(e.RowIndex);
            }
            labelGizliid.Text = dataGridView_staffdel.Rows[e.RowIndex].Cells["id"].Value.ToString();
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

                        string query = "SELECT f_name as Ad, l_name as Soyad, staff_id as id, email, adress, s_birthday as DogumTarihi, s_phone as TelefonNo " +
                    "FROM Staff " +
                    "WHERE " +
                    "(f_name LIKE @Param1 OR @Param1 IS NULL) AND " +
                    "(l_name LIKE @Param2 OR @Param2 IS NULL) AND " +
                    "(email LIKE @Param3 OR @Param3 IS NULL) AND " +
                    "(adress LIKE @Param4 OR @Param4 IS NULL) AND " +
                    "(s_phone LIKE @Param5 OR @Param5 IS NULL)";


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
                            dataGridView_staffdel.DataSource = dataTable;

                            // DataGridView'deki "id" sütununu gizle
                            dataGridView_staffdel.Columns["id"].Visible = false;
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

                // Veriyi çeken sorguyu oluşturun
                string query = "SELECT f_name as Ad,l_name as Soyad,staff_id as id,email,adress,s_birthday as DogumTarihi,s_phone as TelefonNo FROM Staff";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffdel.DataSource = dataTable;

                // DataGridView'deki "id" sütununu gizle
                dataGridView_staffdel.Columns["id"].Visible = false;


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

                        string query = "SELECT f_name as Ad, l_name as Soyad, staff_id as id, email, adress, s_birthday as DogumTarihi, s_phone as TelefonNo " +
                                        "FROM Staff " +
                                        "WHERE " +
                                        "(s_birthday = @Param6 OR @Param6 IS NULL)";

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

                            dataGridView_staffdel.DataSource = dataTable;
                            dataGridView_staffdel.Columns["id"].Visible = false;
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
        
