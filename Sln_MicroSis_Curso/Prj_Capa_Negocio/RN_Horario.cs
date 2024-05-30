using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Prj_Capa_Datos;
using Prj_Capa_Entidad;

namespace Prj_Capa_Negocio
{
  public  class RN_Horario
    {
        public void RN_actualizarHorario(EN_Horario p)
        {
            BD_Horario obj = new BD_Horario();
            obj.BD_actualizarHorario(p);
        }

        public DataTable RN_Leer_Horario()
        {
            BD_Horario obj = new BD_Horario();
           return obj.BD_Leer_Horario();
        }

    }
}
