using System.Collections.Generic;

namespace BlankApp1.Entities
{
    public class Tool : BaseEntity
    {
        public string Label { get; set; }
        public List<Equipment> Equipments { get; set; }
    }
}
