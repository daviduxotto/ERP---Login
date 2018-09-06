using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace erp.Models
{
    public class usuario :conexionBD
    {
        // --- Atributos
        [DisplayName("Usuario")]
        [StringLength(15)]
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string codUsuario { get; set; }

        [DisplayName("Password")]
        [StringLength(15)]
        [Required(ErrorMessage = "El password es obligatorio")]
        public string  clave { get; set; }

        // --- Metodos
        public salida loginUsuario(usuario parusuario)
        {
            salida varSalida = new salida();                                
            try
             {
                abrirBD();            
                SqlCommand comando = new SqlCommand("login_user", conexion);
                comando.CommandType = CommandType.StoredProcedure;
                // Parametros
                SqlParameter prmUsuario = comando.Parameters.Add("parusuario", SqlDbType.VarChar, 15);
                prmUsuario.Value = parusuario.codUsuario;
                SqlParameter prmClave = comando.Parameters.Add("parclave", SqlDbType.VarChar, 15);
                prmClave.Value = parusuario.clave;
                comando.Parameters.Add(new SqlParameter("parretorno", SqlDbType.Int));
                comando.Parameters["parretorno"].Direction = ParameterDirection.Output;
                comando.Parameters.Add(new SqlParameter("parmensaje", SqlDbType.VarChar, 150));
                comando.Parameters["parmensaje"].Direction = ParameterDirection.Output;
                comando.Parameters.Add(new SqlParameter("partoken", SqlDbType.VarChar, 64));
                comando.Parameters["partoken"].Direction = ParameterDirection.Output;
                // ejecuto
                comando.ExecuteScalar();
                // salida
                varSalida.retorno = Convert.ToInt32(comando.Parameters["parretorno"].Value.ToString());
                varSalida.mensaje = comando.Parameters["parmensaje"].Value.ToString();
                varSalida.token = comando.Parameters["partoken"].Value.ToString();
            }
             catch (Exception ex)
             {
                varSalida.retorno = Convert.ToInt32(ex.HResult);            
                varSalida.mensaje = "Datos " + ex.Message.ToString();                
                varSalida.token = "";
                return varSalida;
             }
             finally
                {
                    cerrarBD();                  
                }
            return varSalida;
        }                       
    }
}

