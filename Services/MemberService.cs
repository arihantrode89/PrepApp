using Entities;
using RepositoryContracts;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _member;
        public MemberService(IMemberRepository member)
        {
            _member = member;
        }
        public async Task<Member> AddMemberData(Member member)
        {
            var data = await _member.AddMember(member);
            return data;
        }

        public async Task<List<Member>> GetMembersData()
        {
            var data = await _member.GetMembers();
            return data;
        }
    }
}
