using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class MatchingImages
    {
        public int Id { get; set; }
        private IList<MatchingAnswers> _choices = new List<MatchingAnswers>();
        public string Text { get; set; }
        public double Point { get; set; }
        public int OrderNumber { get; set; }

        public IList<MatchingAnswers> Choices
        {
            get { return _choices; }
            set { _choices = value; }
        }

        public void AddChoice(MatchingAnswers answer)
        {
            _choices.Add(answer);
            answer.Image = this;

        }
    }
}