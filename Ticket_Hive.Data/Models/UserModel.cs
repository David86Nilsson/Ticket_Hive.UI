using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket_Hive.Data.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public List<EventModel> Events { get; set; } = new();
    }
}
