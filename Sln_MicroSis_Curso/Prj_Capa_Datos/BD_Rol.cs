using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

 


namespace Prj_Capa_Datos
{
    public class BD_Rol : Cls_Conexion
    {

      public DataTable BD_listarRoles()
        {

            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Listar_Todos_roles", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dato = new DataTable();
                da.Fill(dato);
                return dato;

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo leer " + ex.Message, "advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;
        }

        }     

    
}
