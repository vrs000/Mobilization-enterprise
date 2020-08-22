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
    public partial class Blank : Form
    {
        public Blank()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //Массив параметров
            string[] List =
            {
                VusNumbtextBox.Text,
                FIOtextBox.Text,
                RanktextBox.Text,
                YeartextBox.Text,
                HomeAdresstextBox.Text,
                WorkPlacetextBox.Text,
                TurnoutAddresstextBox.Text,
                CompanytextBox.Text
            };

            bool IsEmptyField = false;

            foreach (var item in List)
            {
                if (item.Length == 0)
                {
                    IsEmptyField = true;
                }
            }

            if (IsEmptyField)
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var member = new Member()
                {
                    VusNumber = List[0],
                    FIO = List[1],
                    Rank = List[2],
                    YearOfBirth = List[3],
                    HomeAddress = List[4],
                    PlaceOfWork = List[5],
                    TurnoutAddress = List[6],
                    Company = List[7]
                };

                GroupsArray.AddMemberToTheGroup(GroupsArray.LastGroupName, member);

                Group group = GroupsArray.FindGroup(GroupsArray.LastGroupName);

                if (!EditableControls.IsContained(GroupsArray.LastGroupName))
                {
                    EditableControls.CreateNewTab(group);
                }
                else
                //if (EditableControls.IsContained(GroupsArray.LastGroupName))
                {
                    EditableControls.AddMemberToTab(GroupsArray.LastGroupName, member);
                }

                this.Close();
            }



        }
    }
}
