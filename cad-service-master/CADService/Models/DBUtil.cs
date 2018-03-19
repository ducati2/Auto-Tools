using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CADService.Models
{
    public class DBUtil
    {
        public static int executeCommand(string sqlStr)
        {
            SqlConnection sqlConn = new SqlConnection("Data Source=10.150.143.83;Initial Catalog=cse_auto_debugger;uid=cad_admin; pwd=njlcm@2017");
            sqlConn.Open();  
            SqlCommand sqlCmd = new SqlCommand(sqlStr, sqlConn); 
            int num = sqlCmd.ExecuteNonQuery();
            sqlCmd.Clone();
            sqlConn.Close();
            return num;
        }
    }
}