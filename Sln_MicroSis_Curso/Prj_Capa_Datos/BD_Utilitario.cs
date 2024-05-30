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
     public class BD_Utilitario : Cls_Conexion 
    {

     
        public static string BD_NroDoc(int idtipo)
        {

            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = Conectar2();
                MySqlCommand cmd = new MySqlCommand("sp_lista_tipo", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_id_tipo", idtipo);
                string nroDoc = "";

                cn.Open();
                nroDoc = Convert.ToString(cmd.ExecuteScalar());
                return nroDoc;
            }
            catch (Exception ex)
            {
                if(cn.State != ConnectionState.Open)
                {
                    cn.Clone();
                }
                MessageBox.Show("No se pudo leer " + ex.Message, "advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;
        }

      public static void BD_ActualizarNumero(int idtipo, string numero)
        {
            MySqlConnection cn = new MySqlConnection(Conectar2());
            MySqlCommand cmd = new MySqlCommand("sp_Actualiza_Tipo_Doc", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("_idtipo", idtipo);
                cmd.Parameters.AddWithValue("_numero", numero);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
            catch (Exception ex)
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Clone();
                }
                MessageBox.Show("No se pudo leer " + ex.Message, "advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

      public static string BD_leerNumero(int idtipo)
        {


            MySqlConnection cn = new MySqlConnection();

            try
            {
                cn.ConnectionString = Conectar2();
                MySqlCommand cmd = new MySqlCommand("sp_listar_solo_numero", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idtipo", idtipo);

                string numero;
                cn.Open();
                numero = Convert.ToString( cmd.ExecuteScalar());

                return numero;

            }
            catch (Exception ex)
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo leer " + ex.Message, "Advertencia (Leer solo numero)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;
        }
        public static string BD_listar_TipoRobot(int idtipo)
        {
            MySqlConnection cn = new MySqlConnection();

            try
            {
                cn.ConnectionString = Conectar2();
                MySqlCommand cmd = new MySqlCommand("sp_listarRobot", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idtipo", idtipo);

                string tipoRobot = "";

                cn.Open();
                tipoRobot = Convert.ToString( cmd.ExecuteScalar()); 
                cn.Close();
                return tipoRobot;

            }catch (Exception ex)
            {

                if (cn.State != ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo leer " + ex.Message, "Advertencia (Leer solo numero)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;    
            }
        }

        public static bool falta = false;
        public void BD_Actualizar_TipoRobot(int IdTipo, string serie)
        {
            MySqlConnection cn = new MySqlConnection();

            
            try
            {
                cn.ConnectionString = Conectar2();
                MySqlCommand cmd = new MySqlCommand("sp_editarRobot", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idtipo", IdTipo);
                cmd.Parameters.AddWithValue("_tipoRobot", serie);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                falta = true;   
            }
            catch (Exception ex)
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo actualizar " + ex.Message, "Advertencia (Actualizar)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            }

        }
    }
}
