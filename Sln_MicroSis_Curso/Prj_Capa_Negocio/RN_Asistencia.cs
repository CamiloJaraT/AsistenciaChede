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
     public class RN_Asistencia
    {

        public bool RN_verificar_si_marco_aistencia(string idper)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_verificar_si_marco_aistencia(idper);
        }
        public bool RN_verificar_si_marco_falta(string idper)
        {

            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_verificar_si_marco_falta(idper);  
        }
        public bool RN_verificar_si_marco_entrada(string idper)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_verificar_si_marco_entrada(idper);
        }
        public void RN_registar_entrada(EN_Asistencia asi)
        {
            BD_Asistencia obj = new BD_Asistencia();
            obj.BD_registar_entrada(asi);

        }

        public void RN_registar_falta(string idAsis, string idper, string justi, string nomdia)
        {
            BD_Asistencia obj = new BD_Asistencia();
            obj.BD_registar_falta(idAsis, idper, justi, nomdia);

        }
        public DataTable RN_Buscar_Asistencia_deEntrada(string idperson)
        {

            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Buscar_Asistencia_deEntrada(idperson);
        }
        public void RN_registar_salida(EN_Asistencia asi)
        {

            BD_Asistencia obj = new BD_Asistencia();
            obj.BD_registar_salida(asi);
        }
        public DataTable BD_Listar_Todas_Asistencias()
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Listar_Todas_Asistencias();

        }
        public DataTable RN_Buscar_Asistencias(string xvalor)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Buscar_Asistencias(xvalor);
        }
        public void RN_Eliminar_Asistencia(string idasis)
        {

            BD_Asistencia obj = new BD_Asistencia();
            obj.BD_Eliminar_Asistencia(idasis);
        }
        public DataTable RN_Buscar_Dia(DateTime xdia)
        {
            BD_Asistencia obj = new BD_Asistencia();
            return obj.BD_Buscar_Dia(xdia);

        }
    }
}
