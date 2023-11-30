using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class Relationship
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SupervisorId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
