using System.Collections.Generic;

namespace BlankApp1.Entities
{
    public class SpaceType : BaseEntity
    {
        public string Label { get; set; }
        public List<Space> Spaces { get; set; }
    }
}
