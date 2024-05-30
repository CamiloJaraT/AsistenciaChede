using MicroSisPlani.Personal;
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
using System.IO;

namespace MicroSisPlani
{
    public partial class Frm_Principal : Form
    {
        public Frm_Principal()
        {
            InitializeComponent();
        }

        private void Frm_Principal_Load(object sender, EventArgs e)
        {

            Configurar_ListView_Per();
            ConfigurarListView_Justifi();
            P_cargar_Todos_Personal();
            Cargar_todas_Justificaciones();
            Cargar_Horarios();
            Verificar_RobotFaltas();
            configureListView__Asistencia();
            Cargar_TodasAsistencias();
        }

        public void Cargar_Datos_Usuario()
        {
            try
            {
                Frm_Filtro frm_Filtro = new Frm_Filtro();
                MessageBox.Show("Bienvenido Sr(a) " + Cls_Libreria.Apellidos, "Bienvenido al sistema ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frm_Filtro.Hide();
                Lbl_NomUsu.Text = Cls_Libreria.Apellidos;
                lbl_rolNom.Text = Cls_Libreria.Rol;

                if (Cls_Libreria.Foto.Trim().Length == 0 | Cls_Libreria.Foto == null)
                {
                    return;
                }
                if (File.Exists(Cls_Libreria.Foto) == true)
                {
                    pic_user.Load(Cls_Libreria.Foto);
                }
                else
                {
                    pic_user.Image = Properties.Resources.user;
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public void Cargar_Horarios()
        {
            RN_Horario obj = new RN_Horario();
            DataTable dt = new DataTable();
            dt = obj.RN_Leer_Horario();

            if (dt.Rows.Count == 0)
            {
                return;
            }
            lbl_idHorario.Text = Convert.ToString(dt.Rows[0]["Id_Hor"]);
            dtp_horaIngre.Value = Convert.ToDateTime(dt.Rows[0]["HoEntrada"]);
            dtp_horaSalida.Value = Convert.ToDateTime(dt.Rows[0]["HoSalida"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(dt.Rows[0]["MiTolrncia"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(dt.Rows[0]["HoLimite"]);

        }

        public void Verificar_RobotFaltas()
        {

            string tipoRobot = "";
            tipoRobot = RN_Utilitario.RN_listar_TipoRobot(4);

            if (tipoRobot.Trim() == "Si")
            {
                rdb_ActivarRobot.Checked = true;
                pnl_falta.Visible = true;
                timerTempo.Start();
            }
            else if (tipoRobot.Trim() == "No")
            {
                rdb_Desact_Robot.Checked = true;
                timerTempo.Stop();
                timerFalta.Stop();
            }

        }
        public void pnl_titu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btn_mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


        #region Personal

        private void Configurar_ListView_Per()
        {
            var lis = lsv_person;

            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;

            lis.Columns.Add("Id Persona", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Dni", 95, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del Socio", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Direccion", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Correo", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Sex", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Fe Nacim.", 110, HorizontalAlignment.Center);
            lis.Columns.Add("Nro Celular", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Rol", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Distrito", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 100, HorizontalAlignment.Left);

        }

        private void Llenar_ListView(DataTable data)
        {
            lsv_person.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Persl"].ToString());
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["Domicilio"].ToString());
                list.SubItems.Add(dr["Correo"].ToString());
                list.SubItems.Add(dr["Sexo"].ToString());
                list.SubItems.Add(dr["Fec_Naci"].ToString());
                list.SubItems.Add(dr["Celular"].ToString());
                list.SubItems.Add(dr["NomRol"].ToString());

                list.SubItems.Add(dr["Distrito"].ToString());
                list.SubItems.Add(dr["Estado_Per"].ToString());
                lsv_person.Items.Add(list);
            }


            Lbl_total.Text = Convert.ToString(lsv_person.Items.Count);
        }


        private void P_cargar_Todos_Personal()
        {

            RN_Personal obj = new RN_Personal();
            DataTable data = new DataTable();
            data = obj.RN_Lista_Todo_personal();

            if (data.Rows.Count > 0)
            {
                Llenar_ListView(data);
            }
            else
            {
                lsv_person.Items.Clear();
            }
        }
        private void P_cargar_Todos_PersonaldeBaja()
        {

            RN_Personal obj = new RN_Personal();
            DataTable data = new DataTable();
            data = obj.RN_Listar_Personal_deBaja();

            if (data.Rows.Count > 0)
            {
                Llenar_ListView(data);
            }
            else
            {
                lsv_person.Items.Clear();
            }
        }

        private void P_Buscar_Personal_porValor(string valor)
        {

            RN_Personal obj = new RN_Personal();
            DataTable data = new DataTable();
            data = obj.RN_Buscar_personal_porValor(valor);

            if (data.Rows.Count > 0)
            {
                Llenar_ListView(data);
            }
            else
            {
                lsv_person.Items.Clear();
            }
        }



        #endregion

        private void bt_personal_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 1;
            elTabPage2.Visible = true;
            P_cargar_Todos_Personal();
        }

        private void txt_Buscar_OnValueChanged(object sender, EventArgs e)
        {
            if (txt_Buscar.Text.Length > 2)
            {
                P_Buscar_Personal_porValor(txt_Buscar.Text);
            }
        }

        private void txt_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_Buscar.Text.Length > 2)
                {
                    P_Buscar_Personal_porValor(txt_Buscar.Text);
                }
                else
                {
                    P_cargar_Todos_Personal();
                }
            }
        }

        private void Bt_NewPerso_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Registro_Personal per = new Frm_Registro_Personal();

            fil.Show();
            per.ShowDialog();
            fil.Hide();

            if (per.Tag.ToString() == "A")
            {
                P_cargar_Todos_Personal();
            }

        }

        private void Btn_EditPerso_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Edit_Personal frm_Edit_Personal = new Frm_Edit_Personal();
            string idper = "";

            if (lsv_person.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun reistro para editar. Por favor seleccione un registro para editar.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                var lis = lsv_person.SelectedItems[0];
                idper = lis.SubItems[0].Text;


                frm_Filtro.Show();
                frm_Edit_Personal.Tag = idper;
                frm_Edit_Personal.ShowDialog();
                frm_Filtro.Hide();
                if (frm_Edit_Personal.Tag.ToString() == "A")
                {
                    P_cargar_Todos_Personal();
                }
            }
        }

        private void btn_VerTodoPerso_Click(object sender, EventArgs e)
        {
            P_cargar_Todos_Personal();
        }

        private void bt_nuevoPersonal_Click(object sender, EventArgs e)
        {
            Frm_Filtro fil = new Frm_Filtro();
            Frm_Registro_Personal per = new Frm_Registro_Personal();

            fil.Show();
            per.ShowDialog();
            fil.Hide();

            if (per.Tag.ToString() == "A")
            {
                P_cargar_Todos_Personal();
            }

        }

        private void bt_editarPersonal_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Edit_Personal frm_Edit_Personal = new Frm_Edit_Personal();
            string idper = "";

            if (lsv_person.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun reistro para editar. Por favor seleccione un registro para editar.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                var lis = lsv_person.SelectedItems[0];
                idper = lis.SubItems[0].Text;


                frm_Filtro.Show();
                frm_Edit_Personal.Tag = idper;
                frm_Edit_Personal.ShowDialog();
                frm_Filtro.Hide();
                if (frm_Edit_Personal.Tag.ToString() == "A")
                {
                    P_cargar_Todos_Personal();
                }
            }
        }

        private void bt_eliminarPersonal_Click(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Sino frm_Sino = new Frm_Sino();
            Frm_Msm_Bueno frm_Msm = new Frm_Msm_Bueno();

            if (lsv_person.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun reistro para eliminar. Por favor seleccione un registro para eliminar.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                string idper = "";
                var lis = lsv_person.SelectedItems[0];
                idper = lis.SubItems[0].Text;

                frm_Filtro.Show();
                frm_Sino.Lbl_msm1.Text = "Estas seguro de Eliminar Personal seleccionado";
                frm_Sino.ShowDialog();
                frm_Filtro.Hide();

                if (frm_Sino.Tag.ToString() == "Si")
                {
                    RN_Personal obj = new RN_Personal();
                    obj.RN_EliminarPersonal(idper);

                    if (BD_Personal.eliminado == true)
                    {
                        frm_Filtro.Show();
                        frm_Msm.Lbl_msm1.Text = "El personal seleccionado fue eliminado de forma permanente";
                        frm_Msm.ShowDialog();
                        frm_Filtro.Hide();

                    }
                }
            }
        }

        private void darDeBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Sino frm_Sino = new Frm_Sino();
            Frm_Msm_Bueno frm_Msm = new Frm_Msm_Bueno();

            if (lsv_person.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun reistro para dar de baja. Por favor seleccione un registro por favor.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                string idper = "";
                var lis = lsv_person.SelectedItems[0];
                idper = lis.SubItems[0].Text;

                frm_Filtro.Show();
                frm_Sino.Lbl_msm1.Text = "Estas seguro que deseads dar de baja el Personal seleccionado";
                frm_Sino.ShowDialog();
                frm_Filtro.Hide();

                if (frm_Sino.Tag.ToString() == "Si")
                {
                    RN_Personal obj = new RN_Personal();
                    obj.RN_DarDeBajaPersonal(idper);

                    if (BD_Personal.eliminado == true)
                    {
                        frm_Filtro.Show();
                        frm_Msm.Lbl_msm1.Text = "El personal seleccionado fue dado de baja exitosamente";
                        frm_Msm.ShowDialog();
                        frm_Filtro.Hide();


                    }
                }
            }
        }

        private void verPersonalDeBajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            P_cargar_Todos_PersonaldeBaja();
        }

        private void bt_mostrarTodoElPersonal_Click(object sender, EventArgs e)
        {
            P_cargar_Todos_Personal();
        }

        private void bt_copiarNroDNI_Click(object sender, EventArgs e)
        {

            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();

            if (lsv_person.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun registro para copiar. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string xdni = lsv.SubItems[1].Text;
                Clipboard.Clear();
                Clipboard.SetText(xdni.Trim());
            }
        }

        private void Btn_Cerrar_TabPers_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 0;
            elTabPage2.Visible = false;
            txt_Buscar.Text = "";
            P_cargar_Todos_Personal();
        }

        private void btn_cerrarEx_Asis_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 0;
            elTabPage2.Visible = false;
            txt_buscarAsis.Text = "";
            P_cargar_Todos_Personal();
        }

        private void bt_solicitarJustificacion_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm = new Frm_Filtro();
            Frm_Reg_Justificacion jus = new Frm_Reg_Justificacion();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            if (lsv_person.SelectedItems.Count == 0)
            {
                frm.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun Empleado. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm.Hide();
                return;
            }
            else
            {
                var lsv = lsv_person.SelectedItems[0];
                string xidpersonal = lsv.SubItems[0].Text;
                string xnombre = lsv.SubItems[2].Text;

                frm.Show();
                jus.txt_IdPersona.Text = xidpersonal;
                jus.txt_nompersona.Text = xnombre;
                jus.txt_idjusti.Text = RN_Utilitario.RN_NroDoc(3);

                jus.ShowDialog();
                frm.Hide();

            }


        }

        private void elTabPage2_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Detalle_Click(object sender, EventArgs e)
        {

        }

        private void elTabPage5_Click(object sender, EventArgs e)
        {

        }

        #region "JUSTIFICACION"

        private void ConfigurarListView_Justifi()
        {
            var lis = lsv_justifi;
            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;
            lis.Scrollable = true;
            lis.HideSelection = false;


            lis.Columns.Add("IdJusti", 0, HorizontalAlignment.Left);
            lis.Columns.Add("IdPerso", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Nombres del Personal", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Motivo", 110, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 120, HorizontalAlignment.Left);
            lis.Columns.Add("Detalle Justifi", 0, HorizontalAlignment.Left);

        }

        private void Llenar_ListView_Justi(DataTable data)
        {

            lsv_justifi.Items.Clear();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow dr = data.Rows[i];
                ListViewItem lis = new ListViewItem(dr["Id_Justi"].ToString());
                lis.SubItems.Add(dr["Id_Persl"].ToString());
                lis.SubItems.Add(dr["Nombre_Completo"].ToString());
                lis.SubItems.Add(dr["PrincipalMotivo"].ToString());
                lis.SubItems.Add(dr["FechaEmi"].ToString());
                lis.SubItems.Add(dr["FechaJusti"].ToString());
                lis.SubItems.Add(dr["EstadoJusti"].ToString());
                lis.SubItems.Add(dr["Detalle_Justi"].ToString());

                lsv_justifi.Items.Add(lis);

            }

            lbl_totaljusti.Text = lsv_justifi.Items.Count.ToString();

        }

        private void Cargar_todas_Justificaciones()
        {

            RN_Justificacion obj = new RN_Justificacion();
            DataTable dt = new DataTable();

            dt = obj.RN_Cargar_todos_justificaciones();

            if (dt.Rows.Count > 0)
            {

                //Llamamos al metodo: llenar listview:
                Llenar_ListView_Justi(dt);
            }

            else { lsv_justifi.Items.Clear(); }


        }
        #endregion


        private void Buscar_Justificaciones(string xvalor)
        {

            RN_Justificacion obj = new RN_Justificacion();
            DataTable dt = new DataTable();

            dt = obj.RN_BuscarJustificacion_porValor(xvalor);

            if (dt.Rows.Count > 0)
            {

                //Llamamos al metodo: llenar listview:
                Llenar_ListView_Justi(dt);
            }

            else { lsv_justifi.Items.Clear(); }


        }
        private void bt_exploJusti_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 4;
            elTabPage5.Visible = true;
            txt_buscarjusti.Focus();
            Cargar_todas_Justificaciones();
        }

        private void bt_cerrarjusti_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 0;
            elTabPage5.Visible = false;
            txt_buscarjusti.Text = "";
        }

        private void lsv_justifi_MouseClick(object sender, MouseEventArgs e)
        {
            var lsv = lsv_justifi.SelectedItems[0];
            string detalleJusti = lsv.SubItems[7].Text;

            lbl_Detalle.Text = detalleJusti.Trim();
        }

        private void bt_editJusti_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm = new Frm_Filtro();
            Frm_Reg_Justificacion jus = new Frm_Reg_Justificacion();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();



            if (lsv_justifi.SelectedItems.Count == 0)
            {
                frm.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun registro para editar. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm.Hide();
                return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidjusti = lsv.SubItems[0].Text;
                frm.Show();
                jus.Buscar_Justificacion_paraEditar(xidjusti);
                jus.ShowDialog();
                frm.Hide();

                if (jus.Tag.ToString() == "A")
                {
                    Cargar_todas_Justificaciones();
                }
            }

        }

        private void bt_mostrarJusti_Click(object sender, EventArgs e)
        {
            Cargar_todas_Justificaciones();
        }

        private void bt_ElimiJusti_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Sino frm_Sino = new Frm_Sino();

            RN_Justificacion obj = new RN_Justificacion();


            if (lsv_justifi.SelectedItems.Count == 0)
            {
                frm.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun registro para eliminar. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm.Hide();
                return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidjusti = lsv.SubItems[0].Text;
                frm.Show();
                frm_Sino.Lbl_msm1.Text = "Estas seguro de Eliminar este registro?";
                frm_Sino.ShowDialog();
                frm.Hide();

                if (frm_Sino.Tag.ToString() == "Si")
                {
                    obj.RN_Eliminar_Justificacion(xidjusti);
                    if (BD_Justificacion.editado == true)
                    {
                        Cargar_todas_Justificaciones();

                    }

                }
            }
        }

        private void bt_CopiarNroJusti_Click(object sender, EventArgs e)
        {

            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();

            if (lsv_justifi.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun registro para copiar. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xIdjusti = lsv.SubItems[0].Text;
                Clipboard.Clear();
                Clipboard.SetText(xIdjusti.Trim());
            }
        }

        private void bt_aprobarJustificacion_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Sino frm_Sino = new Frm_Sino();

            RN_Justificacion obj = new RN_Justificacion();

            string xestado = "";


            if (lsv_justifi.SelectedItems.Count == 0)
            {
                frm.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun registro para edirae. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm.Hide();
                return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidjusti = lsv.SubItems[0].Text;
                xestado = lsv.SubItems[6].Text;

                if (xestado.Trim() == "Aprobado")
                {
                    frm.Show();
                    frm_Advertencia.Lbl_Msm1.Text = "La justificacion seleccionada ya esta aprobado. Por favor seleccione otra.";
                    frm_Advertencia.ShowDialog();
                    frm.Hide();
                    return;
                }

                frm.Show();
                frm_Sino.Lbl_msm1.Text = "Estas seguro de editar este registro?";
                frm_Sino.ShowDialog();
                frm.Hide();

                if (frm_Sino.Tag.ToString() == "Si")
                {
                    obj.RN_Abrobar_Desaprobar_Justificacion(xidjusti, "Aprobado");
                    if (BD_Justificacion.editado == true)
                    {
                        Cargar_todas_Justificaciones();

                    }

                }
            }
        }

        private void bt_desaprobarJustificacion_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Sino frm_Sino = new Frm_Sino();

            RN_Justificacion obj = new RN_Justificacion();

            string xestado = "";


            if (lsv_justifi.SelectedItems.Count == 0)
            {
                frm.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun registro para edirae. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm.Hide();
                return;
            }
            else
            {
                var lsv = lsv_justifi.SelectedItems[0];
                string xidjusti = lsv.SubItems[0].Text;
                xestado = lsv.SubItems[6].Text;

                if (xestado.Trim() == "Pendiente")
                {
                    frm.Show();
                    frm_Advertencia.Lbl_Msm1.Text = "La justificacion seleccionada aun no esta aprobado. Por favor seleccione otra.";
                    frm_Advertencia.ShowDialog();
                    frm.Hide();
                    return;
                }

                frm.Show();
                frm_Sino.Lbl_msm1.Text = "Estas seguro de editar este registro?";
                frm_Sino.ShowDialog();
                frm.Hide();

                if (frm_Sino.Tag.ToString() == "Si")
                {
                    obj.RN_Abrobar_Desaprobar_Justificacion(xidjusti, "Pendiente");
                    if (BD_Justificacion.editado == true)
                    {
                        Cargar_todas_Justificaciones();

                    }

                }
            }
        }

        private void txt_buscarjusti_OnValueChanged(object sender, EventArgs e)
        {
            if (txt_buscarjusti.Text.Trim().Length > 2)
            {
                Buscar_Justificaciones(txt_buscarjusti.Text);
            }
        }

        private void txt_buscarjusti_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_buscarjusti.Text.Trim().Length > 2)
                {
                    Buscar_Justificaciones(txt_buscarjusti.Text);
                }
                else
                {
                    Cargar_todas_Justificaciones();
                }
            }
        }

        private void elTabPage4_Click(object sender, EventArgs e)
        {

        }

        private void bt_Config_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 3;
            elTabPage4.Visible = true;
        }

        private void btn_SaveHorario_Click(object sender, EventArgs e)
        {


            Frm_Filtro fil = new Frm_Filtro();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Advertencia adver = new Frm_Advertencia();
            try
            {
                RN_Horario hor = new RN_Horario();
                EN_Horario por = new EN_Horario();


                por.Idhora = lbl_idHorario.Text;
                por.HoEntrada = dtp_horaIngre.Value;
                por.HoSalida = dtp_horaSalida.Value;
                por.HoLimite = Dtp_Hora_Limite.Value;
                por.HoTole = dtp_hora_tolercia.Value;

                hor.RN_actualizarHorario(por);

                if (BD_Horario.gauardado == true)
                {
                    fil.Show();
                    ok.Lbl_msm1.Text = "El horario fue actualizado exitosamente";
                    ok.ShowDialog();
                    fil.Hide();

                    elTab1.SelectedTabPageIndex = 0;
                    elTabPage4.Visible = false;
                }
                else
                {

                }

            }
            catch (Exception ex)
            {

                fil.Show();
                adver.Lbl_Msm1.Text = "Ocurrio el error: " + ex.Message;
                adver.ShowDialog();
                fil.Hide();
            }
        }

        private void elTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Savedrobot_Click(object sender, EventArgs e)
        {
            RN_Utilitario uti = new RN_Utilitario();

            if (rdb_ActivarRobot.Checked == true)
            {
                uti.RN_Actualizar_TipoRobot(4, "Si");
                if (BD_Utilitario.falta == true)
                {
                    Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                    ok.Lbl_msm1.Text = "El bot fue actualizado";
                    ok.ShowDialog();
                    elTab1.SelectedTabPageIndex = 0;
                    elTabPage4.Visible = false;

                }
            }
            else if (rdb_Desact_Robot.Checked == true)
            {
                uti.RN_Actualizar_TipoRobot(4, "No");
                if (BD_Utilitario.falta == true)
                {
                    Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
                    ok.Lbl_msm1.Text = "El bot fue actualizado";
                    ok.ShowDialog();
                    elTab1.SelectedTabPageIndex = 0;
                    elTabPage4.Visible = false;

                }
            }
        }

        private void bt_registrarHuellaDigital_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();
            Frm_Regis_Huella frmHuella = new Frm_Regis_Huella();
            string idper = "";

            if (lsv_person.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun reistro para editar. Por favor seleccione un registro para editar.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                var lis = lsv_person.SelectedItems[0];
                idper = lis.SubItems[0].Text;


                frm_Filtro.Show();
                frmHuella.Tag = idper;
                frmHuella.ShowDialog();
                frm_Filtro.Hide();
                if (frmHuella.Tag.ToString() == "A")
                {
                    P_cargar_Todos_Personal();
                }
            }
        }

        private void bt_Explo_Asis_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 2;
            elTabPage3.Visible = true;
        }

        private void btn_Asis_With_Huella_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Marcar_Asistencia asi = new Frm_Marcar_Asistencia();
            frm_Filtro.Show();
            asi.ShowDialog();
            frm_Filtro.Hide();
        }

        private int temp1 = 0;
        private int segun1 = 0;

        private void timerTempo_Tick(object sender, EventArgs e)
        {
            temp1 += 1;
            lbl_temp.Text = segun1.ToString().PadLeft(2, Convert.ToChar("0")) + ":";
            lbl_temp.Text += temp1.ToString().PadLeft(2, Convert.ToChar("0"));
            lbl_temp.Refresh();
            if (temp1 == 30)
            {
                timerTempo.Stop();
                temp1 = 0;
                Empezar_Registro_Faltas();
            }

        }

        private void Empezar_Registro_Faltas()
        {

            RN_Asistencia objAsis = new RN_Asistencia();
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia advertencia = new Frm_Advertencia();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            DataTable dataper = new DataTable();
            RN_Personal objper = new RN_Personal();
            RN_Justificacion objJus = new RN_Justificacion();


            int horaLimite = Dtp_Hora_Limite.Value.Hour;
            int minLimite = Dtp_Hora_Limite.Value.Minute;

            int horaCaptu = DateTime.Now.Hour;
            int minCaptu = DateTime.Now.Minute;

            string Dniper = "";
            int cant = 0;
            int TotalItem = 0;
            string xIdPersona = "";
            string xIdAsistencia = "";
            string xjustifi = "";
            string xnomDia = "";


            if (horaCaptu < horaLimite)
            {
                lbl_temp.Text = "Bot de asistencias activo, Aun es temprano para marcar en: ";
                return;

            }
            if (horaCaptu >= horaLimite && minCaptu > horaLimite)
            {
                dataper = objper.RN_Lista_Todo_personal();
                if (dataper.Rows.Count <= 0)
                {
                    return;
                }

                TotalItem = dataper.Rows.Count;

                foreach (DataRow reg in dataper.Rows)
                {
                    Dniper = Convert.ToString(reg["DNIPR"]);
                    xIdPersona = Convert.ToString(reg["Id_Persl"]);

                    if (objAsis.RN_verificar_si_marco_aistencia(xIdPersona.Trim()) == false)
                    {
                        if (objAsis.RN_verificar_si_marco_falta(xIdPersona.Trim()) == false)
                        {
                            RN_Asistencia objA = new RN_Asistencia();
                            EN_Asistencia asi = new EN_Asistencia();
                            xIdAsistencia = RN_Utilitario.RN_NroDoc(1);

                            if (objJus.RN_verificar_siPersonal_TieneJustificacion(xIdPersona) == true)
                            {
                                xjustifi = "La falta esta justificada";
                            }
                            else
                            {
                                xjustifi = "Falta no justificada";
                            }

                            xnomDia = DateTime.Now.ToString("ddd");


                            objAsis.RN_registar_falta(xIdAsistencia, xIdPersona, xjustifi, xnomDia);

                            if (BD_Asistencia.entrada == true)
                            {
                                Actualizar_SiguienteNumero(1);
                                cant += 1;
                            }

                        }
                    }
                }//fin for

                if (cant > 1)
                {
                    timerFalta.Stop();
                    frm_Filtro.Show();
                    ok.Lbl_msm1.Text = "Untotal de : " + cant.ToString() + "/" + TotalItem.ToString() + " Faltas se han registrado";
                    ok.ShowDialog();
                    frm_Filtro.Hide();
                    pnl_falta.Visible = false;
                }
                else
                {
                    timerFalta.Stop();
                    frm_Filtro.Show();
                    ok.Lbl_msm1.Text = "El dia de hoy no se registrarron faltas";
                    ok.ShowDialog();
                    frm_Filtro.Hide();
                    pnl_falta.Visible = false;
                }

            }

            else
            {
                lbl_temp.Text = "Bot de asistencias activo, Aun es temprano para marcar en:";
                timerTempo.Start();
            }
        }

        private double Generar_NextId(string numero)
        {
            double newnum = Convert.ToDouble(numero) + 1;
            return newnum;

        }

        private void Actualizar_SiguienteNumero(int idtipo)
        {
            string xnum = BD_Utilitario.BD_leerNumero(idtipo);
            string xnuevonum = Convert.ToString(Generar_NextId(xnum));
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

        private void Lbl_stop_Click(object sender, EventArgs e)
        {
            timerTempo.Stop();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
         
            Frm_Marcar_Asis_Manual frm_Marcar_Asis_Manual = new Frm_Marcar_Asis_Manual();
          

            frm_Filtro.Show();
            frm_Marcar_Asis_Manual.ShowDialog();
            frm_Filtro.Hide();
            
        }
        #region Asistencia



        private void configureListView__Asistencia()
        {

            var lis = lsv_asis;

            lis.Columns.Clear();
            lis.Items.Clear();
            lis.View = View.Details;
            lis.GridLines = false;
            lis.FullRowSelect = true;   
            lis.Scrollable = true;
            lis.HideSelection = false;

            lis.Columns.Add("Id Asis", 0, HorizontalAlignment.Left);
            lis.Columns.Add("DNI", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Nombre del Personal", 316, HorizontalAlignment.Left);
            lis.Columns.Add("Fecha", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Dia", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Hr Ingreso", 80, HorizontalAlignment.Left);
            lis.Columns.Add("Tardanza", 60, HorizontalAlignment.Left);
            lis.Columns.Add("Hr Salida", 100, HorizontalAlignment.Left);
            lis.Columns.Add("Adelanto", 90, HorizontalAlignment.Left);
            lis.Columns.Add("Justificacaion", 0, HorizontalAlignment.Left);
            lis.Columns.Add("Estado", 190, HorizontalAlignment.Left);
            lis.Columns.Add("Horas trabajadas", 90, HorizontalAlignment.Left);


        }

        private void Llenar_lvAsistenia(DataTable dato)
        {
            lsv_asis.Items.Clear();
            for (int i = 0; i < dato.Rows.Count; i++)
            {
                DataRow dr = dato.Rows[i];
                ListViewItem list = new ListViewItem(dr["Id_Asis"].ToString());
                list.SubItems.Add(dr["DNIPR"].ToString());
                list.SubItems.Add(dr["Nombre_Completo"].ToString());
                list.SubItems.Add(dr["FechaAsis"].ToString());
                list.SubItems.Add(dr["Nombre_dia"].ToString());
                list.SubItems.Add(dr["HoIngreso"].ToString());
                list.SubItems.Add(dr["Tardanzas"].ToString());
                list.SubItems.Add(dr["HoSalida"].ToString());
                list.SubItems.Add(dr["EstadoAsis"].ToString());
                list.SubItems.Add(dr["Justificacion"].ToString());
                list.SubItems.Add(dr["Total_HrsWorked"].ToString());

                lsv_asis.Items.Add(list);
            }

        }
        private void Cargar_TodasAsistencias()
        {

            RN_Asistencia obj = new RN_Asistencia();
            DataTable dato = new DataTable();
            dato = obj.BD_Listar_Todas_Asistencias();
            
            if (dato.Rows.Count > 0)
            {
                Llenar_lvAsistenia(dato);
            }
            else
            {
                lsv_asis.Items.Clear();
            }

        }

        private void BuscadorAsistencias(string xvalor)
        {

            RN_Asistencia obj = new RN_Asistencia();
            DataTable dato = new DataTable();
            dato = obj.RN_Buscar_Asistencias(xvalor);

            if (dato.Rows.Count > 0)
            {
                Llenar_lvAsistenia(dato);
            }
            else
            {
                lsv_asis.Items.Clear();
            }

        }

        private void BuscadorAsistencias_dia(DateTime xdia)
        {

            RN_Asistencia obj = new RN_Asistencia();
            DataTable dato = new DataTable();
            dato = obj.RN_Buscar_Dia(xdia);
            
            if (dato.Rows.Count > 0)
            {
                Llenar_lvAsistenia(dato);
            }
            else
            {
                lsv_asis.Items.Clear();
            }

        }

        private void txt_buscarAsis_OnValueChanged(object sender, EventArgs e)
        {
            if (txt_buscarAsis.Text.Trim().Length > 2)
            {
                BuscadorAsistencias(txt_buscarAsis.Text);
            }
        }


        #endregion

        private void txt_buscarAsis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_buscarAsis.Text.Trim().Length > 2)
                {
                    BuscadorAsistencias(txt_buscarAsis.Text);
                }
                else
                {
                    Cargar_TodasAsistencias();
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Frm_Advertencia adver = new Frm_Advertencia();
            Frm_Sino sino = new Frm_Sino(); 
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();
            Frm_Filtro fil = new Frm_Filtro();
            RN_Asistencia obj = new RN_Asistencia();

            if (lsv_asis.SelectedIndices.Count == 0)
            {
                fil.Show();
                adver.Lbl_Msm1.Text = "Seleccione el registro que desea eleminar";
                adver.ShowDialog();
                fil.Hide();
                return;
            }
            else
            {
                var lsv = lsv_asis.SelectedItems[0];
                string xidasis = lsv.SubItems[0].Text;

                sino.Lbl_msm1.Text = "Estas seguro de eliminar este registro" + "\n\r" + "Recuerda que el proceso no puede ser revertido";
                fil.Show();
                sino.ShowDialog();
                fil.Hide();

                if (Convert.ToString(sino.Tag) == "Si")
                {
                    obj.RN_Eliminar_Asistencia(xidasis);
                    if (BD_Asistencia.salida == true)
                    {
                        fil.Show();
                        ok.Lbl_msm1.Text = "Asistencia eliminada";
                        ok.ShowDialog();
                        fil.Hide();
                        Cargar_TodasAsistencias();
                    }
                }
            }

        }

        private void bt_vertodasasistencia_Click(object sender, EventArgs e)
        {
            Cargar_TodasAsistencias();
        }

        private void bt_copiarnrodnitoo_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Advertencia frm_Advertencia = new Frm_Advertencia();

            if (lsv_asis.SelectedItems.Count == 0)
            {
                frm_Filtro.Show();
                frm_Advertencia.Lbl_Msm1.Text = "No selecciono ningun registro para copiar. Por favor seleccione uno.";
                frm_Advertencia.ShowDialog();
                frm_Filtro.Hide();
                return;
            }
            else
            {
                var lsv = lsv_asis.SelectedItems[0];
                string xdni = lsv.SubItems[1].Text;
                Clipboard.Clear();
                Clipboard.SetText(xdni.Trim());
            }
        }

        private void bt_verAsistenciasDelDiaT_Click(object sender, EventArgs e)
        {
            BuscadorAsistencias_dia(dtp_fechadeldia.Value);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancel_horio_Click(object sender, EventArgs e)
        {
            elTab1.SelectedTabPageIndex = 0;
            elTabPage4.Visible = false;
            txt_buscarjusti.Text = "";
        }

        private void btn_Asis_Manual_Click(object sender, EventArgs e)
        {
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Marcar_Asis_Manual asi = new Frm_Marcar_Asis_Manual();
            frm_Filtro.Show();
            asi.ShowDialog();
            frm_Filtro.Hide();
        }

        private void lbl_temp_Click(object sender, EventArgs e)
        {

        }

        private void lsv_asis_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void elLabel3_Click(object sender, EventArgs e)
        {

        }
    }

}
