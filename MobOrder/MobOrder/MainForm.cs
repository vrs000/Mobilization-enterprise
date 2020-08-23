using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MobOrder.UpperMenuForms;

namespace MobOrder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            EditableControls.GroupList = GroupList;
            EditableControls.GroupsTab = GroupsTab;

            SQLite.FirstInitialize();

            EditableControls.ResizeColumns();


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

        private void AddButton_Click(object sender, EventArgs e)
        {
            GroupForm win = new GroupForm();
            win.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GroupList_SelectedValueChanged(object sender, EventArgs e)
        {
            //RemoveButton.Enabled = true;
            //if (GroupList.CheckedItems.Count > 0)
            //    RemoveButton.Enabled = true;

            //if (GroupList.CheckedItems.Count == 0)
            //    RemoveButton.Enabled = false;

        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                var dataGrid = (DataGridView)EditableControls.GroupsTab.SelectedTab.Controls[0];
                Member memb;
                string Group_Name;

                foreach (DataGridViewRow row in dataGrid.SelectedRows)
                {

                    Group_Name = row.Cells[0].Value.ToString();

                    //MessageBox.Show(Group_Name);
                    memb = new Member()
                    {
                        FIO = row.Cells[1].Value.ToString(),
                        Rank = row.Cells[2].Value.ToString(),
                        YearOfBirth = row.Cells[3].Value.ToString(),
                        HomeAddress = row.Cells[4].Value.ToString(),
                        PlaceOfWork = row.Cells[5].Value.ToString(),
                        TurnoutAddress = row.Cells[6].Value.ToString(),
                        Company = row.Cells[7].Value.ToString(),
                        VusNumber = row.Cells[8].Value.ToString()
                    };


                    //TODO:
                    //Удалить из БД
                    SQLite.RemoveMemberFromDB(Group_Name, memb);


                    //Удалить из программного хранилища

                    var membs = GroupsArray.FindGroup(Group_Name).members;

                    for (int i = 0; i < membs.Count; i++)
                    {
                        var member = membs[i];

                        if (
                          (member.FIO == memb.FIO)
                          &&
                          (member.Rank == memb.Rank)
                          &&
                          (member.YearOfBirth == memb.YearOfBirth)
                          &&
                          (member.HomeAddress == memb.HomeAddress)
                          &&
                           (member.PlaceOfWork == memb.PlaceOfWork)
                          &&
                          (member.TurnoutAddress == memb.TurnoutAddress)
                          &&
                          (member.Company == memb.Company)
                          &&
                          (member.VusNumber == memb.VusNumber)
                          )
                        {
                            membs.RemoveAt(i);
                            break;
                        }
                    }


                    //Удалить из таблицы
                    dataGrid.Rows.Remove(row);



                }
            }
            catch (Exception)
            {

            }



            #region Удаление группы
            List<string> CheckedGroupNames = new List<string>();
            foreach (var CheckedGroupName in GroupList.CheckedItems)
            {
                CheckedGroupNames.Add(CheckedGroupName.ToString());

                SQLite.RemoveGroupFromDB(CheckedGroupName.ToString());

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
                catch (Exception) { }

            }
            #endregion

        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            new PrintForm().ShowDialog();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {

            var dataGrid = (DataGridView)EditableControls.GroupsTab.SelectedTab.Controls[0];

            if (dataGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Не выбрано", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (dataGrid.SelectedRows.Count == 1)
            {
                EditBlank win = new EditBlank();
                win.ShowDialog();
            }

        }

        //private void СоздатьToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    AddButton_Click(sender, e);
        //}

        //private void ИзменитьToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    ChangeButton_Click(sender, e);
        //}

        //private void УдалитьToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    RemoveButton_Click(sender, e);
        //}

        private void СправкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ReferenceForm().ShowDialog();
        }

        private void НастройкиПодключенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DB_SettingsForm().ShowDialog();
        }

        private void ыфваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DB_SettingsForm().ShowDialog();

        }
    }
}
