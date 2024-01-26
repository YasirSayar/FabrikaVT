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
using System.Windows.Forms.VisualStyles;

namespace FabrikaVT
{
    public partial class FormStfProductAdd : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public string productid;
        public string part_id;
        
        public FormStfProductAdd()
        {
            InitializeComponent();
        }

        private void FormStfProductAdd_Load(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connection1 = baglantiObj.CreateConnection())
                {
                    baglantiObj.OpenConnection(connection1);

                    // SQL sorgusuyla veritabanından p_title değerlerini al
                    string query1 = "SELECT p_title FROM Product";
                    SqlCommand command = new SqlCommand(query1, connection1);
                    SqlDataReader reader = command.ExecuteReader();

                    // Okunan değerleri ComboBox'a ekle
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader["p_title"].ToString());
                    }

                    // Okuma işlemini kapat
                    reader.Close();
                }
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
                        comboBox2.Items.Add(reader["part_name"].ToString());
                    }

                    // Okuma işlemini kapat
                    reader.Close();
                }
                using (SqlConnection connection1 = baglantiObj.CreateConnection())
                {
                    baglantiObj.OpenConnection(connection1);

                    // SQL sorgusuyla veritabanından p_title değerlerini al
                    string query1 = "SELECT part_id FROM Part";
                    SqlCommand command = new SqlCommand(query1, connection1);
                    SqlDataReader reader = command.ExecuteReader();

                    // Okunan değerleri ComboBox'a ekle
                    while (reader.Read())
                    {
                        comboBox3PartIdHolder.Items.Add(reader["part_id"].ToString());
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
            string query = "SELECT \r\n    Product.p_title as Ürün,\r\n    Product.quantity as Adet,\r\n    Part.part_name as [Parça Adı],\r\n    Product.product_id,\r\n    Employee.employee_name,\r\n    Employee.employee_lastname,\r\n    Product.part_id \r\nFROM \r\n    Product \r\nINNER JOIN \r\n    Part ON Product.part_id = Part.part_id \r\nLEFT JOIN \r\n    Employee ON Employee.product_id = Product.product_id;\r\n";
            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dataTable;

            // dataGridView1'deki product_id sütununu gizle
            dataGridView1.Columns["product_id"].Visible = false;
            dataGridView1.Columns["part_id"].Visible = false;
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            textBoxQuantity.Text = string.Empty;
            comboBox1.Text = string.Empty;
            comboBox2.Text = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) { }
        //{
        //    if (e.RowIndex == -1)
        //        return;

        //    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
        //    textBoxQuantity.Text = selectedRow.Cells["Adet"].Value.ToString();
        //    string urunDeger = selectedRow.Cells["Ürün"].Value.ToString();
        //    productid = selectedRow.Cells["product_id"].Value.ToString();
        //    part_id = selectedRow.Cells["part_id"].Value.ToString();
        //    string part = selectedRow.Cells["Parça Adı"].Value.ToString();

        //    // ComboBox'ı seçilen değere göre ayarla
        //    if (!string.IsNullOrEmpty(urunDeger) && comboBox1.Items.Contains(urunDeger))
        //    {
        //        comboBox1.SelectedItem = urunDeger;
        //    }
        //    else
        //    {
        //        // Eğer ComboBox'ta belirli bir değer bulunamazsa, seçimi temizle
        //        comboBox1.SelectedItem = null;
        //    }
        //    if (!string.IsNullOrEmpty(part) && comboBox2.Items.Contains(part))
        //    {
        //        comboBox2.SelectedItem = part;
        //    }
        //    else
        //    {
        //        // Eğer ComboBox'ta belirli bir değer bulunamazsa, seçimi temizle
        //        comboBox2.SelectedItem = null;
        //        MessageBox.Show("dfşvasd", "vsavas");
        //    }
        //}

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxQuantity.Text) && comboBox2.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBox2.SelectedItem.ToString()) && !string.IsNullOrWhiteSpace(textBoxQuantity.Text))
            {
                try
                {

                    using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                    {
                        using (SqlCommand command = new SqlCommand("INSERT INTO Product(p_title,quantity,part_id) VALUES (@Param1,@Param2,@Param3)", connection))
                        {

                            command.Parameters.AddWithValue("@Param1", textBoxUrun.Text);
                            command.Parameters.AddWithValue("@Param2", textBoxQuantity.Text);
                            command.Parameters.AddWithValue("@Param3", comboBox3PartIdHolder.Text );

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
            string query = "SELECT \r\n    Product.p_title as Ürün,\r\n    Product.quantity as Adet,\r\n    Part.part_name as [Parça Adı],\r\n    Product.product_id,\r\n    Employee.employee_name,\r\n    Employee.employee_lastname,\r\n    Product.part_id \r\nFROM \r\n    Product \r\nINNER JOIN \r\n    Part ON Product.part_id = Part.part_id \r\nLEFT JOIN \r\n    Employee ON Employee.product_id = Product.product_id;\r\n";
            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlConnection connectionnew = baglantiObj.CreateConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dataTable;

            // dataGridView1'deki product_id sütununu gizle
            dataGridView1.Columns["product_id"].Visible = false;
            dataGridView1.Columns["part_id"].Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

                // comboBox2'de seçilen indeksi comboBox3PartIdHolder'a atayın
                comboBox3PartIdHolder.SelectedIndex = comboBox2.SelectedIndex;

        }
    }
}
