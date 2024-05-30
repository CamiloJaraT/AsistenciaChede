using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Entidad;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;


namespace Prj_Capa_Datos
{
    public class BD_Asistencia : Cls_Conexion
    {


        public static bool entrada = false;

        public void BD_registar_entrada (EN_Asistencia asi)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_registrar_entrada", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;
                string hr = DateTime.Now.ToString("HH:mm:ss");
               asi.HoraIngre = hr;
                cmd.Parameters.AddWithValue("xId_Asis", asi.IdAsistencia);
                cmd.Parameters.AddWithValue("xId_Persl", asi.Id_Personal);
                cmd.Parameters.AddWithValue("xNombre_dia", asi.Nombre_Dia);
                cmd.Parameters.AddWithValue("xHoIngreso", DateTime.Now.ToString("HH:mm:ss"));
                cmd.Parameters.AddWithValue("xjustificacion", asi.Justificacion);
                cmd.Parameters.AddWithValue("xTardanzas", asi.Tardanza);
                
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                entrada = true;
            }
            catch (Exception ex)
            {
                entrada = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        
            

      }
        public bool BD_verificar_si_marco_aistencia(string idper)
        {
            bool retorno = false;
            Int32 resultado = 0;

            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_verificar_si_marco_aistencia", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 20;

            cmd.Parameters.AddWithValue("xidpers", idper);


            try
            {
                cn.Open();
                resultado = Convert.ToInt32(cmd.ExecuteScalar());
                if (resultado > 0)
                {
                    retorno = true;
                }
                else { 
                    retorno=false;
                }
                cmd.Parameters.Clear();
                cmd.Dispose();
                cn.Close();
            }
            catch (Exception ex)
            {
                entrada = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retorno; 
        }

        public bool BD_verificar_si_marco_falta(string idper)
        {
            bool retorno = false;
            Int32 resultado = 0;

            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_verificar_si_marco_Falta", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 20;

            cmd.Parameters.AddWithValue("xidpers", idper);


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
                entrada = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retorno;
        }

        public bool BD_verificar_si_marco_entrada(string idper)
        {
            bool retorno = false;
            Int32 resultado = 0;

            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_verificar_si_entrada", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 20;

            cmd.Parameters.AddWithValue("xidpers", idper);


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
                entrada = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return retorno;
        }

        public void BD_registar_falta(string idAsis, string idper, string justi, string nomdia)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_registrar_falta", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("_IdAsis", idAsis);
                cmd.Parameters.AddWithValue("_Id_Personal", idper);
                cmd.Parameters.AddWithValue("_nomdia", nomdia);              
                cmd.Parameters.AddWithValue("_Justificacion", justi);
             
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                entrada = true;
            }
            catch (Exception ex)
            {
                entrada = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }

        public DataTable BD_Buscar_Asistencia_deEntrada(string idperson)
        { 
        
            MySqlConnection cn = new MySqlConnection(Conectar());
        
           
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Listar_Asistencia_RecienCreada", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("xidper", idperson);
                DataTable dato= new DataTable();
                da.Fill(dato); da = null;
                return dato;
            }
            catch (Exception ex)
            {

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;    
            }

        }

        public static bool salida = false;
        public void BD_registar_salida(EN_Asistencia asi)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_registrarSalida", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("xId_Asis", asi.IdAsistencia);
                cmd.Parameters.AddWithValue("xId_Persl", asi.Id_Personal);       
                cmd.Parameters.AddWithValue("xhoraSalida", asi.HoraSalida);
                cmd.Parameters.AddWithValue("xTotal_trabajadas", asi.TotalHoras);
             

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                salida = true;
            }
            catch (Exception ex)
            {
                salida = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }
       

        public DataTable BD_Listar_Todas_Asistencias()
        {
            MySqlConnection cn =new MySqlConnection();

            try
            {

                cn.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Listar_todas_asistencias", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dato = new DataTable();

                da.Fill(dato);
                da = null;
                return dato;




            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;



        }

        public DataTable BD_Buscar_Asistencias(string xvalor)
        {
            MySqlConnection cn = new MySqlConnection();

            try
            {

                cn.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_buscar_asistencias", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("xvalor", xvalor);
                DataTable dato = new DataTable();

                da.Fill(dato);
                da = null;
                return dato;




            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;



        }

        
        public DataTable BD_Buscar_Dia(DateTime xdia)
        {
            MySqlConnection cn = new MySqlConnection();

            try
            {

                cn.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_Listar_Asistencia_Dia", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_fechaHoy", xdia);
                DataTable dato = new DataTable();

                da.Fill(dato);
                da = null;
                return dato;




            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            return null;



        }
        public void BD_Eliminar_Asistencia(string idasis)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            MySqlCommand cmd = new MySqlCommand("sp_Eliminar_Asitencia", cn);

            try
            {
                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("_idasis", idasis);
                


                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                salida = true;
            }
            catch (Exception ex)
            {
                salida = false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();

                }
                MessageBox.Show("Error  " + ex.Message, "Marcar Entrada", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }



        }
    }
}
