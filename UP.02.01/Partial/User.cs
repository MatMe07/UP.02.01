using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP._02._01
{
    public partial class User
    {
        public bool IsAdmin => RoleId == 3;

        public string FIO => $"{FirstName} {LastName} {MiddleName}";
    }
}
