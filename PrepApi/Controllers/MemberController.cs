using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace PrepApi.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        { 
            _memberService = memberService;
        }
        [HttpGet]
        [Route("getallmember")]
        public async Task<IActionResult> GetAllMembers()
        {
            var data = await _memberService.GetMembersData();
            return Json(data);
        }

        [HttpGet]
        [Route("addmember")]
        public async Task<IActionResult> AddMember(Member member)
        { 
            var data = await _memberService.AddMemberData(member);
            return Json(data);
        }
    }
}
