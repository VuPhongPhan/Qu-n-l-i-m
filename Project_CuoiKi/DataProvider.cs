using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CuoiKirovider
{
    class DataProvider
    {
        private string connectionSTR = "Data Source=.\\sqlexpress02;Initial Catalog=C#;Integrated Security=True";

        public DataTable ExecQuery(string query)
        {
            DataTable data = new DataTable();

            SqlConnection conn = new SqlConnection(connectionSTR);

            conn.Open();

            SqlCommand command = new SqlCommand(query, conn);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(data);

            conn.Close();

            return data;
        }
    }
}
