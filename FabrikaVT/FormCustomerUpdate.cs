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
    public partial class FormCustomerUpdate : Form
    {
        public string customerID;
        public string Persname;
        public string Perslastname;
        public int staffId;
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public void SetStaffId(int id)
        {
            staffId = id;
        }
        public FormCustomerUpdate()
        {
            InitializeComponent();
        }

        private void FormCustomerUpdate_Load(object sender, EventArgs e)
        {
            // Bağlantı nesnesini oluşturun ve bağlantıyı açın
            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            //string query = "SELECT f_name as Ad,l_name as Soyad,staff_id as id,email,adress,s_birthday as DogumTarihi,s_phone as TelefonNo FROM Staff";
            string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where Staff.staff_id=" + staffId + ";";

            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffAdd.DataSource = dataTable;

            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connection);
            dataGridView_staffAdd.Columns["c_id"].Visible = false;

           
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

        private void dataGridView_staffAdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Eğer tıklanan hücre satır başlığı ise işlemi gerçekleştirme
            if (e.RowIndex == -1)
                return;

            DataGridViewRow selectedRow = dataGridView_staffAdd.Rows[e.RowIndex];

            // Örneğin, seçilen satırdaki belirli bir sütundan veri almak için:
            textBox_Ad.Text = selectedRow.Cells["Müş. Adı"].Value.ToString();
            textBox_Soyad.Text = selectedRow.Cells["Müş. Soyad"].Value.ToString();
            customerID = selectedRow.Cells["c_id"].Value.ToString();
            textBox_Adres.Text = selectedRow.Cells["Adres"].Value.ToString();
           
        }
        private bool TextBoxesAreNotEmpty()
        {
            // Eğer herhangi bir textbox boşsa false döndür
            if (string.IsNullOrWhiteSpace(textBox_Ad.Text) ||
                string.IsNullOrWhiteSpace(textBox_Soyad.Text) ||
                string.IsNullOrWhiteSpace(textBox_Adres.Text)
                 )
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
                    using (SqlCommand command = new SqlCommand("UPDATE Customer SET cf_name = @Param1, cl_name = @Param2, c_adress = @Param3 WHERE Customer_id = @customerID", connection))
                    {
                        command.Parameters.AddWithValue("@Param1", textBox_Ad.Text.First().ToString().ToUpper() + textBox_Ad.Text.Substring(1).ToLower());
                        command.Parameters.AddWithValue("@Param2", textBox_Soyad.Text.ToUpper());
                        
                        command.Parameters.AddWithValue("@Param3", textBox_Adres.Text);
                        command.Parameters.AddWithValue("@customerID", customerID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where Staff.staff_id=" + staffId + ";";
                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffAdd.DataSource = dataTable;
                dataGridView_staffAdd.Columns["c_id"].Visible = false;


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
            if (textBox_Ad.Text != "" || textBox_Soyad.Text != "" || textBox_Adres.Text != ""  || textBox_Adres.Text != "")
            {
                try
                {

                    // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                    using (SqlConnection connection = baglantiObj.CreateConnection())
                    {
                        baglantiObj.OpenConnection(connection);
                        string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where (cf_name LIKE @Param1 OR @Param1 IS NULL) AND (cl_name LIKE @Param2 OR @Param2 IS NULL)  AND (c_adress LIKE @Param3 OR @Param3 IS NULL) AND   Staff.staff_id=" + staffId + ";";

                        


                        // SqlCommand'u oluşturun
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // TextBox'ları kullanarak parametre değerlerini ayarlayın
                            command.Parameters.AddWithValue("@Param1", string.IsNullOrEmpty(textBox_Ad.Text) ? (object)DBNull.Value : "%" + textBox_Ad.Text.First().ToString().ToUpper() + textBox_Ad.Text.Substring(1).ToLower() + "%");
                            command.Parameters.AddWithValue("@Param2", string.IsNullOrEmpty(textBox_Soyad.Text) ? (object)DBNull.Value : "%" + textBox_Soyad.Text.ToUpper() + "%");
                            command.Parameters.AddWithValue("@Param3", string.IsNullOrEmpty(textBox_Adres.Text) ? (object)DBNull.Value : "%" + textBox_Adres.Text + "%");

                            //MessageBox.Show(command.CommandText,"sorgu");
                            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'e veriyi yükleyin
                            dataGridView_staffAdd.DataSource = dataTable;

                            // DataGridView'deki "id" sütununu gizle
                            dataGridView_staffAdd.Columns["c_id"].Visible = false;
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
                string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where Staff.staff_id=" + staffId + ";";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffAdd.DataSource = dataTable;

                // DataGridView'deki "id" sütununu gizle
                dataGridView_staffAdd.Columns["c_id"].Visible = false;


                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connection);
            }
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            textBox_Ad.Text = "";
            textBox_Soyad.Text = "";
            textBox_Adres.Text = "";
            
        }

        private void dataGridView_staffAdd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
