using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLeadGUI
{
    public class MicroleavesInfo
    {
        public string member { get; set; }
        public string token { get; set; }
        public override string ToString()
        {
            return member;
        }
    }
}
