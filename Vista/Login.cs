using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            int idUsuarioa_esperado = LoginUsuario.Loguear(txtUsuario.Text, txtClave.Text);

            if (idUsuarioa_esperado != 0)
            {
                MessageBox.Show("Bienveido " + txtUsuario.Text);
                this.Hide();
                MDI_Principal mdi = new MDI_Principal(idUsuarioa_esperado);
                mdi.Show();
            }
            else
            {
                MessageBox.Show("Usuario NO Encontrado");
            }
        }

        private void txtClave_TextChanged(object sender, EventArgs e)
        {
            txtClave.PasswordChar = '*';
        }
    }
}
