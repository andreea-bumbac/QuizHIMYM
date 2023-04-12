using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace accesa.src.domain
{
    public class Quest : Entity<long>
    {
        public string question { get; set; }
        public string firstAnswer { get; set; }
        public string secondAnswer { get; set; }
        public string thirdAnswer { get; set; }
        public string correctAnswer { get; set; }

        public Quest(string question, string firstAnswer, string secondAnswer, string thirdAnswer, string correctAnswer)
        {
            this.question = question;
            this.firstAnswer = firstAnswer;
            this.secondAnswer = secondAnswer;
            this.thirdAnswer = thirdAnswer;
            this.correctAnswer = correctAnswer;

        }
    }
}
