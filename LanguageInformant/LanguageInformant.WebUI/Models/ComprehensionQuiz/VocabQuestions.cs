using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class VocabQuestions
    {
        public int Id { get; set; }
        private IList<VocabAnswers> _choices = new List<VocabAnswers>();
        public string Text { get; set; }
        public double Point { get; set; }
        public int OrderNumber { get; set; }

        public IList<VocabAnswers> Choices
        {
            get { return _choices; }
            set { _choices = value; }
        }

        public void AddChoice(VocabAnswers answer)
        {
            _choices.Add(answer);
            answer.Question = this;

        }
    }
}