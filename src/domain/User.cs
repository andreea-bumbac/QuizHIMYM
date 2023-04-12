using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.domain
{
    public class User : Entity<long>
    {
        public string username { get; set; }
        public string parola { get; set; }

        public int tokens { get; set; } 

        public int intr { get; set; }

        public User(string username, string parola, int tokens, int intr)
        {
            this.username = username;
            this.parola = parola;
            this.tokens = tokens;   
            this.intr = intr;
        }
    }
}










