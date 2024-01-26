using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FabrikaVT
{
    public partial class FormCustomerDelete : Form
    {
        public int staffId;
        public void SetStaffId(int id)
        {
            staffId = id;
        }
        public string CustomerID;
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public FormCustomerDelete()
        {
            InitializeComponent();
        }

        private void FormCustomerDelete_Load(object sender, EventArgs e)
        { // Bağlantı nesnesini oluşturun ve bağlantıyı açın
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
            dataGridView_staffdel.DataSource = dataTable;

            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connection);
            dataGridView_staffdel.Columns["c_id"].Visible = false;


            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connection);


            buttonDelete.Image = Image.FromFile(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\ekDosya\\delete.png");
            Image resizedimage = buttonDelete.Image.GetThumbnailImage(32, 32, null, IntPtr.Zero);
            //buttonUpdate.Image.GetThumbnailImage(16, 16, null, IntPtr.Zero);
            buttonDelete.Image = resizedimage;

        }

        private void dataGridView_staffdel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // DatagridView'de bir hücreye tıklandığında satırın rengini değiştirmek için bu olayı kullanabilirsiniz
            if (e.RowIndex >= 0)
            {
                // Tıklanan satırı seçili yap
                dataGridView_staffdel.Rows[e.RowIndex].Selected = true;

                // Tıklanan satırın rengini değiştir (RowPrePaint olayını tetikleyecektir)
                dataGridView_staffdel.InvalidateRow(e.RowIndex);
            }
            CustomerID = dataGridView_staffdel.Rows[e.RowIndex].Cells["c_id"].Value.ToString();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

            dataGridView_staffdel.DataSource = null;

            dataGridView_staffdel.Rows.Clear();
            using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
            {
                using (SqlCommand command = new SqlCommand("DELETE FROM Customer WHERE  Customer_id = @custıd", connection))
                {

                    command.Parameters.AddWithValue("@custıd", CustomerID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            SqlConnection connectionnew = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connectionnew);

            string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where Staff.staff_id=" + staffId + ";";
            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffdel.DataSource = dataTable;
            dataGridView_staffdel.Columns["c_id"].Visible = false;


            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connectionnew);
            MessageBox.Show("Silme başarılı.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            textBox_Ad.Text = "";
            textBox_Soyad.Text = "";
            textBox_Adres.Text = "";

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
                            dataGridView_staffdel.DataSource = dataTable;

                            // DataGridView'deki "id" sütununu gizle
                            dataGridView_staffdel.Columns["c_id"].Visible = false;
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
                dataGridView_staffdel.DataSource = dataTable;

                // DataGridView'deki "id" sütununu gizle
                dataGridView_staffdel.Columns["c_id"].Visible = false;


                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connection);
            }
        }
    }
}  