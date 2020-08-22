using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace MobOrder
{

    public static class SQLite
    {
        public static string database = "MembersDataBase.db";
 
        private static SQLiteConnection connection = 
            new SQLiteConnection($@"Data Source={database}; Version=3;");

        public static void FirstInitialize()
        {
           

            if (!File.Exists(database))
            {
                SQLiteConnection.CreateFile(database);
            }


            //SQLiteConnection connection = new SQLiteConnection($@"Data Source={database}; Version=3;");



            //Запрос на создание БД с таблицей
            string StartCommand = "CREATE TABLE if not exists ListMembers" +
                "(" +
                "Team VARCHAR(50)," +
                "Vus VARCHAR(50)," +
                "FIO TEXT," +
                "_rank TEXT," +
                "Date_year DATE," +
                "Adress TEXT," +
                "WorkPlace TEXT," +
                "TurnoutAdress TEXT," +
                "Company TEXT" +
                ");";

            string request = "select * from ListMembers;";


            SQLiteCommand comm = new SQLiteCommand(StartCommand, connection);
            SQLiteCommand comm1 = new SQLiteCommand(request, connection);

            try
            {
                connection.Open();

                comm.ExecuteNonQuery();

                using (DbDataReader reader = comm1.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            //"Team VARCHAR(50)," +
                            //"Vus VARCHAR(50)," +
                            //"FIO TEXT," +
                            //"_rank TEXT," +
                            //"Date_year DATE," +
                            //"Adress VARCHAR(50)," +
                            //"WorkPlace VARCHAR(50)," +
                            //"TurnoutAdress VARCHAR(50)," +
                            //"Company TEXT" +

                            string GroupName = reader.GetString(0);
                            string _VusNumber = reader.GetString(1);
                            string _FIO = reader.GetString(2);
                            string _rank = reader.GetString(3);
                            string _YearOfBirth = reader.GetString(4).Split(' ')[0];
                            string _HomeAddress = reader.GetString(5);
                            string _PlaceOfWork = reader.GetString(6);
                            string _TurnoutAddress = reader.GetString(7);
                            string _Company = reader.GetString(8);

                            GroupsArray.LastGroupName = GroupName;

                            //Если группа еще не создана, то создать группу и добавить пукт в список групп
                            if (!GroupsArray.IsCreated(GroupName))
                            {
                                GroupsArray.CreateNewGroup(GroupName);
                                EditableControls.GroupList.Items.Add(GroupName);
                            }


                            var member = new Member()
                            {
                                VusNumber = _VusNumber,
                                FIO = _FIO,
                                Rank = _rank,
                                YearOfBirth = _YearOfBirth,
                                HomeAddress = _HomeAddress,
                                PlaceOfWork = _PlaceOfWork,
                                TurnoutAddress = _TurnoutAddress,
                                Company = _Company
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


                            //MessageBox.Show(GroupName);

                            #region
                            //// Индекс (index) столбца Emp_ID в команде SQL.
                            //int empIdIndex = reader.GetOrdinal("Emp_Id"); // 0


                            //long empId = Convert.ToInt64(reader.GetValue(0));

                            //// Столбец Emp_No имеет index = 1.
                            //string empNo = reader.GetString(1);
                            //int empNameIndex = reader.GetOrdinal("Emp_Name");// 2
                            //string empName = reader.GetString(empNameIndex);

                            //// Индекс (index) столбца Mng_Id в команде SQL.
                            //int mngIdIndex = reader.GetOrdinal("Mng_Id");

                            //long? mngId = null;

                            //// Проверить значение данного столбца может являться null или нет.
                            //if (!reader.IsDBNull(mngIdIndex))
                            //{
                            //    mngId = Convert.ToInt64(reader.GetValue(mngIdIndex));
                            //}
                            #endregion
                        }
                    }


                }
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }


        }

        public static void AddMemberToDB(string GroupName, Member member)
        {


            //Запрос на создание БД с таблицей
            string AddMemberCommand = 
                "CREATE TABLE if not exists ListMembers" +
                "(" +
                "Team VARCHAR(50)," +
                "Vus VARCHAR(50)," +
                "FIO TEXT," +
                "_rank TEXT," +
                "Date_year DATE," +
                "Adress TEXT," +
                "WorkPlace TEXT," +
                "TurnoutAdress TEXT," +
                "Company TEXT" +
                ");";


            var date = member.YearOfBirth.Split('.');

            string request = "";
            //ЗАпрос на вставку строки
            try
            {
                request = "insert into ListMembers(Team,Vus,Fio,_rank,Date_year,Adress,Workplace,TurnoutAdress,Company) " +
                  $"values ('{GroupName}', '{member.VusNumber}', '{member.FIO}','{member.Rank}','{date[2]}-{date[1]}-{date[0]}'," +
                  $"'{member.HomeAddress}','{member.PlaceOfWork}','{member.TurnoutAddress}','{member.Company}' );";
            }
            catch (Exception)
            {
                request = "insert into ListMembers(Team,Vus,Fio,_rank,Date_year,Adress,Workplace,TurnoutAdress,Company) " +
                  $"values ('{GroupName}', '{member.VusNumber}', '{member.FIO}','{member.Rank}','{1990}-{01}-{01}'," +
                  $"'{member.HomeAddress}','{member.PlaceOfWork}','{member.TurnoutAddress}','{member.Company}' );";
            }

            SQLiteCommand comm = new SQLiteCommand(AddMemberCommand, connection);
            SQLiteCommand comm1 = new SQLiteCommand(request, connection);





            try
            {
                connection.Open();
                comm.ExecuteNonQuery();
                comm1.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }

        }


        public static void RemoveGroupFromDB(string GroupName)
        {
           
            

            string DeleteRequest = $"delete from listmembers where Team='{GroupName}';";
            SQLiteCommand command = new SQLiteCommand(DeleteRequest, connection);

             


            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public static void RemoveMemberFromDB(string GroupName, Member member)
        {
          

            string DeleteRequest = $"delete from listmembers where Team='{GroupName}' and FIO='{member.FIO}'" +
            $" and vus = '{member.VusNumber}' and _rank = '{member.Rank}' " +
            $"and adress = '{member.HomeAddress}' and workplace = '{member.PlaceOfWork}' and turnoutadress = '{member.TurnoutAddress}' and company = '{member.Company}'";
            
            SQLiteCommand command = new SQLiteCommand(DeleteRequest, connection);
 

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }


        public static void UpdateMemberInfo(string Key, string Value, string InitKey, string InitValue)
        {
        

            string UpdateRequest = $"update listmembers set {Key}='{Value}' where {InitKey} = '{InitValue}';";

            SQLiteCommand command = new SQLiteCommand(UpdateRequest, connection);
 

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public static void UpdateMemberInfo(Member OldMember, Member NewMember)
        {
            var date1 = OldMember.YearOfBirth.Split('.');
            var date2 = NewMember.YearOfBirth.Split('.');


            string UpdateRequest = $"update listmembers " +
                $"set vus = '{NewMember.VusNumber}', fio = '{NewMember.FIO}', _rank = '{NewMember.Rank}', date_year = '{date2[2]}-{date2[1]}-{date2[0]}', " +
                $"adress = '{NewMember.HomeAddress}', workplace = '{NewMember.PlaceOfWork}', turnoutadress = '{NewMember.TurnoutAddress}', company = '{NewMember.Company}'" +
                $"where vus = '{OldMember.VusNumber}' and fio = '{OldMember.FIO}' and _rank = '{OldMember.Rank}' and date_year = '{date1[2]}-{date1[1]}-{date1[0]}' " +
                $"and adress = '{OldMember.HomeAddress}' and workplace = '{OldMember.PlaceOfWork}' and turnoutadress = '{OldMember.TurnoutAddress}' and company = '{OldMember.Company}'";

            SQLiteCommand command = new SQLiteCommand(UpdateRequest, connection);


            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }


    }
}
