using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cool_Text_Generator
{
    class CoolName
    {
        public CoolName(string Type, string Name)
        {
            this.Type = Type;
            this.Name = Name;
        }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
