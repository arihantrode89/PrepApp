using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LoginModel
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [ForeignKey("roleid")]
        public int RoleId {  get; set; }
        public Roles Role { get; set; }

        public Password Password { get; set; }


    }
}
