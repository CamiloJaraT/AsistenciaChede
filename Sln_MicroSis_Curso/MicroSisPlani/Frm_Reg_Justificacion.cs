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
using Prj_Capa_Entidad;
using Prj_Capa_Negocio;
using MicroSisPlani.Msm_Forms;

namespace MicroSisPlani
{
    public partial class Frm_Reg_Justificacion : Form
    {
        public Frm_Reg_Justificacion()
        {
            InitializeComponent();
        }

        private void Frm_Reg_Justificacion_Load(object sender, EventArgs e)
        {
           // txt_idjusti.Text = RN_Utilitario.RN_NroDoc(3);
        }

       
        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            Utilitarios ui = new Utilitarios();
            if (e.Button == MouseButtons.Left)
            {
                ui.Mover_formulario(this);
            }

        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Tag = "";
            this.Close();
        }

        private bool ValidarCampos()
        {
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Filtro frm_Filtro = new Frm_Filtro();

            if (txt_IdPersona.Text.Trim().Length <2)
            {
                frm_Filtro.Show(); 
                frm_Advertencia.Lbl_Msm1.Text = "Falta el ID del Personal";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                txt_IdPersona.Focus();
                return false;

            }
            if (cbo_motivJusti.SelectedIndex == -1)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "Seleccione un motivo para la justificacion";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                cbo_motivJusti.Focus();
                return false;

            }
            if (txt_DetalleJusti.Text.Trim().Length < 2)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "Agrega una descripcion para la justificacion";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                txt_DetalleJusti.Focus();
                return false;

            }
            return true;    
        }
        private void Registrar_Justificacion()
        {
            RN_Justificacion obj = new RN_Justificacion();
            EN_Justificacion jus = new EN_Justificacion();

            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Msm_Bueno frm_Msm_Bueno = new Frm_Msm_Bueno();

            try
            {
                jus.IdJusti = txt_idjusti.Text;
                jus.Id_Personal = txt_IdPersona.Text;
                jus.PrincipalMotivo = cbo_motivJusti.Text;
                jus.Detalle = txt_DetalleJusti.Text; 
                jus.Fecha = Dtp_FechaJusti.Value;

                obj.RN_registrar_Justificacion(jus);

                if (BD_Justificacion.guardo == true)
                {
                    Actualizar_SiguienteNumero(3);
                    frm_Filtro.Show();
                    frm_Msm_Bueno.Lbl_msm1.Text = "La solicitud de justificacion fue registrada exitosamente, pendiente de aprobacion";
                    frm_Msm_Bueno.ShowDialog();
                    frm_Filtro.Hide();
                    this.Tag = "A";
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                frm_Filtro.Show();
                MessageBox.Show("Error " + ex.Message, "Guardar justificacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                frm_Filtro.Hide();
            }
        }

        private void Editar_Justificacion()
        {
            RN_Justificacion obj = new RN_Justificacion();
            EN_Justificacion jus = new EN_Justificacion();

            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Msm_Bueno frm_Msm_Bueno = new Frm_Msm_Bueno();

            try
            {
                jus.IdJusti = txt_idjusti.Text;
                jus.PrincipalMotivo = cbo_motivJusti.Text;
                jus.Detalle = txt_DetalleJusti.Text;
                jus.Fecha = Dtp_FechaJusti.Value;

                obj.RN_Editar_Justificacion(jus);

                if (BD_Justificacion.editado == true)
                {
                
                    frm_Filtro.Show();
                    frm_Msm_Bueno.Lbl_msm1.Text = "La solicitud de justificacion fue actualizada exitosamente, pendiente de aprobacion";
                    frm_Msm_Bueno.ShowDialog();
                    frm_Filtro.Hide();
                    this.Tag = "A";
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                frm_Filtro.Show();
                MessageBox.Show("Error " + ex.Message, "Guardar justificacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                frm_Filtro.Hide();
            }
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
            int td = xnuevonum.Length;
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

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos()==true)
            {
                if (editar == true)
                {
                    Editar_Justificacion();
                }
                else
                {
                    Registrar_Justificacion();
                }
                
            }
        }

        bool editar = false;
        public void Buscar_Justificacion_paraEditar(string idjusti)
        {
            try
            {
                 RN_Justificacion obj = new RN_Justificacion();  
                DataTable dt = new DataTable();

                dt = obj.RN_BuscarJustificacion_porValor(idjusti);

                if (dt.Rows.Count > 0)
                {
                    txt_idjusti.Text = Convert.ToString(dt.Rows[0]["Id_Justi"]);
                    txt_IdPersona.Text = Convert.ToString(dt.Rows[0]["Id_Persl"]);
                    txt_nompersona.Text = Convert.ToString(dt.Rows[0]["Nombre_Completo"]);
                    cbo_motivJusti.Text = Convert.ToString(dt.Rows[0]["PrincipalMotivo"]);
                    txt_DetalleJusti.Text = Convert.ToString(dt.Rows[0]["Detalle_Justi"]);
                    Dtp_FechaJusti.Value = Convert.ToDateTime(dt.Rows[0]["FechaJusti"]);
                    editar = true;
                    btn_aceptar.Enabled = editar;
                }
            }
            catch (Exception ex)
            {
              
                MessageBox.Show("Error al buscar datos " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
           
            


        }
    }
}
