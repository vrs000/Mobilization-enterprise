using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobOrder.UpperMenuForms
{
    public partial class DB_SettingsForm : Form
    {
        public DB_SettingsForm()
        {
            InitializeComponent();


            IpTextBox.Text = MySql.host;
            PortTextBox.Text = MySql.port.ToString();
            LoginTextBox.Text = MySql.username;
            PasswordTextBox.Text = MySql.password;

        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }
    }
}
