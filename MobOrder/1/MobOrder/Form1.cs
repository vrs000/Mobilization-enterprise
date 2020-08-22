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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            EditableControls.GroupList = GroupList;
            EditableControls.GroupsTab = GroupsTab;

           // AddTab();
        }



        private void AddTab()
        {
            TabPage page = new TabPage();
            page.Text = "Text1";


            GroupsTab.TabPages.Add(page);

            DataGridView dataGrid = new DataGridView();
            dataGrid.Dock = DockStyle.Fill;

            dataGrid.ColumnCount = 5;
            dataGrid.RowCount = 1;

            dataGrid.Rows[0].HeaderCell.Value = "1245";
            dataGrid.Rows[0].Cells[0].Value = 1;
            dataGrid.Rows[0].Cells[1].Value = 1;
            dataGrid.Rows[0].Cells[2].Value = 1;
            dataGrid.Rows[0].Cells[3].Value = 1;

            dataGrid.Rows.Insert(0, 1);
            dataGrid.Rows[0].Cells[0].Value = 1;
            dataGrid.Rows[0].Cells[1].Value = 1;
            dataGrid.Rows[0].Cells[2].Value = 1;
            dataGrid.Rows[0].Cells[3].Value = 1;
            dataGrid.Rows.Insert(0, 1);

            GroupsTab.TabPages[0].Controls.Add(dataGrid);


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            GroupForm win = new GroupForm();
            win.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GroupList_SelectedValueChanged(object sender, EventArgs e)
        {

            if (GroupList.CheckedItems.Count > 0)
                RemoveButton.Enabled = true;

            if (GroupList.CheckedItems.Count == 0)
                RemoveButton.Enabled = false;

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            List<string> CheckedGroupNames = new List<string>();


            foreach (var CheckedGroupName in GroupList.CheckedItems)
            {
                CheckedGroupNames.Add(CheckedGroupName.ToString());
            }

            //TODO: реализовать удаление из ListCheckBox, удаление вкладок и удаление из хранилища данных

            foreach (var GroupName in CheckedGroupNames)
            {
                try
                {
                    GroupsArray.RemoveGroup(GroupName);
                    EditableControls.RemoveCheckedListItem(GroupName);
                    EditableControls.RemoveTabPage(GroupName);
                }
                catch (Exception){ }

            }

        }
    }
}
