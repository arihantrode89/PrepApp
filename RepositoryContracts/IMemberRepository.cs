using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IMemberRepository
    {
        public Task<List<Member>> GetMembers();

        public Task<Member> AddMember(Member member);
    }
}
