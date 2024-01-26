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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace FabrikaVT
{
    public partial class FormOrderEkranı : Form
    {
        public string customerID;
        public string Persname;
        public string Perslastname;
        public int staffId;
        public string orderid;
        public string eklerkenorderid;
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public void SetStaffId(int id)
        {
            staffId = id;
        }
        public FormOrderEkranı()
        {
            InitializeComponent();
        }

        private void FormOrderEkranı_Load(object sender, EventArgs e)
        {
           // MessageBox.Show((comboBox1.SelectedIndex + 200).ToString(), "deefff");
           // MessageBox.Show(staffId.ToString(), "İİİDDDD");
            // Bağlantı nesnesini oluşturun ve bağlantıyı açın
            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            //string query = "SELECT f_name as Ad,l_name as Soyad,staff_id as id,email,adress,s_birthday as DogumTarihi,s_phone as TelefonNo FROM Staff";
            string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id  ";

            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffAdd.DataSource = dataTable;

            string queryOrder = "SELECT Customer.Customer_id, Customer.cf_name as[Müş. Adı], Customer.cl_name as [Müş. Soyad] , Orders.Order_id as [Sipariş No], Orders.status as Durum, OrderLine.orderQuantity as Adet, Product.p_title as Ürün, Customer.stf_id\r\nFROM Customer\r\nJOIN Orders ON Customer.Customer_id = Orders.cst_id\r\nJOIN OrderLine ON Orders.Order_id = OrderLine.Order_id\r\nJOIN Product ON OrderLine.product_id = Product.product_id ;";
            SqlDataAdapter adapterTb2 = new SqlDataAdapter(queryOrder, connection);
            DataTable dataTable2=new DataTable();
            adapterTb2.Fill(dataTable2);
            dataGridViewOrders.DataSource = dataTable2;
            dataGridViewOrders.Columns["Customer_id"].Visible = false;
            dataGridViewOrders.Columns["stf_id"].Visible = false;

            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connection);
            dataGridView_staffAdd.Columns["c_id"].Visible = false;


            //COMBO BOX DOLDURMACA
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
        }

        private void buttonAra_Click(object sender, EventArgs e)
        {
            if (textBox_Ad.Text != "" || textBox_Soyad.Text != "" || textBox_Adres.Text != "" || textBox_Adres.Text != "")
            {
                try
                {

                    // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                    using (SqlConnection connection = baglantiObj.CreateConnection())
                    {
                        baglantiObj.OpenConnection(connection);
                        string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where (cf_name LIKE @Param1 OR @Param1 IS NULL) AND (cl_name LIKE @Param2 OR @Param2 IS NULL)  AND (c_adress LIKE @Param3 OR @Param3 IS NULL) ;";



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
                string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id  ;";

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
            customerID = null;
            textBox_Ad.Text = "";
            textBox_Soyad.Text = "";
            textBox_Adres.Text = "";
            textBoxOrderId.Text = string.Empty;
            textBoxQuantity.Text=string.Empty;
            comboBox1.Text = string.Empty;
            comboBoxStatus.Text = string.Empty;
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

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox_Ad.Text != string.Empty || textBox_Soyad.Text != string.Empty || textBox_Adres.Text != string.Empty || textBoxOrderId.Text != string.Empty || textBoxQuantity.Text!=string.Empty || comboBox1.Text!=string.Empty || comboBoxStatus.Text!=string.Empty)
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
                        string queryOrder = "SELECT Customer.Customer_id, Customer.cf_name as[Müş. Adı], Customer.cl_name as [Müş. Soyad] , Orders.Order_id as [Sipariş No], Orders.status as Durum, OrderLine.orderQuantity as Adet, Product.p_title as Ürün, Customer.stf_id\r\nFROM Customer\r\nJOIN Orders ON Customer.Customer_id = Orders.cst_id\r\nJOIN OrderLine ON Orders.Order_id = OrderLine.Order_id\r\nJOIN Product ON OrderLine.product_id = Product.product_id where (cf_name LIKE @Param1 OR @Param1 IS NULL) AND (cl_name LIKE @Param2 OR @Param2 IS NULL)  AND (c_adress LIKE @Param3 OR @Param3 IS NULL) AND (Orders.Order_id LIKE @ParamOrderId OR @ParamOrderId IS NULL) AND (status LIKE @status OR @status IS NULL) AND (orderQuantity LIKE @orderQuantity OR @orderQuantity IS NULL) AND (p_title LIKE @UrunAdı OR @UrunAdı IS NULL) ";




                        // SqlCommand'u oluşturun
                        using (SqlCommand command = new SqlCommand(queryOrder, connection))
                        {
                            // TextBox'ları kullanarak parametre değerlerini ayarlayın
                            command.Parameters.AddWithValue("@Param1", string.IsNullOrEmpty(textBox_Ad.Text) ? (object)DBNull.Value : "%" + textBox_Ad.Text.First().ToString().ToUpper() + textBox_Ad.Text.Substring(1).ToLower() + "%");
                            command.Parameters.AddWithValue("@Param2", string.IsNullOrEmpty(textBox_Soyad.Text) ? (object)DBNull.Value : "%" + textBox_Soyad.Text.ToUpper() + "%");
                            command.Parameters.AddWithValue("@Param3", string.IsNullOrEmpty(textBox_Adres.Text) ? (object)DBNull.Value : "%" + textBox_Adres.Text + "%");
                            command.Parameters.AddWithValue("@ParamOrderId", string.IsNullOrEmpty(textBoxOrderId.Text) ? (object)DBNull.Value : "%" + textBoxOrderId.Text + "%");
                            switch (comboBoxStatus.Text)
                            {
                                case "Teslim Edildi":
                                    command.Parameters.AddWithValue("@status", string.IsNullOrEmpty("1") ? (object)DBNull.Value : "%" + "1" + "%");
                                    break;
                                case "İşlemde":
                                    command.Parameters.AddWithValue("@status", string.IsNullOrEmpty("0") ? (object)DBNull.Value : "%" + "0" + "%");
                                    break;
                                case "İptal Edildi":
                                    command.Parameters.AddWithValue("@status", string.IsNullOrEmpty("-1") ? (object)DBNull.Value : "%" + "-1" + "%");
                                    break;
                                default:
                                    command.Parameters.AddWithValue("@status", string.IsNullOrEmpty("") ? (object)DBNull.Value : "%" +""  + "%");
                                    break;
                            }
                            command.Parameters.AddWithValue("@orderQuantity", string.IsNullOrEmpty(textBoxQuantity.Text) ? (object)DBNull.Value : "%" + textBoxQuantity.Text + "%");
                            command.Parameters.AddWithValue("@UrunAdı", string.IsNullOrEmpty(comboBox1.Text) ? (object)DBNull.Value : "%" + comboBox1.Text + "%");
                            //MessageBox.Show(command.CommandText,"sorgu");
                            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // DataGridView'e veriyi yükleyin
                            dataGridViewOrders.DataSource = dataTable;

                            dataGridViewOrders.Columns["Customer_id"].Visible = false;
                            dataGridViewOrders.Columns["stf_id"].Visible = false;
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
                //return;
                string queryOrder = "SELECT Customer.Customer_id, Customer.cf_name as[Müş. Adı], Customer.cl_name as [Müş. Soyad] , Orders.Order_id as [Sipariş No], Orders.status as Durum, OrderLine.orderQuantity as Adet, Product.p_title as Ürün, Customer.stf_id\r\nFROM Customer\r\nJOIN Orders ON Customer.Customer_id = Orders.cst_id\r\nJOIN OrderLine ON Orders.Order_id = OrderLine.Order_id\r\nJOIN Product ON OrderLine.product_id = Product.product_id ;";
                SqlDataAdapter adapterTb2 = new SqlDataAdapter(queryOrder, connection);
                DataTable dataTable2 = new DataTable();
                adapterTb2.Fill(dataTable2);
                dataGridViewOrders.DataSource = dataTable2;
                dataGridViewOrders.Columns["Customer_id"].Visible = false;
                dataGridViewOrders.Columns["stf_id"].Visible = false;

                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connection);
            }
        }

        private void dataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            DataGridViewRow selectedRow = dataGridViewOrders.Rows[e.RowIndex];
            // string queryOrder = "SELECT Customer.Customer_id, Customer.cf_name as[Müş. Adı], Customer.cl_name as [Müş. Soyad] , Orders.Order_id as [Sipariş No], Orders.status as Durum, OrderLine.orderQuantity as Adet, Product.p_title as Ürün, Customer.stf_id,Customer.Customer_id \r\nFROM Customer\r\nJOIN Orders ON Customer.Customer_id = Orders.cst_id\r\nJOIN OrderLine ON Orders.Order_id = OrderLine.Order_id\r\nJOIN Product ON OrderLine.product_id = Product.product_id;";

            textBoxOrderId.Text = selectedRow.Cells["Sipariş No"].Value.ToString();
               orderid= selectedRow.Cells["Sipariş No"].Value.ToString();
            string durum = selectedRow.Cells["Durum"].Value.ToString();
            switch (durum)
            {
                case "1"://Asıl değeri okuyor. Teslim edildi falan yazsa da 
                    comboBoxStatus.SelectedIndex = 0;
                    break;
                case "0":
                    comboBoxStatus.SelectedIndex = 1;
                    break;
                case "-1":
                    comboBoxStatus.SelectedIndex = 2;
                    break;
 
                default:
                    comboBoxStatus.SelectedIndex = -1; 
                    break;
            }
            customerID = selectedRow.Cells["Customer_id"].Value.ToString();
            textBoxQuantity.Text = selectedRow.Cells["Adet"].Value.ToString();
            //  textBox_Adres.Text = selectedRow.Cells["Adres"].Value.ToString();
            string urunDeger = selectedRow.Cells["Ürün"].Value.ToString();

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

        private void dataGridViewOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewOrders.Columns["Durum"].Index && e.Value != null && e.Value != DBNull.Value)
            {
                int statusValue;

                if (int.TryParse(e.Value.ToString(), out statusValue))
                {
                    switch (statusValue)
                    {
                        case 1:
                            e.Value = "Teslim Edildi";
                            break;
                        case 0:
                            e.Value = "İşlemde";
                            break;
                        case -1:
                            e.Value = "İptal Edildi";
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    //e.Value = "Belirsiz";
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxQuantity.Text) ||string.IsNullOrEmpty(textBoxOrderId.Text)||string.IsNullOrEmpty(comboBoxStatus.Text)||string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
              
                using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("UPDATE Orders SET status = @Param1 WHERE Order_id = @orderidTxt", connection))
                    {
                        switch (comboBoxStatus.Text)
                        {
                            case "Teslim Edildi":
                                command.Parameters.AddWithValue("@Param1", "1");
                                break;
                            case "İşlemde":
                                command.Parameters.AddWithValue("@Param1", "0");
                                break;
                            case "İptal Edildi":
                                command.Parameters.AddWithValue("@Param1", "-1");
                                break;
                            default:
                                comboBoxStatus.SelectedIndex = -1;
                                break;
                        }
                        command.Parameters.AddWithValue("@orderidTxt", orderid);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand("UPDATE OrderLine SET orderQuantity = @Param1 WHERE Order_id = @orderidTxt", connection))
                    {
                        command.Parameters.AddWithValue("@Param1", textBoxQuantity.Text);
                        command.Parameters.AddWithValue("@orderidTxt", orderid);
                        command.ExecuteNonQuery();
                    }

                    // Diğer sorguları buraya ekleyebilirsiniz

                    // SqlConnection'ı kapatmak için bir sonraki using bloğunu kullanabilirsiniz
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                string query = "SELECT Customer.Customer_id, Customer.cf_name as[Müş. Adı], Customer.cl_name as [Müş. Soyad] , Orders.Order_id as [Sipariş No], Orders.status as Durum, OrderLine.orderQuantity as Adet, Product.p_title as Ürün, Customer.stf_id\r\nFROM Customer\r\nJOIN Orders ON Customer.Customer_id = Orders.cst_id\r\nJOIN OrderLine ON Orders.Order_id = OrderLine.Order_id\r\nJOIN Product ON OrderLine.product_id = Product.product_id ;";
                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridViewOrders.Columns["Customer_id"].Visible = false;
                dataGridViewOrders.Columns["stf_id"].Visible = false;
                // DataGridView'e veriyi yükleyin
                dataGridViewOrders.DataSource = dataTable;
              


                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connectionnew);
                MessageBox.Show("Güncelleme başarılı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxQuantity.Text)&& comboBoxStatus.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBoxStatus.SelectedItem.ToString()) && comboBox1.SelectedItem != null && !string.IsNullOrWhiteSpace(comboBox1.SelectedItem.ToString()))
            {
                using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Orders(status,cst_id) VALUES (@status1,@Param2)", connection))
                    {
                        switch (comboBoxStatus.Text)
                        {
                            case "Teslim Edildi":
                                command.Parameters.AddWithValue("@status1", "1");
                                break;
                            case "İşlemde":
                                command.Parameters.AddWithValue("@status1", "0");
                                break;
                            case "İptal Edildi":
                                command.Parameters.AddWithValue("@status1", "-1");
                                break;
                            default:
                                comboBoxStatus.SelectedIndex = -1;
                                break;
                        }
                        command.Parameters.AddWithValue("@Param2", customerID.ToString());

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    try
                    {

                        using (SqlConnection connection1 = baglantiObj.CreateConnection())
                        {
                            baglantiObj.OpenConnection(connection1);

                            // SQL sorgusuyla veritabanından p_title değerlerini al
                            string query1 = "SELECT Order_id FROM Orders ";
                            SqlCommand command = new SqlCommand(query1, connection1);
                            SqlDataReader reader = command.ExecuteReader();

                            // Okunan değerleri ComboBox'a ekle
                            while (reader.Read())
                            {
                                eklerkenorderid=(reader["Order_id"].ToString());
                            }

                            // Okuma işlemini kapat
                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    using (SqlCommand command = new SqlCommand("INSERT INTO OrderLine(Order_id,product_id,orderQuantity) VALUES (@Param476,@Param2,@Param3)", connection))
                    {
                        command.Parameters.AddWithValue("@Param476",eklerkenorderid);
                        command.Parameters.AddWithValue("@Param2", (comboBox1.SelectedIndex+200).ToString());
                        command.Parameters.AddWithValue("@Param3", textBoxQuantity.Text);

                        
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand commandUpdate = new SqlCommand("UPDATE Product SET quantity = quantity - @DusurulecekMiktar WHERE product_id = @UrunId", connection))
                    {
                        commandUpdate.Parameters.AddWithValue("@DusurulecekMiktar", textBoxQuantity.Text);
                        commandUpdate.Parameters.AddWithValue("@UrunId", (comboBox1.SelectedIndex + 200).ToString());//DÜZELTİLECEK KISIM BU 2. COMBOBOX EKLEYİP VERİLERİ EŞİTLEMEN LAZIM CONİ
                        

                        
                        commandUpdate.ExecuteNonQuery();
                    }
                    connection.Close();
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                // Veriyi çeken sorguyu oluşturun




                //tabloyu doldurcaz
                string queryOrder = "SELECT Customer.Customer_id, Customer.cf_name as[Müş. Adı], Customer.cl_name as [Müş. Soyad] , Orders.Order_id as [Sipariş No], Orders.status as Durum, OrderLine.orderQuantity as Adet, Product.p_title as Ürün, Customer.stf_id\r\nFROM Customer\r\nJOIN Orders ON Customer.Customer_id = Orders.cst_id\r\nJOIN OrderLine ON Orders.Order_id = OrderLine.Order_id\r\nJOIN Product ON OrderLine.product_id = Product.product_id ;";
                SqlDataAdapter adapterTb2 = new SqlDataAdapter(queryOrder, connectionnew);
                DataTable dataTable2 = new DataTable();
                adapterTb2.Fill(dataTable2);
                dataGridViewOrders.DataSource = dataTable2;
                dataGridViewOrders.Columns["Customer_id"].Visible = false;
                dataGridViewOrders.Columns["stf_id"].Visible = false;


            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
