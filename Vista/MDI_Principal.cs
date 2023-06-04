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
using System.Reflection;

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
                    ToolStripMenuItem menuHijo = new ToolStripMenuItem(objSubMenu.nombreSub, null, clic_en_Menu, objSubMenu.nombreFormulario);
                    menuPadre.DropDownItems.Add(menuHijo);
                }
            }

            this.MainMenuStrip = miMenu;
            Controls.Add(miMenu);
        }

        //Metodo para clickear en el container
        private void clic_en_Menu(object sender, System.EventArgs e)
        {
            ToolStripMenuItem menuSeleccionado = (ToolStripMenuItem)sender;
            
            Assembly asm = Assembly.GetEntryAssembly();//obtenie el proceso del ejecutable, es decir los elemento dentro del ejecutable
            Type elemento = asm.GetType(asm.GetName().Name + "." + menuSeleccionado.Name);

            if (elemento == null )
            {
                MessageBox.Show("Formulario no encontrado");
            }
            else
            {
                Form formularioCreado = (Form)Activator.CreateInstance(elemento);
                //validacion si existe el formulario hijo dentro del formulario padre o container
                int auxEncontrado = this.MdiChildren.Where(x => x.Name == formularioCreado.Name).ToList().Count();

                if(auxEncontrado != 0 )
                {
                    //si lo encuentra que no haga nada 
                   ((Form)(this.MdiChildren.Where(x => x.Name == formularioCreado.Name).FirstOrDefault())).WindowState = FormWindowState.Normal;
                    ((Form)(this.MdiChildren.Where(x => x.Name == formularioCreado.Name).FirstOrDefault())).Activate();
                }
                else
                {
                    formularioCreado.MdiParent = this;
                    formularioCreado.Show();
                }

                
            }
            
        }
    }
}
