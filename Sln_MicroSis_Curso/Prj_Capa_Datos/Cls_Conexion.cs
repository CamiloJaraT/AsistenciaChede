using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Prj_Capa_Datos
{
   public class Cls_Conexion
    {

        public string Conectar()
        {
            return @"Data Source=Localhost; Initial Catalog=gestion_personal;uid=root;pwd=1234";
            
        }

        public static string Conectar2()
        {
            return @"Data Source=Localhost; Initial Catalog=gestion_personal;uid=root;pwd=1234";

            
        }

    }
}
