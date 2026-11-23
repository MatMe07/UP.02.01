using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP._02._01
{
    public  class UserData
    {
        public static User User { get; set; } = null;
        public static bool IsAuth => User != null;

    }
}
