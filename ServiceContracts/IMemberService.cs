using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IMemberService
    {
        public Task<List<Member>> GetMembersData();

        public Task<Member> AddMemberData(Member member);
    }
}
