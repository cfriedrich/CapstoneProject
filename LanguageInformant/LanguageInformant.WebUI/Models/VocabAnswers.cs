using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class VocabAnswers
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsAnswer { get; set; }
        private VocabQuestions _questions = new VocabQuestions();
        public bool IsSelected { get; set; }

        public VocabQuestions Question
        {
            get { return _questions; }
            set { _questions = value; }
        }
    }
}