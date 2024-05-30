﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using Prj_Capa_Entidad;
using Prj_Capa_Negocio;
using Prj_Capa_Datos;
using DPFP;
using System.IO;
using MicroSisPlani.Msm_Forms;



namespace MicroSisPlani
{
    public partial class Frm_Marcar_Asis_Manual : Form
    {
        public Frm_Marcar_Asis_Manual()
        {
            InitializeComponent();
        }



        private void Frm_Marcar_Asis_Manual_Load(object sender, EventArgs e)
        {

            Cargar_Horarios();
            txt_dni_Buscar.Focus();
        }

        private void Cargar_Horarios()
        {

            RN_Horario obj = new RN_Horario();
            DataTable dt = new DataTable();
            dt = obj.RN_Leer_Horario();

            if (dt.Rows.Count == 0)
            {
                return;
            }

            dtp_horaIngre.Value = Convert.ToDateTime(dt.Rows[0]["HoEntrada"]);


            Lbl_HoraEntrada.Text = dtp_horaIngre.Value.Hour.ToString() + "" + dtp_horaIngre.Value.Minute.ToString(":00");
            dtp_horaSalida.Value = Convert.ToDateTime(dt.Rows[0]["HoSalida"]);
            dtp_hora_tolercia.Value = Convert.ToDateTime(dt.Rows[0]["MiTolrncia"]);
            Dtp_Hora_Limite.Value = Convert.ToDateTime(dt.Rows[0]["HoLimite"]);

        }

        private void Calcular_minutosTarde()
        {
            int horaIn = dtp_horaIngre.Value.Hour;
            int minIn = dtp_horaIngre.Value.Minute;

            int horaTole = dtp_hora_tolercia.Value.Hour;
            int minTole = dtp_hora_tolercia.Value.Minute;

            int horaNow = DateTime.Now.Hour;
            int minNow = DateTime.Now.Minute;

            int totalMin = minTole + minIn;
            int totalTadanza = 0;


            if (horaNow == horaIn && minNow > totalMin)
            {
                lbl_totaltarde.Text = "0";
            }
            if (horaNow == horaIn & minNow > totalMin)
            {
                totalTadanza = minNow - totalMin;
                lbl_totaltarde.Text = totalTadanza.ToString();
            }
            else if (horaNow > horaIn)
            {
                int horaTarde = horaNow - horaIn;
                totalTadanza = minNow - totalMin;
                int horaEnMin = horaTarde * 60;

                int nuevoTarde = horaEnMin - totalMin;
                totalTadanza = minNow + nuevoTarde;


                lbl_totaltarde.Text = totalTadanza.ToString();
            }

        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Calcular_minutosTarde();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lbl_hora.Text = DateTime.Now.ToString("hh:mm:ss");

        }

        private void Lbl_HoraEntrada_Click(object sender, EventArgs e)
        {

        }

        private void MarcarAsisManual()
        {


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

        private bool Verificar_hrLimite()
        {

            int xhour = Dtp_Hora_Limite.Value.Hour;
            int xminu = Dtp_Hora_Limite.Value.Minute;

            int horaCaptu = DateTime.Now.Hour;
            int minuCaptu = DateTime.Now.Minute;

            if (xhour == horaCaptu & xminu < minuCaptu)
            {

                return false;

            }
            else if (xhour < horaCaptu)
            {
                return false;
            }
            else if (xhour == horaCaptu & xminu > minuCaptu)
            {
                return true;
            }
            else if (xhour > horaCaptu)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private void TocarAudio()
        {
            string ruta;
            ruta = Application.StartupPath;
            System.Media.SoundPlayer son;
            son = new System.Media.SoundPlayer(ruta + @"\timbre1.wav");
            son.Play();

        }

        private int segundo = 10;
        private void tmr_Conta_Tick(object sender, EventArgs e)
        {
            segundo -= 1;

            lbl_Cont.Text = segundo.ToString();
            lbl_Cont.Refresh();

            if (segundo == 0)
            {
                LimpiarFormulaario();
                segundo = 10;
                tmr_Conta.Stop();
                lbl_Cont.Text = "10";
            }
        }
        private void LimpiarFormulaario()
        {


            lbl_nombresocio.Text = "";
            lbl_totaltarde.Text = "0";
            lbl_TotalHotrabajda.Text = "0";
            lbl_Dni.Text = "";
            lbl_Cont.Text = "0";
            lbl_IdAsis.Text = "";
            Lbl_Idperso.Text = "";
            lbl_justifi.Text = "";
            lbl_msm.BackColor = Color.Transparent;
            lbl_msm.Text = "";
            picSocio.Image = null;

        }

        private void btn_Salir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_dni_Buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_dni_Buscar.Text.Length > 2)
                {
                    btn_buscar_Click(sender, e);
                }
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            lbl_hora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void timer1_Tick_2(object sender, EventArgs e)
        {
            lbl_hora.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            RN_Asistencia asis = new RN_Asistencia();
            RN_Personal personal = new RN_Personal();
            DataTable dtper = new DataTable();
            DataTable dtasis = new DataTable();
            EN_Asistencia asistencia = new EN_Asistencia();

            Frm_Advertencia frm = new Frm_Advertencia();
            Frm_Filtro frm_Filtro = new Frm_Filtro();
            Frm_Msm_Bueno ok = new Frm_Msm_Bueno();

            byte[] huellaPersona;
            string xidPersona = "";
            string xRutaFoto = "";
            string xnombrePersonal = "";
            string xDniPersona = "";


            bool termina = false;
            int totalFilas = 0;
            int nroVeces = 0;

            RN_Horario obj = new RN_Horario();
            DataTable dt = new DataTable();
            dt = obj.RN_Leer_Horario();

            try
            {
                dtper = personal.RN_Buscar_personal_porValor(txt_dni_Buscar.Text.Trim());
                if (dtper.Rows.Count == 0)
                {
                    return;
                }
                totalFilas = dtper.Rows.Count;
                var datoPer = dtper.Rows[0];

                xidPersona = Convert.ToString(datoPer["Id_Persl"]);

                xRutaFoto = Convert.ToString(datoPer["Foto"]);
                lbl_nombresocio.Text = Convert.ToString(datoPer["Nombre_Completo"]);
                Lbl_Idperso.Text = Convert.ToString(datoPer["Id_Persl"]);
                lbl_Dni.Text = Convert.ToString(datoPer["DNIPR"]);

                if (File.Exists(xRutaFoto) == true)
                {
                    picSocio.Load(xRutaFoto.Trim());

                }
                else
                {
                    picSocio.Load(Application.StartupPath + @"\user.png");
                }

                if (asis.RN_verificar_si_marco_aistencia(Lbl_Idperso.Text) == true)
                {
                    frm_Filtro.Show();
                    frm.Lbl_Msm1.Text = "El personal ya marco asistencia";
                    frm.ShowDialog();
                    frm_Filtro.Hide();
                    termina = true;
                    return;
                }
                if (asis.RN_verificar_si_marco_entrada(Lbl_Idperso.Text) == true)
                {
                    Frm_Sino frm_Sino = new Frm_Sino();
                    termina = true;
                    frm_Filtro.Show();
                    frm_Sino.Lbl_msm1.Text = "El usuario ya tiene registro de entrada. Le egustraria marcar su salida?";
                    frm_Sino.ShowDialog();
                    frm_Filtro.Hide();

                    if (frm_Sino.Tag.ToString() == "Si")
                    {
                        dtasis = asis.RN_Buscar_Asistencia_deEntrada(Lbl_Idperso.Text);
                        if (dtasis.Rows.Count == 0)
                        {
                            MessageBox.Show("No tiene asistencia registrada");
                            return;
                        }

                        lbl_IdAsis.Text = Convert.ToString(dtasis.Rows[0]["Id_Asis"]);

                        int thr = Convert.ToInt32(DateTime.Now.Hour - Convert.ToDateTime(dt.Rows[0]["HoEntrada"]).Hour);

                        asistencia.IdAsistencia = lbl_IdAsis.Text;
                        asistencia.Id_Personal = Lbl_Idperso.Text;
                        asistencia.HoraSalida = lbl_hora.Text;
                        asistencia.TotalHoras = thr;


                        asis.RN_registar_salida(asistencia);


                        if (BD_Asistencia.salida == true)
                        {
                            lbl_msm.BackColor = Color.YellowGreen;
                            lbl_msm.ForeColor = Color.White;
                            lbl_msm.Text = "La salida del personal fue registrada exitosamente";
                            TocarAudio();

                            pnl_Msm.Visible = true;

                            lbl_Cont.Text = "10";
                            tmr_Conta.Enabled = true;





                        }

                    }



                }
                else
                {
                    if (Verificar_hrLimite() == false)
                    {
                        frm_Filtro.Show();
                        frm.Lbl_Msm1.Text = "Su hora de entrada ya caduco";
                        frm.ShowDialog();
                        frm_Filtro.Hide();
                        return;
                    }
                    Calcular_minutosTarde();
                    lbl_IdAsis.Text = RN_Utilitario.RN_NroDoc(1);
                    string nomDia = DateTime.Now.ToString("dddd");

                    asistencia.IdAsistencia = lbl_IdAsis.Text;
                    asistencia.Id_Personal = Lbl_Idperso.Text;
                    asistencia.Nombre_Dia = nomDia;
                    asistencia.HoraIngre = lbl_hora.Text;
                    asistencia.Justificacion = "Pendiente";
                    asistencia.Tardanza = Convert.ToInt32(lbl_totaltarde.Text);

                    asis.RN_registar_entrada(asistencia);

                    if (BD_Asistencia.entrada == true)
                    {
                        Actualizar_SiguienteNumero(1);
                        frm.Show();
                        ok.Lbl_msm1.Text = "La asistencia se registro correctamente";
                        ok.ShowDialog();
                        frm.Hide();

                        termina = true;
                    }
                }




            }
            catch (Exception ex)
            {

                string sms = ex.Message;
            }

        }
        
        private void tmr_Conta_Tick_1(object sender, EventArgs e)
        {
            segundo -= 1;

            lbl_Cont.Text = segundo.ToString();
            lbl_Cont.Refresh();

            if (segundo == 0)
            {
                LimpiarFormulaario();
                segundo = 10;
                tmr_Conta.Stop();
                lbl_Cont.Text = "10";
            }
        }
    }
}
