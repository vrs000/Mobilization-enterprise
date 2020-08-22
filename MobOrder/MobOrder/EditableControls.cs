using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MobOrder
{
    public static class EditableControls
    {
        public static CheckedListBox GroupList { get; set; }
        public static TabControl GroupsTab { get; set; }


        //Закрытый метод для обработки события изменения данных в ячейки таблицы
        private static void CellEdited(object sender, DataGridViewCellEventArgs e)
        {
            /*Нужно обработать изменение данных в ячейке
             *1. Столбцы в строке должны быть заполнены 
             *2. Находим название группы
             *3. Идентифицируем человека (по фио, датам и т.д.)
             *4. узнаем параметр который изменился
             */

            DataGridView dataGrid = (DataGridView)sender;

            int Col = e.ColumnIndex;
            int Row = e.RowIndex;



            bool IsFilled = true;

            List<string> listParams = new List<string>();

            for (int i = 0; i < dataGrid.ColumnCount; i++)
            {
                string value = dataGrid.Rows[Row].Cells[i].Value.ToString();

                if (value == "")
                    IsFilled = false;


                listParams.Add(value);

            }

            //Если строка заполнена полностью
            if (IsFilled)
            {
                string GroupName = dataGrid.Rows[Row].Cells[0].Value.ToString();
                var group = GroupsArray.FindGroup(GroupName);

                int count = 0;
                int pointer = 0;

                foreach (var memb in group.members)
                {
                    count = 0;

                    //Ищем совпадение по 7 пунктам из 8
                    if (memb.FIO == listParams[1]) count++; else pointer = 1;
                    if (memb.Rank == listParams[2]) count++; else pointer = 2;
                    if (memb.YearOfBirth == listParams[3]) count++; else pointer = 3;
                    if (memb.HomeAddress == listParams[4]) count++; else pointer = 4;
                    if (memb.PlaceOfWork == listParams[5]) count++; else pointer = 5;
                    if (memb.TurnoutAddress == listParams[6]) count++; else pointer = 6;
                    if (memb.Company == listParams[7]) count++; else pointer = 7;
                    if (memb.VusNumber == listParams[8]) count++; else pointer = 8;

                    //Если нашли, то записываем измененный параметр в память
                    if (count == 7)
                    {
                        MessageBox.Show("Success");
                        switch (pointer)
                        {
                            case 1:
                                memb.FIO = listParams[1];
                                MySql.UpdateMemberInfo("FIO", listParams[1], "Adress", listParams[4]);
                                break;
                            case 2:
                                memb.Rank = listParams[2];
                                MySql.UpdateMemberInfo("_rank", listParams[2], "FIO", listParams[1]);
                                break;
                            case 3:
                                memb.YearOfBirth = listParams[3];

                                break;
                            case 4:
                                memb.HomeAddress = listParams[4];
                                MySql.UpdateMemberInfo("Adress", listParams[4], "FIO", listParams[1]);

                                break;
                            case 5:
                                memb.PlaceOfWork = listParams[5];
                                MySql.UpdateMemberInfo("WorkPlace", listParams[5], "FIO", listParams[1]);
                                break;
                            case 6:
                                memb.TurnoutAddress = listParams[6];
                                MySql.UpdateMemberInfo("TurnoutAdress", listParams[6], "FIO", listParams[1]);
                                break;
                            case 7:
                                memb.Company = listParams[7];
                                MySql.UpdateMemberInfo("Company", listParams[7], "FIO", listParams[1]);
                                break;
                            case 8:
                                memb.VusNumber = listParams[8];
                                MySql.UpdateMemberInfo("Vus", listParams[8], "FIO", listParams[1]);
                                break;

                            default:
                                break;
                        }
                    }

                    //ВВОД ПОСРЕДСТВОМ ДОБАВЛЕНИЯ НОВОЙ СТРОКИ НЕ ПРЕДУСМОТРЕН 



                }


            }

            //Id;
            //"ФИО";
            //"Звание";
            //"Год рождения";
            //"Домашний адрес";
            //"Место работы";
            //"Явиться по адресу";
            //"Компания";
            //"ВУС №";




        }

        //Закрытый метод для обработки события удаления строки из таблицы
        private static void UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var list = e.Row.Cells;

            //Id;
            //"ФИО";
            //"Звание";
            //"Год рождения";
            //"Домашний адрес";
            //"Место работы";
            //"Явиться по адресу";
            //"Компания";
            //"ВУС №";


            string GroupName = list[0].Value.ToString();

            Member member = new Member()
            {
                FIO = list[1].Value.ToString(),
                Rank = list[2].Value.ToString(),
                YearOfBirth = list[3].Value.ToString(),
                HomeAddress = list[4].Value.ToString(),
                PlaceOfWork = list[5].Value.ToString(),
                TurnoutAddress = list[6].Value.ToString(),
                Company = list[7].Value.ToString(),
                VusNumber = list[8].Value.ToString()
            };


            var group = GroupsArray.FindGroup(GroupName);
            group.RemoveMember(member);

            MySql.RemoveMemberFromDB(GroupName, member);

            //MessageBox.Show("Deleted");



        }

        //Формирование таблицы DataGridView
        private static DataGridView MakeTable(Group group)
        {
            DataGridView dataGrid = new DataGridView();

            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGrid.CellEndEdit += new DataGridViewCellEventHandler(CellEdited);
            dataGrid.UserDeletingRow += new DataGridViewRowCancelEventHandler(UserDeletingRow);

            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGrid.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGrid.RowTemplate.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGrid.BackgroundColor = Color.White;
            //dataGrid.ForeColor = Color.Black;
            //dataGrid.GridColor = Color.Black;

            dataGrid.AllowUserToAddRows = false;
            dataGrid.AllowUserToDeleteRows = false;
            //dataGrid.cha

            dataGrid.Dock = DockStyle.Fill;
            dataGrid.ColumnCount = 9;

            /*
             * ###############
             * #TEST##########
             * ###############
             */

            //dataGrid.RowCount = group.members.Count;
            dataGrid.RowCount = 2;

            //dataGrid.Columns[0].HeaderText = group.Id;
            dataGrid.Columns[0].HeaderText = "Команда №";
            dataGrid.Columns[1].HeaderText = "ФИО";
            dataGrid.Columns[2].HeaderText = "Звание";
            dataGrid.Columns[3].HeaderText = "Дата рождения";
            dataGrid.Columns[4].HeaderText = "Домашний адрес";
            dataGrid.Columns[5].HeaderText = "Место работы";
            dataGrid.Columns[6].HeaderText = "Явиться по адресу";
            dataGrid.Columns[7].HeaderText = "Компания";
            dataGrid.Columns[8].HeaderText = "ВУС №";



            return dataGrid;
        }

        //Создание новой вкладкой с данной группой
        public static void CreateNewTab(Group group)
        {
            TabPage page = new TabPage();
            page.Text = group.Id;

            //Формирование таблицы
            DataGridView dataGrid = MakeTable(group);

         

            //Заполнение таблицы
            for (int i = 0; i < group.members.Count; i++)
            {
                dataGrid.Rows[i].Cells[0].Value = group.Id;

                dataGrid.Rows[i].Cells[1].Value = group.members[i].FIO;
                dataGrid.Rows[i].Cells[2].Value = group.members[i].Rank;
                dataGrid.Rows[i].Cells[3].Value = group.members[i].YearOfBirth;
                dataGrid.Rows[i].Cells[4].Value = group.members[i].HomeAddress;
                dataGrid.Rows[i].Cells[5].Value = group.members[i].PlaceOfWork;
                dataGrid.Rows[i].Cells[6].Value = group.members[i].TurnoutAddress;
                dataGrid.Rows[i].Cells[7].Value = group.members[i].Company;
                dataGrid.Rows[i].Cells[8].Value = group.members[i].VusNumber;

            }

            page.Controls.Add(dataGrid);

          

            GroupsTab.TabPages.Add(page);


        }


        //Добавить пользователя в существующую таблицу
        public static void AddMemberToTab(string GroupName, Member member)
        {
            DataGridView dataGrid = new DataGridView();

            //Ищем вкладку куда будем добавлять пользователя
            for (int i = 0; i < GroupsTab.TabPages.Count; i++)
            {
                if (GroupsTab.TabPages[i].Text == GroupName)
                {
                    dataGrid = (DataGridView)GroupsTab.TabPages[i].Controls[0];
                }
            }

            

            //Добавляем новую строку в таблицу и заполняем её
            //dataGrid.Rows.Add();
            dataGrid.Rows.Insert(0, 1);

            int LastRow = 0;

            dataGrid.Rows[LastRow].Cells[0].Value = GroupName;

            dataGrid.Rows[LastRow].Cells[1].Value = member.FIO;
            dataGrid.Rows[LastRow].Cells[2].Value = member.Rank;
            dataGrid.Rows[LastRow].Cells[3].Value = member.YearOfBirth;
            dataGrid.Rows[LastRow].Cells[4].Value = member.HomeAddress;
            dataGrid.Rows[LastRow].Cells[5].Value = member.PlaceOfWork;
            dataGrid.Rows[LastRow].Cells[6].Value = member.TurnoutAddress;
            dataGrid.Rows[LastRow].Cells[7].Value = member.Company;
            dataGrid.Rows[LastRow].Cells[8].Value = member.VusNumber;

            dataGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);


        }


        //Существует ли вкладка с заголовком GroupName
        public static bool IsContained(string GroupName)
        {

            for (int i = 0; i < GroupsTab.TabPages.Count; i++)
            {
                if (GroupsTab.TabPages[i].Text == GroupName)
                    return true;
            }

            return false;
        }


        //Удалить группу из CheckedListBox
        public static void RemoveCheckedListItem(string GroupName)
        {
            //TODO

            for (int i = 0; i < GroupList.Items.Count; i++)
            {
                if (GroupList.Items[i].ToString() == GroupName)
                {
                    GroupList.Items.RemoveAt(i);
                }
            }

        }

        //Удалить вкладку из TabControl
        public static void RemoveTabPage(string GroupName)
        {
            //TODO
            int Number = 0;
            for (int i = 0; i < GroupsTab.TabPages.Count; i++)
            {
                if (GroupsTab.TabPages[i].Text == GroupName)
                    Number = i;

            }

            GroupsTab.TabPages.RemoveAt(Number);



        }


        //Автоподбор ширины всех таблиц
        public static void ResizeColumns()
        {
            foreach (TabPage item in GroupsTab.TabPages)
            {
                ((DataGridView)item.Controls[0]).AutoResizeColumns();
            }
        }

    }
}
