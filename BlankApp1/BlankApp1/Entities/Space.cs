using BlankApp1.Entities;
using System.Collections.Generic;

namespace BlankApp1.Entities
{
    public class Space : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SpaceType Type { get; set; }
        public int Capacity { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Equipment> Equipments { get; set; }
    }
}
