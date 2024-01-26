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

namespace FabrikaVT
{
    public partial class CustomerAdd : Form
    {
        public string Persname;
        public string Perslastname;
        public int staffId;
        public void SetStaffId(int id)
        {
            staffId = id;
        }
        private ConnectionHelper baglantiObj = new ConnectionHelper("Data Source=localhost;Initial Catalog=FabrikaDB;Integrated Security=True;");
        public CustomerAdd()
        {
            InitializeComponent();
        }

        private void CustomerAdd_Load(object sender, EventArgs e)
        {
            // Bağlantı nesnesini oluşturun ve bağlantıyı açın
            SqlConnection connection = baglantiObj.CreateConnection();
            baglantiObj.OpenConnection(connection);

            // Veriyi çeken sorguyu oluşturun
            //string query = "SELECT f_name as Ad,l_name as Soyad,staff_id as id,email,adress,s_birthday as DogumTarihi,s_phone as TelefonNo FROM Staff";
            string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id;";

            // SqlDataAdapter ve DataTable kullanarak veriyi çekin
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            // DataGridView'e veriyi yükleyin
            dataGridView_staffAdd.DataSource = dataTable;

            // Bağlantıyı kapatın
            baglantiObj.CloseConnection(connection);
            dataGridView_staffAdd.Columns["c_id"].Visible = false;

            //MessageBox.Show(staffId.ToString(), "Staff ID");
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
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                // Veriyi çeken sorguyu oluşturun
                string query = "SELECT Customer_id as [c_id], cf_name as [Müş. Adı], cl_name as [Müş. Soyad], c_adress as Adres, Staff.f_name as [Pers. Adı], Staff.l_name as [Pers. Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id where Staff.staff_id="+staffId+";";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffAdd.DataSource = dataTable;
                dataGridView_staffAdd.Columns["c_id"].Visible = false;

                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connectionnew);
            }
            else
            {
                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                // Veriyi çeken sorguyu oluşturun
                string query = " SELECT Customer_id as [c_id], cf_name as [Müş.Adı], cl_name as [Müş.Soyad], c_adress as Adres, Staff.f_name as [Pers.Adı], Staff.l_name as [Pers.Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id; ";

                // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                // DataGridView'e veriyi yükleyin
                dataGridView_staffAdd.DataSource = dataTable;
                dataGridView_staffAdd.Columns["c_id"].Visible = false;

                // Bağlantıyı kapatın
                baglantiObj.CloseConnection(connectionnew);
            }
        }

        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (TextBoxesAreNotEmpty())
            {
                dataGridView_staffAdd.DataSource = null;

                dataGridView_staffAdd.Rows.Clear();
                using (SqlConnection connection = new SqlConnection(baglantiObj.getConnection()))
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Customer(cf_name,cl_name,c_adress,stf_id) VALUES (@Param1, @Param2, @Param3, @Param4)", connection))
                    {
                        command.Parameters.AddWithValue("@Param1", textBox_Ad.Text.First().ToString().ToUpper() + textBox_Ad.Text.Substring(1).ToLower());
                        command.Parameters.AddWithValue("@Param2", textBox_Soyad.Text.ToUpper());
                        command.Parameters.AddWithValue("@Param3", textBox_Adres.Text);
                        command.Parameters.AddWithValue("@Param4", staffId);
                        
                        

                     
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                ////PopulateDataGridView();
                // Bağlantı nesnesini oluşturun ve bağlantıyı açın
                SqlConnection connectionnew = baglantiObj.CreateConnection();
                baglantiObj.OpenConnection(connectionnew);

                // Veriyi çeken sorguyu oluşturun
                if (checkBox1.Checked)
                {
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
                }
                else
                {
                    string query = " SELECT Customer_id as [c_id], cf_name as [Müş.Adı], cl_name as [Müş.Soyad], c_adress as Adres, Staff.f_name as [Pers.Adı], Staff.l_name as [Pers.Soyad] FROM Customer INNER JOIN Staff ON Staff.staff_id = Customer.stf_id; ";
                    // SqlDataAdapter ve DataTable kullanarak veriyi çekin
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connectionnew);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // DataGridView'e veriyi yükleyin
                    dataGridView_staffAdd.DataSource = dataTable;
                    dataGridView_staffAdd.Columns["c_id"].Visible = false;

                    // Bağlantıyı kapatın
                    baglantiObj.CloseConnection(connectionnew);
                }
               
                
            }
            else
            {
                MessageBox.Show("Lütfen tüm bilgileri doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}