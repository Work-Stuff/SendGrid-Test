using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendGrid_Test
{
    [Flags]
    public enum EnumEmailState : int
    {
        SENT = 1 << 0,
        INVALID = 1 << 1,
        SPAM = 1 << 2,
        BOUNCED = 1 << 3
    }
}
