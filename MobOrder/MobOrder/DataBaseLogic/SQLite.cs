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

        private static SQLiteCommand command;
        private static SQLiteCommand command1;

        public static void FirstInitialize()
        {
            if (!File.Exists(database))
            {
                SQLiteConnection.CreateFile(database);
            }


            string StartCommand = DataBaseCommands.CreateTableListMembers();
            string request = DataBaseCommands.SelectAll();


            command = new SQLiteCommand(StartCommand, connection);
            command1 = new SQLiteCommand(request, connection);


            try
            {
                connection.Open();

                command.ExecuteNonQuery();

                using (DbDataReader reader = command1.ExecuteReader())
                {
                    string GroupName, _VusNumber, _FIO,
                           _rank, _YearOfBirth, _HomeAddress,
                           _PlaceOfWork, _TurnoutAddress, _Company;

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            GroupName = reader.GetString(0);
                            _VusNumber = reader.GetString(1);
                            _FIO = reader.GetString(2);
                            _rank = reader.GetString(3);
                            _YearOfBirth = reader.GetString(4).Split(' ')[0];
                            _HomeAddress = reader.GetString(5);
                            _PlaceOfWork = reader.GetString(6);
                            _TurnoutAddress = reader.GetString(7);
                            _Company = reader.GetString(8);

                            GroupsArray.LastGroupName = GroupName;

                            
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
                            {
                                EditableControls.AddMemberToTab(GroupsArray.LastGroupName, member);
                            }
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
            string AddMemberCommand = DataBaseCommands.CreateTableListMembers();
            string request = DataBaseCommands.InsertMemberIntoTable(GroupName, member);


            command = new SQLiteCommand(AddMemberCommand, connection);
            command1 = new SQLiteCommand(request, connection);


            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                command1.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }

        }

        public static void RemoveGroupFromDB(string GroupName)
        {
            string DeleteRequest = DataBaseCommands.RemoveGroup(GroupName);


            command = new SQLiteCommand(DeleteRequest, connection);


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
            string DeleteRequest = DataBaseCommands.RemoveMember(GroupName, member);


            command = new SQLiteCommand(DeleteRequest, connection);


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
            string UpdateRequest = DataBaseCommands.UpdateMemberInfo(Key, Value, InitKey, InitValue);


            command = new SQLiteCommand(UpdateRequest, connection);


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
            string UpdateRequest = DataBaseCommands.UpdateMemberInfo(OldMember, NewMember);


            command = new SQLiteCommand(UpdateRequest, connection);


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
