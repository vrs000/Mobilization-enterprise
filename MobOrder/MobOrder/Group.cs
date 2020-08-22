using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MobOrder
{
    public class Group
    {     

        public string Id { get; set; }

        public List<Member> members { get; set; }

        public void AddMember(Member member)
        {
            members.Add(member);
        }

        public void RemoveMember(Member member)
        {
            members.Remove(member);
        }


        public Group(string GroupName)
        {
            Id = GroupName;
            members = new List<Member>();
        }

    }
}
