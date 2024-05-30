using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prj_Capa_Datos;
using Prj_Capa_Entidad;
using System.Data;



namespace Prj_Capa_Negocio
{
     public class RN_Personal
    {

        public void RN_RegistrarPersonal(EN_Persona per)
        {
            BD_Personal obj = new BD_Personal();
            obj.BD_RegistrarPersonal(per);
        }

        public DataTable RN_Lista_Todo_personal()
        {

            BD_Personal obj = new BD_Personal();
            return obj.BD_Lista_Todo_personal();
        }

        public DataTable RN_Buscar_personal_porValor(string valor)
        {
            BD_Personal obj = new BD_Personal();
            return obj.BD_Buscar_personal_porValor(valor);

        }
        public void RN_ActualizarPersonal(EN_Persona per)
        {
            BD_Personal obj = new BD_Personal();
            obj.BD_ActualizarPersonal(per);
        }

        public void RN_EliminarPersonal(string idper)
        {
            BD_Personal obj = new BD_Personal();
            obj.BD_EliminarPersonal(idper);

        }

        public void RN_DarDeBajaPersonal(string idper)
        {
            BD_Personal obj = new BD_Personal();
            obj.BD_DarDeBajaPersonal(idper);

        }
        public DataTable RN_Listar_Personal_deBaja()
        {
            BD_Personal obj = new BD_Personal();
            return obj.BD_Listar_Personal_deBaja();

        }
        public void RN_Registrat_HuellaPersonal(string idper, object huella)
        {

            BD_Personal obj = new BD_Personal();
            obj.Registra_HuellaPersonal(idper, huella);
        }
    }
}
