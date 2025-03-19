using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LoginModel
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        public string PasswordHash {  get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

    }
}
