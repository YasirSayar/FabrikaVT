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
    public partial class FormPartUpdate : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public string suplier;
        public string idsuplier;
        public string partid;
        public FormPartUpdate()
        {
            InitializeComponent();
        }

        private void FormPartUpdate_Load(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connection1 = baglantiObj.CreateConnection())
                {
                    baglantiObj.OpenConnection(connection1);

                    // SQL sorgusuyla veritabanından p_title değerlerini al
                    string query1 = "SELECT part_name FROM Part";
                    SqlCommand command = new SqlCommand(query1, connection1);
                    SqlDataReader reader = command.ExecuteReader();

                    // Okunan değerleri ComboBox'a ekle
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["part_name"].ToString());
                    }

                    // Okuma işlemini kapat
                    reader.Close();
                }
                using (SqlConnection connection1 = baglantiObj.CreateConnection())
                {
                    baglantiObj.OpenConnection(connection1);

                    // SQL sorgusuyla veritabanından p_title değerlerini al
                    string query1 = "SELECT sup_name FROM Suplier";
                    SqlCommand command = new SqlCommand(query1, connection1);
                    SqlDataReader reader = command.ExecuteReader();

                    // Okunan değerleri ComboBox'a ekle
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader["sup_name"].ToString());
                    }

                    // Okuma işlemini kapat
                    reader.Close();
                }
                using (SqlConnection connection1 = baglantiObj.CreateConnection())
                {
                    baglantiObj.OpenConnection(connection1);

                    // SQL sorgusuyla veritabanından p_title değerlerini al
                    string query1 = "SELECT suplier_id FROM Suplier";
                    SqlCommand command = new SqlCommand(query1, connection1);
                    SqlDataReader reader = command.ExecuteReader();

                    // Okunan değerleri ComboBox'a ekle
                    while (reader.Read())
                    {
                        comboBox3Holder.Items.Add(reader["suplier_id"].ToString());
                    }

                    // Okuma işlemini kapat
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            string query = "SELECT Part.part_name as [Parça Adı], Part.part_quantity as Adet , Suplier.sup_name as [Tedarikçi],Part.part_id,Part.suplier_id FROM Part INNER JOIN Suplier ON Part.suplier_id=Suplier.suplier_id";
            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dataTable;

            // dataGridView1'deki product_id sütununu gizle
            dataGridView1.Columns["part_id"].Visible = false;
            dataGridView1.Columns["suplier_id"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
           
            textBoxQuantity.Text = selectedRow.Cells["Adet"].Value.ToString();
            //  textBox_Adres.Text = selectedRow.Cells["Adres"].Value.ToString();
            string urunDeger = selectedRow.Cells["Parça Adı"].Value.ToString();
            suplier = selectedRow.Cells["Tedarikçi"].Value.ToString();
            idsuplier= selectedRow.Cells["suplier_id"].Value.ToString();
            partid= selectedRow.Cells["part_id"].Value.ToString();



            // ComboBox'ı seçilen değere göre ayarla
            if (!string.IsNullOrEmpty(urunDeger) && comboBox1.Items.Contains(urunDeger))
            {
                comboBox1.SelectedItem = urunDeger;
            }
            else
            {
                
                // Eğer ComboBox'ta belirli bir değer bulunamazsa, seçimi temizle
                comboBox1.SelectedItem = null;
            }
            if (!string.IsNullOrEmpty(suplier) && comboBox2.Items.Contains(suplier))
            {
                comboBox2.SelectedItem = suplier;
            }
            else
            {
                
                // Eğer ComboBox'ta belirli bir değer bulunamazsa, seçimi temizle
                comboBox2.SelectedItem = null;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            // comboBox2'de seçilen indeksi comboBox3PartIdHolder'a atayın
            comboBox3Holder.SelectedIndex = comboBox2.SelectedIndex;

        }

        private void labelProduct_Click(object sender, EventArgs e)
        {

        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            comboBox2.Text= string.Empty;
            textBoxQuantity.Text= string.Empty;
            comboBox1.Text= string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxQuantity.Text != string.Empty || comboBox1.Text != string.Empty ||comboBox2.Text!=string.Empty)
            {
                try
                {
                    // MessageBox.Show("boş değil birisi en az");
                    //  return;
                    // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                    using (SqlConnection connection = baglantiObj.CreateConnection())
                    {
                        baglantiObj.OpenConnection(connection);
                        //string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where (cf_name LIKE @Param1 OR @Param1 IS NULL) AND (cl_name LIKE @Param2 OR @Param2 IS NULL)  AND (c_adress LIKE @Param3 OR @Param3 IS NULL) AND (Orders.) ";
                        string query = "SELECT Part.part_name as [Parça Adı], Part.part_quantity as Adet , Suplier.sup_name as [Tedarikçi],Part.part_id,Part.suplier_id FROM Part INNER JOIN Suplier ON Part.suplier_id=Suplier.suplier_id where (part_name LIKE @Param1 OR @Param1 IS NULL) AND (part_quantity LIKE @Param2 OR @Param2 IS NULL) AND (Suplier.sup_name LIKE @Param3 OR @Param3 IS NULL);";




                        // SqlCommand'u oluşturun
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // TextBox'ları kullanarak parametre değerlerini ayarlayın
                            command.Parameters.AddWithValue("@Param1", string.IsNullOrEmpty(comboBox1.Text) ? (object)DBNull.Value : "%" + comboBox1.Text + "%");
                            command.Parameters.AddWithValue("@Param2", string.IsNullOrEmpty(textBoxQuantity.Text) ? (object)DBNull.Value : "%" + textBoxQuantity.Text + "%");
                            command.Parameters.AddWithValue("@Param3", string.IsNullOrEmpty(comboBox2.Text) ? (object)DBNull.Value : "%" + comboBox2.Text + "%");

                            //MessageBox.Show(command.CommandText,"sorgu");
                            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'e veriyi yükleyin
                            dataGridView1.DataSource = dataTable;

                            // dataGridView1.Columns["product_id"].Visible = false;
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
                SqlConnection connection = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connection);
                //MessageBox.Show("hepsi boş", "dsafdsafv");
                string query = "SELECT Part.part_name as [Parça Adı], Part.part_quantity as Adet , Suplier.sup_name as [Tedarikçi],Part.part_id,Part.suplier_id FROM Part INNER JOIN Suplier ON Part.suplier_id=Suplier.suplier_id";
                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dataTable;

                // dataGridView1'deki product_id sütununu gizle
                dataGridView1.Columns["part_id"].Visible = false;
                dataGridView1.Columns["suplier_id"].Visible = false;
                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connection);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxQuantity.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                {
                    connection.Open();



                    using (SqlCommand command = new SqlCommand("UPDATE Part SET part_quantity = @Param1,part_name=@Param2,suplier_id=@param3 WHERE part_id= @id", connection))
                    {
                        command.Parameters.AddWithValue("@Param1", textBoxQuantity.Text);
                        command.Parameters.AddWithValue("@Param2", comboBox1.Text);
                        command.Parameters.AddWithValue("@Param3", comboBox3Holder.Text);
                        command.Parameters.AddWithValue("@id", partid);
                        command.ExecuteNonQuery();
                    }

                    // Diğer sorguları buraya ekleyebilirsiniz

                    // SqlConnection'ı kapatmak için bir sonraki using bloğunu kullanabilirsiniz
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                string query = "SELECT Part.part_name as [Parça Adı], Part.part_quantity as Adet , Suplier.sup_name as [Tedarikçi],Part.part_id,Part.suplier_id FROM Part INNER JOIN Suplier ON Part.suplier_id=Suplier.suplier_id";
                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dataTable;

                // dataGridView1'deki product_id sütununu gizle
                dataGridView1.Columns["part_id"].Visible = false;
                dataGridView1.Columns["suplier_id"].Visible = false;

                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connectionnew);
                MessageBox.Show("Güncelleme başarılı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
