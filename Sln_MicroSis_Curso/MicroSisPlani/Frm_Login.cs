using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Prj_Capa_Datos;
using Prj_Capa_Negocio;
using MicroSisPlani.Msm_Forms;
namespace MicroSisPlani
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Aceptar_Click(object sender, EventArgs e)
        {
            Verficar_Acceso();
        }
        private bool validarTB()
        {
            Frm_Filtro frm_Filtro  = new Frm_Filtro();
            if (txt_usu.Text.Trim().Length == 0)
            {
                frm_Filtro.Show();
                MessageBox.Show("Ingrese un Usuario", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_usu.Focus();
                frm_Filtro.Hide();
                return false;
            }
            if (txt_pass.Text.Trim().Length == 0)
            {
                frm_Filtro.Show();
                MessageBox.Show("Ingrese su clave", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_pass.Focus();
                frm_Filtro.Hide();
                return false;
            }
            return true;

        }
        private void Verficar_Acceso()
        {

            RN_Usuario obj = new RN_Usuario();
            DataTable dt = new DataTable();

            int nveces = 0;
            if (validarTB()==false)
            {
                return;
            }
            string usu, pass;

            usu = txt_usu.Text;
            pass = txt_pass.Text;

            if (obj.RN_Verificar_Acceso(usu, pass))
            {
                Cls_Libreria.Usuario = usu;
                dt = obj.RN_Leer_Datos_Usuario(usu);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    Cls_Libreria.IdRol = Convert.ToString(dr["Id_Usu"]);   
                    Cls_Libreria.Apellidos = dr["Nombre_Completo"].ToString();
                    Cls_Libreria.IdRol = Convert.ToString(dr["Id_Rol"]);
                    Cls_Libreria.Rol = dr["NomRol"].ToString();
                    Cls_Libreria.Foto = dr["Avatar"].ToString();

                }

                this.Hide();
                Frm_Principal frm_Principal = new Frm_Principal();
                frm_Principal.Show();
                frm_Principal.Cargar_Datos_Usuario();
                frm_Principal.Verificar_RobotFaltas();

            }
            else
            {
                txt_pass.Text = "";
                txt_pass.Text = "";
                MessageBox.Show("Usuario o calve no son correctos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_usu.Focus();
                nveces += 1;
            }
            if (nveces == 3)
            {
                MessageBox.Show("Ha excedido el numero de intentos para ingresaion", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }

        }

        private void txt_usu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_usu.Focus();
            }
        }

        private void txt_pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Aceptar_Click(sender, e);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            Utilitarios utilitarios = new Utilitarios();
            if (e.Button == MouseButtons.Left)
            {
                utilitarios.Mover_formulario(this);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
