using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Prj_Capa_Datos;




namespace Prj_Capa_Negocio
{
     public class RN_Rol
    {


        public DataTable RN_listarRoles()
        {
            BD_Rol obj = new BD_Rol();
            return obj.BD_listarRoles();
        }

    }
}
