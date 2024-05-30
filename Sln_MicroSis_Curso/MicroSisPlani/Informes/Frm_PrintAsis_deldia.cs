using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MicroSisPlani.Informes
{
    public partial class Frm_PrintAsis_deldia : Form
    {
        public Frm_PrintAsis_deldia()
        {
            InitializeComponent();
        }

        public string tipoinfo = "";

        private void Frm_PrintAsis_deldia_Load(object sender, EventArgs e)
        {
           
           
        }



        private void pnl_titulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button ==MouseButtons.Left )
            {
                Utilitarios u = new Utilitarios();
                u.Mover_formulario(this);
            }
        }

       
    }
}
