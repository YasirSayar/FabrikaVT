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
    public partial class FormPartAdd : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public string suplier_id;
        public string part_id;
        public FormPartAdd()
        {
            InitializeComponent();
        }

        private void FormPartAdd_Load(object sender, EventArgs e)
        {
            try
            {

                
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
                        comboBox3PartIdHolder.Items.Add(reader["suplier_id"].ToString());
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
            string query = "SELECT Part.part_name as [Parça Adı], Part.part_quantity as Adet , Suplier.sup_name as [Tedarikçi],Part.part_id FROM Part INNER JOIN Suplier ON Part.suplier_id=Suplier.suplier_id";
            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dataTable;

            // dataGridView1'deki product_id sütununu gizle
           
            dataGridView1.Columns["part_id"].Visible = false;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxQuantity.Text) && comboBox2.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBox2.SelectedItem.ToString()) && !string.IsNullOrWhiteSpace(textBoxQuantity.Text))
            {
                try
                {

                    using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                    {
                        using (SqlCommand command = new SqlCommand("INSERT INTO Part(part_name,part_quantity,suplier_id) VALUES (@Param1,@Param2,@Param3)", connection))
                        {

                            command.Parameters.AddWithValue("@Param1", textBoxUrun.Text);
                            command.Parameters.AddWithValue("@Param2", textBoxQuantity.Text);
                            command.Parameters.AddWithValue("@Param3", comboBox3PartIdHolder.Text);

                            connection.Open();
                            command.ExecuteNonQuery();
                        }

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }

            }




            else
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            string query = "SELECT Part.part_name as [Parça Adı], Part.part_quantity as Adet , Suplier.sup_name as [Tedarikçi],Part.part_id FROM Part INNER JOIN Suplier ON Part.suplier_id=Suplier.suplier_id";
            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlConnection connectionnew = baglantiObj.CreateConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dataTable;

            // dataGridView1'deki product_id sütununu gizle
            dataGridView1.Columns["part_id"].Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
                // comboBox2'de seçilen indeksi comboBox3PartIdHolder'a atayın
                comboBox3PartIdHolder.SelectedIndex = comboBox2.SelectedIndex;
        }
    }
}
