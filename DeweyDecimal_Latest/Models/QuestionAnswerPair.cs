using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweyDecimal_Latest.Models
{
    internal class QuestionAnswerPair
    {
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }

        public QuestionAnswerPair(string question, string correctAnswer)
        {
            Question = question;
            CorrectAnswer = correctAnswer;
        }
    }
}
