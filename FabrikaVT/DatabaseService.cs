using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data;
using System.Data.SqlClient;
namespace FabrikaVT
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void InsertData(string ad, string soyad, string email, string adres)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("INSERT INTO YourTableName (Column1, Column2, Column3, Column4) VALUES (@Param1, @Param2, @Param3, @Param4)", connection))
                {
                    command.Parameters.AddWithValue("@Param1", ad);
                    command.Parameters.AddWithValue("@Param2", soyad);
                    command.Parameters.AddWithValue("@Param3", email);
                    command.Parameters.AddWithValue("@Param4", adres);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetData()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM YourTableName", connection))
                {
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }
    }
}