using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.domain
{
    public class Badges : Entity<long>
    {
        public string name { get; set; }
        public int minimumTokens { get; set; }
        
        public Badges(string name, int minimumTokens)
        {
            this.name = name;
            this.minimumTokens = minimumTokens;
        }
    }
}
