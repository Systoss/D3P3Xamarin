using System;
using System.Collections.Generic;
using System.Text;

namespace BlankApp1.Entities
{
    public class Equipment
    {
        public int IdSpace { get; set; }
        public Space Space{ get; set; }
        public int idTool { get; set; }
        public Tool Tool { get; set; }
        public int Quantity { get; set; }
    }
}
