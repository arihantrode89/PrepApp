using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private ApplicationDbContext _dbContext;
        public MemberRepository(ApplicationDbContext context)
        { 
            _dbContext = context;
        }
        public async Task<Member> AddMember(Member member)
        {
            try
            {
                await _dbContext.member.AddAsync(member);
                await _dbContext.SaveChangesAsync();
                return member;
            }
            catch (Exception ex)
            {
                return new Member();
            }
        }

        public async Task<List<Member>> GetMembers()
        {
            var data = await _dbContext.member.Include(x => x.BorrowRecords).ToListAsync();
            return data;
        }
    }
}
