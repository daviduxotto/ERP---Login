using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace erp.Models
{
    public class conexionBD
    {
        public SqlConnection conexion = new SqlConnection();
        public bool abrirBD()
        {
            try
            {
                conexion.ConnectionString = ConfigurationManager.ConnectionStrings["cnx"].ToString();
                conexion.Open();
                return true;
            }
            catch { return false; }
        }

        public bool cerrarBD()
        {
            try
            {
                conexion.Close();
                return true;
            }
            catch { return false; }
        }
    }
}