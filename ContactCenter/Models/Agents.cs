using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace presidio.Models
{
    public class Agents
    {
        [Key]
        public int AgentId  { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int TeamId { get; set; }
        public int IsSupervisor { get; set; }
    }
}
