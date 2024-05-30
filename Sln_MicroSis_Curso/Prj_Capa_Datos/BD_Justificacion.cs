using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_Capa_Entidad;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Prj_Capa_Datos
{
    public class BD_Justificacion :Cls_Conexion
    {
        public static bool guardo = false;  
        public void Bd_registrar_Justificacion(EN_Justificacion jus)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_registrar_justificacion", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                //los parametros:
                cmd.Parameters.AddWithValue("_Id_Justi", jus.IdJusti);
                cmd.Parameters.AddWithValue("_Id_Personal", jus.Id_Personal);
                cmd.Parameters.AddWithValue("_Principalmoti", jus.PrincipalMotivo);
                cmd.Parameters.AddWithValue("_Detalle", jus.Detalle);
                cmd.Parameters.AddWithValue("_FechaJusti", jus.Fecha);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                guardo = true;


            }catch(Exception ex)
            {
                guardo = false;
                ; if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); MessageBox.Show("Algo malo pasó: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public DataTable BD_Cargar_todos_justificaciones()
        {

            MySqlConnection xcn = new MySqlConnection();
            try
            { 
                xcn.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Listar_Justificaciones", xcn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable Dato = new DataTable();
                da.Fill(Dato);
                da = null;
                return Dato;

            }
            catch (Exception ex)
            {
                ; if (xcn.State == ConnectionState.Open)
                {
                    xcn.Close(); MessageBox.Show("Algo malo pasó: " + ex.Message, "Advertencia Cargar justificaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
            return null;

        }

        public DataTable BD_BuscarJustificacion_porValor(string xdato)
        {

            MySqlConnection cn = new MySqlConnection();
            try
            {
                cn.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Buscar_Justificaciones", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_valor", xdato);
                DataTable Dato = new DataTable();
                da.Fill(Dato);
                da = null;
                return Dato;
            }
            catch (Exception ex)
            {
                    if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); MessageBox.Show("Algo malo pasó: " + ex.Message, "Advertencia Cargar justificaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;


        }

        public static bool editado = false;
        public void Bd_Editar_Justificacion(EN_Justificacion jus)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_Editar_justificacion", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                //los parametros:
                cmd.Parameters.AddWithValue("_Id_Justi", jus.IdJusti);            
                cmd.Parameters.AddWithValue("_Principalmoti", jus.PrincipalMotivo);
                cmd.Parameters.AddWithValue("_Detalle", jus.Detalle);
                cmd.Parameters.AddWithValue("_FechaJusti", jus.Fecha);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                editado = true;


            }
            catch (Exception ex)
            {
                editado = false;
                ; if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); MessageBox.Show("Algo malo pasó: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void Bd_Eliminar_Justificacion(string idJusti)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("Sp_EliminarJustificacion", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                //los parametros:
                cmd.Parameters.AddWithValue("_idjusti", idJusti);
      
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                editado = true;


            }
            catch (Exception ex)
            {
                editado = false;
                ; if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); MessageBox.Show("Algo malo pasó: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void Bd_Abrobar_Desaprobar_Justificacion(string idJusti, string estadoJus)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_aprobarJustificacion", cn);
            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                //los parametros:
                cmd.Parameters.AddWithValue("_idjusti", idJusti);
                cmd.Parameters.AddWithValue("_estadoJusti", estadoJus);

                

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                editado = true;


            }
            catch (Exception ex)
            {
                editado = false;
                ; if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); MessageBox.Show("Algo malo pasó: " + ex.Message, "Advertencia de Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public bool BD_verificar_siPersonal_TieneJustificacion(string idper)
        {
            bool retorno = false;
            Int32 resultado = 0;

            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_verficar_Justificacion_aprobada", cn);
            cmd.CommandType  = CommandType.StoredProcedure;
            cmd.CommandTimeout = 20;

            cmd.Parameters.AddWithValue("_Id_Personal", idper);


            try
            {
                cn.Open();
                resultado = Convert.ToInt32(cmd.ExecuteScalar());
                if (resultado > 0)
                {
                    retorno = true;
                }
                else
                {
                    retorno = false;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                cn.Close();
            }
            catch (Exception ex)
            {
               
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retorno;
        }


    }
}
