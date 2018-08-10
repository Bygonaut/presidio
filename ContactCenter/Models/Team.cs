using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace presidio.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int SupervisorAgentId { get; set; }

    }
}
