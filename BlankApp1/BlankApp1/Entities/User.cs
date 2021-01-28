using System.Collections.Generic;

namespace BlankApp1.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
