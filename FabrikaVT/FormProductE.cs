using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace FabrikaVT
{
    public partial class FormProductE : Form
    {
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public string productid;
        public FormProductE()
        {
            InitializeComponent();
        }

        private void FormProductE_Load(object sender, EventArgs e)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            string query = "SELECT Product.p_title as Ürün,Product.quantity as Adet ,Part.part_name as [Parça Adı],Product.product_id , Employee.employee_name,Employee.employee_lastname FROM Product INNER JOIN Part ON Product.part_id = Part.part_id INNER JOIN Employee ON Employee.product_id=Product.product_id";
            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = dataTable;

            // dataGridView1'deki product_id sütununu gizle
            dataGridView1.Columns["product_id"].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            // string queryOrder = "SELECT Customer.Customer_id, Customer.cf_name as[Müş. Adı], Customer.cl_name as [Müş. Soyad] , Orders.Order_id as [Sipariş No], Orders.status as Durum, OrderLine.orderQuantity as Adet, Product.p_title as Ürün, Customer.stf_id,Customer.Customer_id \r\nFROM Customer\r\nJOIN Orders ON Customer.Customer_id = Orders.cst_id\r\nJOIN OrderLine ON Orders.Order_id = OrderLine.Order_id\r\nJOIN Product ON OrderLine.product_id = Product.product_id;";

            
            textBoxQuantity.Text = selectedRow.Cells["Adet"].Value.ToString();
            //  textBox_Adres.Text = selectedRow.Cells["Adres"].Value.ToString();
            string urunDeger = selectedRow.Cells["Ürün"].Value.ToString();
            productid = selectedRow.Cells["product_id"].Value.ToString();

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
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxQuantity.Text)  || string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

                using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                {
                    connection.Open();

                   

                    using (SqlCommand command = new SqlCommand("UPDATE Product SET quantity = @Param1 WHERE product_id= @id", connection))
                    {
                        command.Parameters.AddWithValue("@Param1", textBoxQuantity.Text);
                        command.Parameters.AddWithValue("@id", productid);
                        command.ExecuteNonQuery();
                    }

                    // Diğer sorguları buraya ekleyebilirsiniz

                    // SqlConnection'ı kapatmak için bir sonraki using bloğunu kullanabilirsiniz
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                string query = "SELECT Product.p_title as Ürün,Product.quantity as Adet ,Part.part_name as [Parça Adı],Product.product_id , Employee.employee_name,Employee.employee_lastname FROM Product INNER JOIN Part ON Product.part_id = Part.part_id INNER JOIN Employee ON Employee.product_id=Product.product_id";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;



                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connectionnew);
                MessageBox.Show("Güncelleme başarılı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBoxQuantity.Text != string.Empty || comboBox1.Text != string.Empty )
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
                        string query = "SELECT Product.p_title as Ürün,Product.quantity as Adet ,Part.part_name as [Parça Adı],Product.product_id , Employee.employee_name,Employee.employee_lastname FROM Product INNER JOIN Part ON Product.part_id = Part.part_id INNER JOIN Employee ON Employee.product_id=Product.product_id where (Product.p_title LIKE @Param1 OR @Param1 IS NULL) AND (quantity LIKE @Param2 OR @Param2 IS NULL)";




                        // SqlCommand'u oluşturun
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            // TextBox'ları kullanarak parametre değerlerini ayarlayın
                            command.Parameters.AddWithValue("@Param1", string.IsNullOrEmpty(comboBox1.Text) ? (object)DBNull.Value : "%" + comboBox1.Text + "%");
                            command.Parameters.AddWithValue("@Param2", string.IsNullOrEmpty(textBoxQuantity.Text) ? (object)DBNull.Value : "%" + textBoxQuantity.Text + "%");
                             
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
                string query = "SELECT Product.p_title as Ürün,Product.quantity as Adet ,Part.part_name as [Parça Adı],Product.product_id , Employee.employee_name,Employee.employee_lastname FROM Product INNER JOIN Part ON Product.part_id = Part.part_id INNER JOIN Employee ON Employee.product_id=Product.product_id";
                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // dataGridView1 üzerindeki sütunları otomatik olarak oluşturmasını sağlayın
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.DataSource = dataTable;

                // dataGridView1'deki product_id sütununu gizle
                dataGridView1.Columns["product_id"].Visible = false;
                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connection);
            }
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            
                textBoxQuantity.Text = string.Empty;
                comboBox1.Text = string.Empty;
            
        }
    }
}
