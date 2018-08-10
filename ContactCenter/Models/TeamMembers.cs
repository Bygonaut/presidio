using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace presidio.Models
{
    public class TeamMembers
    {
        [Key]
        public int TeamMemberId { get; set; }
        public int TeamId { get; set; }
        public int AgentId { get; set; }
        
    }
}
