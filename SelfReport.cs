using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class SelfReport
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string ReportDate { get; set; }
        public string ReportContent { get; set; }
    }
}
