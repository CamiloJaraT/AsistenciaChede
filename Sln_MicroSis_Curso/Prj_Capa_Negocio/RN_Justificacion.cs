using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_Capa_Entidad;
using Prj_Capa_Datos;
using System.Data;

namespace Prj_Capa_Negocio
{
    public class RN_Justificacion
    {


        public void RN_registrar_Justificacion(EN_Justificacion jus)
        {

            BD_Justificacion obj = new BD_Justificacion();  
            obj.Bd_registrar_Justificacion(jus);
        }
        public DataTable RN_Cargar_todos_justificaciones()
        {
            BD_Justificacion obj = new BD_Justificacion();
            return obj.BD_Cargar_todos_justificaciones();

        }
        public DataTable RN_BuscarJustificacion_porValor(string xdato)
        {

            BD_Justificacion obj = new BD_Justificacion();
            return obj.BD_BuscarJustificacion_porValor(xdato);
        }

        public void RN_Editar_Justificacion(EN_Justificacion jus)
        {
            BD_Justificacion obj = new BD_Justificacion();
            obj.Bd_Editar_Justificacion(jus);

        }

        public void RN_Eliminar_Justificacion(string idJusti)
        {
            BD_Justificacion obj = new BD_Justificacion();
            obj.Bd_Eliminar_Justificacion(idJusti);

        }

        public void RN_Abrobar_Desaprobar_Justificacion(string idJusti, string estadoJus)
        {

            BD_Justificacion obj = new BD_Justificacion();
            obj.Bd_Abrobar_Desaprobar_Justificacion(idJusti, estadoJus);
        }
        public bool RN_verificar_siPersonal_TieneJustificacion(string idper)
        {
            BD_Justificacion obj = new BD_Justificacion();
            return obj.BD_verificar_siPersonal_TieneJustificacion(idper);
        }

    }
}
