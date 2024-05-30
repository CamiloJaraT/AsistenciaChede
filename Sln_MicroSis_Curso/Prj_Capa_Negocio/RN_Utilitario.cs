using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Prj_Capa_Datos;





namespace Prj_Capa_Negocio
{
  public class RN_Utilitario
    {
        public static string RN_NroDoc(int idtipo)
        {
            return BD_Utilitario.BD_NroDoc(idtipo);
        }

        public static void RN_ActualizarNumero(int idtipo, string numero)
        {
            BD_Utilitario.BD_ActualizarNumero(idtipo, numero);
        }

        public static string RN_leerNumero(int idtipo)
        {
            return BD_Utilitario.BD_leerNumero(idtipo);
        }

        public static string RN_listar_TipoRobot(int idtipo)
        {
            return BD_Utilitario.BD_listar_TipoRobot(idtipo);
        }
        public void RN_Actualizar_TipoRobot(int IdTipo, string serie)
        {
            BD_Utilitario obj = new BD_Utilitario();
            obj.BD_Actualizar_TipoRobot(IdTipo, serie);
        }
    }
}
