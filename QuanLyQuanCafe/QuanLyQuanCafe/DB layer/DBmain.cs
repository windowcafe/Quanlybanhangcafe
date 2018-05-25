using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCafe.DB_layer
{
    class DBmain
    {
        string ConnStr = "Data Source=(local);Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
        SqlConnection conn = null;
        SqlCommand comm = null;
        SqlDataAdapter da = null;
        public DBmain()
        {
            conn = new SqlConnection(ConnStr);
            comm = conn.CreateCommand();
        }
        public DataSet ExecuteQueryDataSet(String strSQL,CommandType ct)
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.Open();
            comm.CommandText = strSQL;
            comm.CommandType = ct;
            da = new SqlDataAdapter(comm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }

}
