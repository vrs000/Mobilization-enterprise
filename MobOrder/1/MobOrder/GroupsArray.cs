using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobOrder
{
    public class GroupsArray
    {
        public static List<Group> Groups { get; set; } = new List<Group>();

        //Название группы в которую будем осуществляться добавление нового человека
        public static string LastGroupName { get; set; }


        //Создать новую группу
        public static void CreateNewGroup(string GroupName)
        {
            Groups.Add(new Group(GroupName));

        }

        //Удалить группу
        public static void RemoveGroup(string GroupName)
        {
            int Number = 0;

            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Id == GroupName)
                    Number = i;
            }

            Groups.RemoveAt(Number);
        }



        //Найти группу по id
        public static Group FindGroup(string GroupName)
        {
            Group result = new Group("NONE");
            foreach (var group in Groups)
            {
                if (group.Id == GroupName)
                    result = group;
            }

            return result;
        }


    
        //Проверяет создана ли группа с именем GroupName
        //Если да то возвращает true, иначе false
        public static bool IsCreated(string GroupName)
        {
            foreach (var group in Groups)
            {
                if (group.Id == GroupName)
                    return true;
            }


            return false;
        }

        
        
        //Добавить человека в группу
        public static void AddMemberToTheGroup(string GroupName, Member member)
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Id == GroupName)
                {
                    Groups[i].AddMember(member);
                }
            }
        }



    }
}
