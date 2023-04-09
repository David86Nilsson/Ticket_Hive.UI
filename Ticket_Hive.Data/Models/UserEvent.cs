using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket_Hive.Data.Models
{
    public class UserEvent
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public int EventId { get; set; }
        public EventModel Event { get; set; }
    }
}
