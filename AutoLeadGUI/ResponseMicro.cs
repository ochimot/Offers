using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLeadGUI
{
    public class ResponseMicro
    {
        public string error { get; set; }
        public string code { get; set; }
        public List<string> data { get; set; }
        public List<string> advanced_geo { get; set; }
    }
    public class PutData
    {
        public List<string> geo { get; set; }
        public List<string> advanced_geo { get; set; }
    }
}
