using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class Grade
    {
        public double TotalPoints { get; set; }
        public double Score { get; set; }
        public VocabQuiz Quiz { get; set; }
        public MatchingQuiz MatchingQuiz { get; set; }
    }
}