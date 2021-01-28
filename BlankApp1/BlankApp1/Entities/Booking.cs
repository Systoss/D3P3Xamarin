namespace BlankApp1.Entities
{
    using System;
    using System.Collections.Generic;
    public class Booking : BaseEntity
    {
        public string Name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public User Organizer { get; set; }
        public Space Space { get; set; }
        public List<User> Participants { get; set; }
    }
}
