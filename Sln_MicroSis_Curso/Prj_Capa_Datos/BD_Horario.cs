using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using Prj_Capa_Entidad;
using System.Windows.Forms;

namespace Prj_Capa_Datos
{
    public class BD_Horario : Cls_Conexion
    {
        public static bool gauardado;
       public void BD_actualizarHorario(EN_Horario p)
        {

            MySqlConnection cn = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                cn.ConnectionString = Conectar();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_actualizar_horario";
                cmd.Connection = cn;
                cmd.CommandTimeout = 20;


                cmd.Parameters.AddWithValue("xId_Hor",p.Idhora);
                cmd.Parameters.AddWithValue("xHoEntrada",p.HoEntrada);
                cmd.Parameters.AddWithValue("xMiTolrncia",p.HoTole);
                cmd.Parameters.AddWithValue("xHoLimite",p.HoLimite);
                cmd.Parameters.AddWithValue("xHoSalida",p.HoSalida);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                cmd.Dispose();
                cmd = null;
                gauardado = true;

            }
            catch (Exception ex)
            {

                gauardado = false;
                MessageBox.Show("Error al actualizar horario" + ex.Message, "informe sistema" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }


         

        }

           public DataTable BD_Leer_Horario()
           {

            MySqlConnection cn = new MySqlConnection();

           try
           {
                cn.ConnectionString = Conectar();
                MySqlDataAdapter da = new MySqlDataAdapter("sp_listarHorario", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                da.Fill(dt);
                da = null;
                return dt;  


           }
             catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar horario" + ex.Message, "informe sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                return null;

            }

              }




    }
}
