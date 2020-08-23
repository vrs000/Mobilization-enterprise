using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MobOrder
{
    public partial class GroupForm : Form
    {
        public GroupForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string GroupName = GroupNameTextBox.Text;

            if (GroupName.Length > 0)
            {
                GroupsArray.LastGroupName = GroupName;

                if (!GroupsArray.IsCreated(GroupName))
                {
                    GroupsArray.CreateNewGroup(GroupName);
                    EditableControls.GroupList.Items.Add(GroupName);
                }


                Blank win = new Blank();
                win.ShowDialog();
                this.DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show("Введите номер", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GroupForm_Load(object sender, EventArgs e)
        {

        }
    }
}
