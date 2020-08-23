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
            DialogResult = DialogResult.OK;
        }

        private void DialogButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog.InitialDirectory = "c:\\";
            OpenFileDialog.Filter = "DataBase files (*.db)|*.db|All files (*.*)|*.*";
            OpenFileDialog.FilterIndex = 2;
            OpenFileDialog.RestoreDirectory = true;

            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                MessageBox.Show(OpenFileDialog.FileName);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
