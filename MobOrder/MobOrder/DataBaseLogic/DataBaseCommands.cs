using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobOrder
{
    public static class DataBaseCommands
    {
        public static string CreateTableListMembers()
        {
            string query = "CREATE TABLE if not exists ListMembers" +
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
            return query;
        }

        public static string SelectAll()
        {
            string query = "select * from ListMembers;";
            return query;
        }

        public static string InsertMemberIntoTable(string GroupName, Member member)
        {
            string query = "";

            var date = member.YearOfBirth.Split('.');

            try
            {
                query = "insert into ListMembers(Team,Vus,Fio,_rank,Date_year,Adress,Workplace,TurnoutAdress,Company) " +
                  $"values ('{GroupName}', '{member.VusNumber}', '{member.FIO}','{member.Rank}','{date[2]}-{date[1]}-{date[0]}'," +
                  $"'{member.HomeAddress}','{member.PlaceOfWork}','{member.TurnoutAddress}','{member.Company}' );";
            }
            catch (Exception)
            {
                query = "insert into ListMembers(Team,Vus,Fio,_rank,Date_year,Adress,Workplace,TurnoutAdress,Company) " +
                  $"values ('{GroupName}', '{member.VusNumber}', '{member.FIO}','{member.Rank}','{1990}-{01}-{01}'," +
                  $"'{member.HomeAddress}','{member.PlaceOfWork}','{member.TurnoutAddress}','{member.Company}' );";
            }


            return query;
        }

        public static string RemoveGroup(string GroupName)
        {
            string query = $"delete from listmembers where Team='{GroupName}';";
            return query;
        }

        public static string RemoveMember(string GroupName, Member member)
        {
            string query = $"delete from listmembers where Team='{GroupName}' and FIO='{member.FIO}'" +
            $" and vus = '{member.VusNumber}' and _rank = '{member.Rank}' " +
            $"and adress = '{member.HomeAddress}' and workplace = '{member.PlaceOfWork}' " +
            $"and turnoutadress = '{member.TurnoutAddress}' and company = '{member.Company}'";

            return query;
        }

        public static string UpdateMemberInfo(string Key, string Value, string InitKey, string InitValue)
        {
            string query = $"update listmembers set {Key}='{Value}' where {InitKey} = '{InitValue}';";
            return query;
        }
        public static string UpdateMemberInfo(Member OldMember, Member NewMember)
        {
            var date1 = OldMember.YearOfBirth.Split('.');
            var date2 = NewMember.YearOfBirth.Split('.');


            string query = $"update listmembers " +
                $"set vus = '{NewMember.VusNumber}', fio = '{NewMember.FIO}', " +
                $"_rank = '{NewMember.Rank}', date_year = '{date2[2]}-{date2[1]}-{date2[0]}', " +
                $"adress = '{NewMember.HomeAddress}', workplace = '{NewMember.PlaceOfWork}', " +
                $"turnoutadress = '{NewMember.TurnoutAddress}', company = '{NewMember.Company}'" +
                $"where vus = '{OldMember.VusNumber}' and fio = '{OldMember.FIO}' " +
                $"and _rank = '{OldMember.Rank}' and date_year = '{date1[2]}-{date1[1]}-{date1[0]}' " +
                $"and adress = '{OldMember.HomeAddress}' and workplace = '{OldMember.PlaceOfWork}' " +
                $"and turnoutadress = '{OldMember.TurnoutAddress}' and company = '{OldMember.Company}'";


            return query;
        }


    }

}
