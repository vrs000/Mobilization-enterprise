using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobOrder
{
    public partial class PrintForm : Form
    {
        public PrintForm()
        {
            InitializeComponent();

            //CheckedList filling
            for (int i = 0; i < GroupsArray.Groups.Count; i++)
            {
                GroupsCheckedList.Items.Add(GroupsArray.Groups[i].Id);
            }


            //DataGridView filling

            //Calculating count of members
            int TotalMember = 0;
            foreach (var item in GroupsArray.Groups)
            {
                TotalMember += item.members.Count;
            }

            //filling
            foreach (var group in GroupsArray.Groups)
            {
                foreach (var member in group.members)
                {
                    ListMembersDataGridView.Rows.Insert(0, 1);
                    ListMembersDataGridView.Rows[0].Cells[0].Value = group.Id;
                    ListMembersDataGridView.Rows[0].Cells[1].Value = member.VusNumber;
                    ListMembersDataGridView.Rows[0].Cells[2].Value = member.FIO;
                    ListMembersDataGridView.Rows[0].Cells[3].Value = member.Rank;
                    ListMembersDataGridView.Rows[0].Cells[4].Value = member.YearOfBirth;
                    ListMembersDataGridView.Rows[0].Cells[5].Value = member.HomeAddress;
                    ListMembersDataGridView.Rows[0].Cells[6].Value = member.PlaceOfWork;
                    ListMembersDataGridView.Rows[0].Cells[7].Value = member.TurnoutAddress;
                    ListMembersDataGridView.Rows[0].Cells[8].Value = member.Company;

                }
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Выбран один человек
            if ((ListMembersDataGridView.SelectedRows.Count == 1) && (GroupsCheckedList.CheckedItems.Count == 0))
            {
                string[] list = {
                    ListMembersDataGridView.SelectedRows[0].Cells[0].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[1].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[2].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[3].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[4].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[5].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[6].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[7].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[0].Cells[8].Value.ToString()
                };



                SaveToWord.SaveMember(list);

            }

            //Выбрано несколько человек
            if ((ListMembersDataGridView.SelectedRows.Count > 1) && (GroupsCheckedList.CheckedItems.Count == 0))
            {
                List<string[]> list = new List<string[]>();

                for (int i = 0; i < ListMembersDataGridView.SelectedRows.Count; i++)
                {
                    string[] l =
                    {
                    ListMembersDataGridView.SelectedRows[i].Cells[0].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[1].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[2].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[3].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[4].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[5].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[6].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[7].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[8].Value.ToString()
                    };

                    list.Add(l);
                }

                SaveToWord.SaveMembers(list);
            }

            //Если выбраны группы одна или больше
            if ((ListMembersDataGridView.SelectedRows.Count == 0) && (GroupsCheckedList.CheckedItems.Count > 0))
            {
                List<string[]> list = new List<string[]>();

                List<Group> groups = new List<Group>();
                //Ищем группы
                for (int i = 0; i < GroupsCheckedList.CheckedItems.Count; i++)
                {
                    groups.Add(GroupsArray.FindGroup(GroupsCheckedList.CheckedItems[i].ToString()));
                    //MessageBox.Show(GroupsArray.FindGroup(GroupsCheckedList.CheckedItems[i].ToString()).Id);
                }
                //нашли все выделенные группы


                //Заполняем список людей для печати

                foreach (var group in groups)
                {
                    foreach (var member in group.members)
                    {
                        string[] l =
                        {
                            group.Id,
                            member.VusNumber,
                            member.FIO,
                            member.Rank,
                            member.YearOfBirth,
                            member.HomeAddress,
                            member.PlaceOfWork,
                            member.TurnoutAddress,
                            member.Company
                        };

                        list.Add(l);
                    }
                }

                

                SaveToWord.SaveMembers(list);

            }

            if ((ListMembersDataGridView.SelectedRows.Count > 0) && (GroupsCheckedList.CheckedItems.Count > 0))
            {
                List<string[]> list = new List<string[]>();

                List<Group> groups = new List<Group>();
                //Ищем группы
                for (int i = 0; i < GroupsCheckedList.CheckedItems.Count; i++)
                {
                    groups.Add(GroupsArray.FindGroup(GroupsCheckedList.CheckedItems[i].ToString()));
                    //MessageBox.Show(GroupsArray.FindGroup(GroupsCheckedList.CheckedItems[i].ToString()).Id);
                }
                //нашли все выделенные группы


                //Заполняем список людей для печати из групп

                foreach (var group in groups)
                {
                    foreach (var member in group.members)
                    {
                        string[] l =
                        {
                            group.Id,
                            member.VusNumber,
                            member.FIO,
                            member.Rank,
                            member.YearOfBirth,
                            member.HomeAddress,
                            member.PlaceOfWork,
                            member.TurnoutAddress,
                            member.Company
                        };

                        list.Add(l);
                    }
                }


                for (int i = 0; i < ListMembersDataGridView.SelectedRows.Count; i++)
                {
                    string[] l =
                    {
                    ListMembersDataGridView.SelectedRows[i].Cells[0].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[1].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[2].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[3].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[4].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[5].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[6].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[7].Value.ToString(),
                    ListMembersDataGridView.SelectedRows[i].Cells[8].Value.ToString()
                    };

                    list.Add(l);
                }

                SaveToWord.SaveMembers(list);
                


            }
        }




        private void GroupsCheckedList_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedGroupLabel.Text = $"Групп {GroupsCheckedList.CheckedItems.Count}";
        }

        private void ListMembersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            SelectedMembersLabel.Text = $"Люди {ListMembersDataGridView.SelectedRows.Count}";
        }
    }
}
