using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Staff_Monitor_Engagement
{
    public class Meeting
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SupervisorId { get; set; }
        public string MeetingDate { get; set; }
        public string MeetingSubject { get; set; }
        public string MeetingNotes { get; set; }
    }
}
