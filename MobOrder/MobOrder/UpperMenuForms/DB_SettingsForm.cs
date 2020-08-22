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


            PathTextBox.Text = SQLite.database;

        }

        private void OkButton_Click(object sender, EventArgs e)
        {

        }
    }
}
