
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Negocio;
using Prj_Capa_Datos;
using Prj_Capa_Entidad;
using System.IO;




namespace MicroSisPlani.Personal
{
    public partial class Frm_Edit_Personal : Form
    {
        public Frm_Edit_Personal()
        {
            InitializeComponent();
        }

           
      
        private void Frm_Edit_Personal_Load(object sender, EventArgs e)
        {
            
            Listar_roles();
            Listar_Distritos();
            Buscar_PersonalEdicion(this.Tag.ToString());
          
        }

         private void Listar_roles (){
        
            RN_Rol obj = new RN_Rol();
            DataTable data = new DataTable();

            data = obj.RN_listarRoles();
            if (data.Rows.Count > 0 )
            {
                var cbo = cbo_rol;

                cbo.DataSource = data;
                cbo.DisplayMember = "NomRol";
                cbo.ValueMember = "Id_Rol";
                cbo.SelectedIndex = -1;
            }
        
        
        
        }
         private void Listar_Distritos()
        {
            RN_Distrito obj = new RN_Distrito();    
            DataTable data = new DataTable();

            data = obj.RN_liistarDistrtos();

            if (data.Rows.Count > 0)
            {
                var cbo = cbo_Distrito;

                cbo.DataSource = data;
                cbo.DisplayMember = "Distrito";
                cbo.ValueMember = "Id_Distrito";
                cbo.SelectedIndex = -1;

            }



        }
        private void Buscar_PersonalEdicion(string idper)
        {
            RN_Personal obj = new RN_Personal();
            DataTable data = new DataTable();
            string sex = "";

            data = obj.RN_Buscar_personal_porValor(idper);

            if (data.Rows.Count > 0)
            {
                txt_IdPersona.Text = Convert.ToString(data.Rows[0]["Id_Persl"]);
                txt_Dni.Text = Convert.ToString(data.Rows[0]["DNIPR"]);
                txt_nombres.Text = Convert.ToString(data.Rows[0]["Nombre_Completo"]);
                txt_direccion.Text = Convert.ToString(data.Rows[0]["Domicilio"]);
                txt_correo.Text = Convert.ToString(data.Rows[0]["Correo"]);
                txt_NroCelular.Text = Convert.ToString(data.Rows[0]["Celular"]);
                dtp_fechaNaci.Value = Convert.ToDateTime(data.Rows[0]["Fec_Naci"]);
                

                sex = Convert.ToString(data.Rows[0]["Sexo"]);
                if (sex == "M")
                {
                    cbo_sexo.SelectedIndex = 0;
                }
                else if (sex == "F")
                {
                    cbo_sexo.SelectedIndex = 1;
                }

                cbo_rol.SelectedValue = data.Rows[0]["Id_rol"]; 
                cbo_Distrito.SelectedValue = data.Rows[0]["Id_Distrito"];
                txt_IdPersona.Text = Convert.ToString(data.Rows[0]["Id_Persl"]);

                xfoto = Convert.ToString(data.Rows[0]["Foto"]);
                if (File.Exists(xfoto) == false)
                {
                    xfoto = Application.StartupPath + @"\user.png";
                    Pic_persona.Load(Application.StartupPath + @"\user.png");
                }
                else
                {
                    Pic_persona.Load(xfoto);
                }
            }

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        string xfoto = "";
        private void Registrar_Personal()
        {

            RN_Personal obj = new RN_Personal();
            EN_Persona per = new EN_Persona();

            try
            {

                per.Idpersonal = txt_IdPersona.Text;
                per.Dni = txt_Dni.Text;
                per.Nombres = txt_nombres.Text;
                per.FechaNaci = dtp_fechaNaci.Value;
                if (cbo_sexo.SelectedIndex == 0)
                {
                    per.Sexo = "M";
                }
                else if (cbo_sexo.SelectedIndex == 1)
                {
                    per.Sexo = "F";
                }
                per.Direccion = txt_direccion.Text;
                per.Correo = txt_correo.Text;
                per.Celular = Convert.ToInt32(txt_NroCelular.Text);
                per.IdRol = cbo_rol.SelectedValue.ToString();
                per.xImagen = xfoto;
                per.IdDistrito = cbo_Distrito.SelectedValue.ToString();

                obj.RN_ActualizarPersonal(per);

                

                MessageBox.Show("Datos actualizados exitosamente");
                this.Tag = "A";
                this.Close();   

            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo leer " + ex.Message, "advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Pic_persona_Click(object sender, EventArgs e)
        {
            var filepath = string.Empty;

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    xfoto = openFileDialog1.FileName;
                    Pic_persona.Load(xfoto);

                }
                else
                {
                    xfoto = Application.StartupPath + @"\user.png";
                    Pic_persona.Load(Application.StartupPath + @"\user.png");
                }

            }
            catch (Exception ex)
            {

                xfoto = Application.StartupPath + @"\user.png";
                Pic_persona.Load(Application.StartupPath + @"\user.png");
            }

        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            Registrar_Personal();
        }

        private double GenerarNextId(string numero)
        {
            double newnum = Convert.ToDouble(numero) + 1;
            return newnum;
        }
        private void Actualizar_SiguienteNumero(int idtipo)
        {
            string xnum = BD_Utilitario.BD_leerNumero(idtipo);
            string xnuevonum = Convert.ToString(GenerarNextId(xnum));
            int td  = xnuevonum.Length;
            string nuevoCorrelativo = "";

            if (xnuevonum.Length < 5)
            {
                if (td == 1)
                {
                    nuevoCorrelativo = "0000" + xnuevonum;
                }
                if (td == 2)
                {
                    nuevoCorrelativo = "000" + xnuevonum;
                }
                if (td == 3)
                {
                    nuevoCorrelativo = "00" + xnuevonum;
                }
                if (td == 4)
                {
                    nuevoCorrelativo = "0" + xnuevonum;
                }
                BD_Utilitario.BD_ActualizarNumero(idtipo, nuevoCorrelativo);
            }

        }
    }
}
