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
  public class BD_Personal : Cls_Conexion
    {
        public void BD_RegistrarPersonal(EN_Persona per)
        {
            MySqlConnection cn = new MySqlConnection();
            
            
            try
            {
                cn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("SP_Registrar_Personal", cn);
                
                cmd.CommandTimeout = 20;     
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("_Id_Person", per.Idpersonal); 
                cmd.Parameters.AddWithValue("_dni", per.Dni);
                cmd.Parameters.AddWithValue("_nombreComplto",per.Nombres); 
                cmd.Parameters.AddWithValue("_FechaNacmnto",per.FechaNaci); 
                cmd.Parameters.AddWithValue("_Sexo", per.Sexo);
                cmd.Parameters.AddWithValue("_Domicilio", per.Direccion); 
                cmd.Parameters.AddWithValue("_Correo", per.Correo);
                cmd.Parameters.AddWithValue("_Celular", per.Celular);
                cmd.Parameters.AddWithValue("_Id_rol", per.IdRol);
                cmd.Parameters.AddWithValue("_Foto", per.xImagen);
                cmd.Parameters.AddWithValue("_Id_Distrito", per.IdDistrito);

                cn.Open();
                cmd.ExecuteNonQuery();  
                cn.Close(); 



            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo leer " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           

        }

        public void BD_ActualizarPersonal(EN_Persona per)
        {
            MySqlConnection cn = new MySqlConnection();


            try
            {
                cn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("Sp_Actualizar_Personal", cn);

                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("_Id_Person", per.Idpersonal);
                cmd.Parameters.AddWithValue("_dni", per.Dni);
                cmd.Parameters.AddWithValue("_nombreComplto", per.Nombres);
                cmd.Parameters.AddWithValue("_FechaNacmnto", per.FechaNaci);
                cmd.Parameters.AddWithValue("_Sexo", per.Sexo);
                cmd.Parameters.AddWithValue("_Domicilio", per.Direccion);
                cmd.Parameters.AddWithValue("_Correo", per.Correo);
                cmd.Parameters.AddWithValue("_Celular", per.Celular);
                cmd.Parameters.AddWithValue("_Id_rol", per.IdRol);
                cmd.Parameters.AddWithValue("_Foto", per.xImagen);
                cmd.Parameters.AddWithValue("_Id_Distrito", per.IdDistrito);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();



            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo leer " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }

        public static bool xhuella = false;
        public void Registra_HuellaPersonal(string idper, object huella)
        {
            MySqlConnection cn = new MySqlConnection();


            try
            {
                cn.ConnectionString = Conectar();
                MySqlCommand cmd = new MySqlCommand("Sp_registrarHuella", cn);

                cmd.CommandTimeout = 20;
                cmd.CommandType = CommandType.StoredProcedure;


                cmd.Parameters.AddWithValue("xidper", idper);
                cmd.Parameters.AddWithValue("xhuellaper", huella);
                

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                xhuella = true;

            }
            catch (Exception ex)
            {
                xhuella=false;
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo leer " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


        }
        public DataTable BD_Lista_Todo_personal()
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Listar_Todo_Personal", cn); 
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dato = new DataTable(); 
                da.Fill(dato); 
                return dato;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + ex.Message, "Listar todo personal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }

        public DataTable BD_Buscar_personal_porValor(string valor)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Buscar_Personal_porValor", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("_valor", valor);

                DataTable dato = new DataTable();
                da.Fill(dato);
                return dato;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + ex.Message, "Listar todo personal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }
        public static bool eliminado = false;   
        public void BD_EliminarPersonal(string idper)
        {
            MySqlConnection cn =new MySqlConnection(Conectar());
            try
            {
                MySqlCommand cmd = new MySqlCommand("Sp_EliminarPersonal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idper", idper);

                cn.Open();  
                cmd.ExecuteNonQuery();
                cn.Close();

                eliminado = true;   

            }
            catch (Exception ex)
            {
                eliminado=false;

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo eliminar " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void BD_DarDeBajaPersonal(string idper)
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            try
            {
                MySqlCommand cmd = new MySqlCommand("Sp_Dardebaja_personal", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("_idper", idper);

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

                eliminado = true;

            }
            catch (Exception ex)
            {
                eliminado = false;

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                MessageBox.Show("No se pudo eliminar " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        public DataTable BD_Listar_Personal_deBaja()
        {
            MySqlConnection cn = new MySqlConnection(Conectar());
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("Sp_Listar_PersonalDeBaja", cn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
               // da.SelectCommand.Parameters.AddWithValue("_valor", valor);

                DataTable dato = new DataTable();
                da.Fill(dato);
                return dato;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + ex.Message, "Listar todo personal", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }
    }
}
