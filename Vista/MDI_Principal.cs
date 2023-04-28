using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Conexion;
using System.IO;

namespace Vista
{
    public partial class MDI_Principal : Form
    {
        private int idUsuarioMdi;
        private LoginUsuario cnMenu = new LoginUsuario();
        public MDI_Principal(int idUsuario_Load)
        {
            InitializeComponent();
            idUsuarioMdi = idUsuario_Load;
        }

        private void MDI_Principal_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(idUsuario.ToString());
            List<Conexion.Menu> permisos_esperado = LoginUsuario.ObtenerPermiso(idUsuarioMdi);

            MenuStrip miMenu = new MenuStrip();

            //para cada menu se leera cada uno de la lista
            foreach (Conexion.Menu objMenu in permisos_esperado)
            {
                ToolStripMenuItem menuPadre = new ToolStripMenuItem(objMenu.nombre);

                //se agrega los iconos al menu
                menuPadre.TextImageRelation = TextImageRelation.ImageAboveText;


                string rutaimagen = Path.GetFullPath(Path.Combine(Application.StartupPath, @"../../") + objMenu.icono);

                menuPadre.Image = new Bitmap(rutaimagen);
                menuPadre.ImageScaling = ToolStripItemImageScaling.None;
                miMenu.Items.Add(menuPadre);
                //Construccion de submenu
                foreach (SubMenu objSubMenu in objMenu.listaSubMenu)
                {
                    ToolStripMenuItem menuHijo = new ToolStripMenuItem(objSubMenu.nombreSub);
                    menuPadre.DropDownItems.Add(menuHijo);
                }
            }

            this.MainMenuStrip = miMenu;
            Controls.Add(miMenu);
        }
    }
}
