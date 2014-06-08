using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class VocabQuiz
    {
        public int Id { get; set; }
        private IList<VocabQuestions> _questions = new List<VocabQuestions>();
        public string Name { get; set; }

        public IList<VocabQuestions> Questions
        {
            get { return _questions; }
            set { _questions = value; }
        }

        public void AddQuestion(IList<VocabQuestions> questions)
        {
            foreach (var question in questions)
            {
                AddQuestion(question);
            }
        }

        public void AddQuestion(VocabQuestions question)
        {
            _questions.Add(question);
        }

        public double TotalPoints
        {
            get
            {
                return (from q in _questions
                        select q.Point).Sum();
            }
        }
    }
}