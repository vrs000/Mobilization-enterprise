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
    public partial class EditBlank : Form
    {
        List<string> pars;
        DataGridView dataGrid;
        string GroupName;

        public EditBlank()
        {
            InitializeComponent();

            pars = new List<string>();

            dataGrid = (DataGridView)EditableControls.GroupsTab.SelectedTab.Controls[0];


            var row = dataGrid.SelectedRows[0];

            GroupName = row.Cells[0].Value.ToString();

            //группа фио звание дата_рождения Домашний адрес


            for (int i = 1; i <= 8; i++)
                pars.Add(row.Cells[i].Value.ToString());


            VusNumbtextBox.Text = row.Cells[8].Value.ToString();

            FIOtextBox.Text = row.Cells[1].Value.ToString();
            RankComboBox.SelectedItem = (object)row.Cells[2].Value.ToString();

            var date = row.Cells[3].Value.ToString();
            int year, month, day;

            year = Convert.ToInt32(date.Split('.')[2]);
            month = Convert.ToInt32(date.Split('.')[1]);
            day = Convert.ToInt32(date.Split('.')[0]);


            DateOfBirth.Value = new DateTime(year, month, day);
            HomeAdresstextBox.Text = row.Cells[4].Value.ToString();
            WorkPlacetextBox.Text = row.Cells[5].Value.ToString();
            TurnoutAddresstextBox.Text = row.Cells[6].Value.ToString();
            CompanytextBox.Text = row.Cells[7].Value.ToString();


        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //Массив параметров
            string[] List =
            {
                FIOtextBox.Text,
                RankComboBox.SelectedItem.ToString(),
                $"{DateOfBirth.Value.Day:0#}.{DateOfBirth.Value.Month:0#}.{DateOfBirth.Value.Year}",
                HomeAdresstextBox.Text,
                WorkPlacetextBox.Text,
                TurnoutAddresstextBox.Text,
                CompanytextBox.Text,
                VusNumbtextBox.Text

            };

            bool IsChanged = false;

            for (int i = 0; i < List.Length; i++)
            {
                if (pars[i] != List[i])
                    IsChanged = true;
            }


            //Если внесли изменения
            if (IsChanged)
            {
                //MessageBox.Show("Changed");


                Member OldMember = new Member()
                {
                    FIO = pars[0],
                    Rank = pars[1],
                    YearOfBirth = pars[2],
                    HomeAddress = pars[3],
                    PlaceOfWork = pars[4],
                    TurnoutAddress = pars[5],
                    Company = pars[6],
                    VusNumber = pars[7]
                };

                Member NewMember = new Member()
                {
                    FIO = FIOtextBox.Text,
                    Rank = RankComboBox.SelectedItem.ToString(),
                    YearOfBirth = $"{DateOfBirth.Value.Day:0#}.{DateOfBirth.Value.Month:0#}.{DateOfBirth.Value.Year}",
                    HomeAddress = HomeAdresstextBox.Text,
                    PlaceOfWork = WorkPlacetextBox.Text,
                    TurnoutAddress = TurnoutAddresstextBox.Text,
                    Company = CompanytextBox.Text,
                    VusNumber = VusNumbtextBox.Text
                };



                //TODO:


                //Обновить в списке
                var group = GroupsArray.FindGroup(GroupName);
                for (int i = 0; i < group.members.Count; i++)
                {
                    var memb = group.members[i];

                    if (
                        (memb.FIO == OldMember.FIO)
                        &&
                        (memb.Rank == OldMember.Rank)
                        &&
                        (memb.YearOfBirth == OldMember.YearOfBirth)
                        &&
                        (memb.HomeAddress == OldMember.HomeAddress)
                        &&
                         (memb.PlaceOfWork == OldMember.PlaceOfWork)
                        &&
                        (memb.TurnoutAddress == OldMember.TurnoutAddress)
                        &&
                        (memb.Company == OldMember.Company)
                        &&
                        (memb.VusNumber == OldMember.VusNumber)
                        )
                    {


                        group.members[i].FIO = OldMember.FIO;

                        group.members[i].Rank = OldMember.Rank;

                        group.members[i].YearOfBirth = OldMember.YearOfBirth;

                        group.members[i].HomeAddress = OldMember.HomeAddress;

                        group.members[i].PlaceOfWork = OldMember.PlaceOfWork;

                        group.members[i].TurnoutAddress = OldMember.TurnoutAddress;

                        group.members[i].Company = OldMember.Company;

                        group.members[i].VusNumber = OldMember.VusNumber;
                        break;
                    }
                }


                //Обновить таблицу

                for (int i = 0; i < List.Length; i++)
                {
                    dataGrid.SelectedRows[0].Cells[i + 1].Value = List[i];
                }




                //Обновить БД
                MySql.UpdateMemberInfo(OldMember, NewMember);






                Close();
            }


            if (!IsChanged)
            {
                //MessageBox.Show("Not changed");

                Close();
            }

        }
    }
}
