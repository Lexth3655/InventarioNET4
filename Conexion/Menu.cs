using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conexion
{
    public class Menu
    {
        public string nombre { get; set; }
        public string icono { get; set; }

        //lista de submenu
        public List<SubMenu> listaSubMenu { get; set; }
    }
}
