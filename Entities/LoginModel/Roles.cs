using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LoginModel
{
    public class Roles
    {
        public int Id { get; set; }

        public string RoleName {  get; set; }

        public IEnumerable<UserModel> Users { get; set; }
    }
}
