using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Concrete;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LanguageInformant.WebUI.Models
{
    public class MatchingViewModel
    {
        private IMeaningRepository meaningrepo = new EFMeaningRepository();
        private IWordRepository wordrepo = new EFWordRepository();

         

        //user logged in to track progress
        Member member = new Member();

        public MatchingQuiz GetQuiz()
        {
            Word theWord = wordrepo.GetWord("pato");

            var quiz = new MatchingQuiz() 
            { 
                Id = 200, 
                Name = "Vocabulary Matching Lesson",
                Word = theWord.Name
            };
            quiz.AddImage(GetImages());

            return quiz;
        }

        private IList<MatchingImages> GetImages()
        {

            Word matchingWord1 = wordrepo.GetWord("perro");
            Word matchingWord2 = wordrepo.GetWord("pato");
            Word matchingWord3 = wordrepo.GetWord("abrigo");
            Word matchingWord4 = wordrepo.GetWord("camisa");
            
            var images = new List<MatchingImages>()
                {
                    new MatchingImages() 
                    { 
                        Text = matchingWord1.Name, 
                        Point = 10, 
                        Id = matchingWord1.Meanings.First().MeaningID, 
                        OrderNumber = 0
                    }, 
                    new MatchingImages() 
                    { 
                        Text = matchingWord2.Name, 
                        Point = 10, 
                        Id = matchingWord2.Meanings.First().MeaningID, 
                        OrderNumber = 1
                    },
                    new MatchingImages()
                    { 
                        Text = matchingWord3.Name, 
                        Point = 10, 
                        Id = matchingWord3.Meanings.First().MeaningID, 
                        OrderNumber = 2
                    },
                    new MatchingImages() 
                    { 
                        Text = matchingWord4.Name, 
                        Point = 10, 
                        Id = matchingWord4.Meanings.First().MeaningID, 
                        OrderNumber = 3
                    }
                };
            
            images[0].AddChoice(new MatchingAnswers() { IsAnswer = false, Text = matchingWord1.Name, Id = matchingWord1.Meanings.First().MeaningID });
            images[1].AddChoice(new MatchingAnswers() { IsAnswer = true, Text = matchingWord2.Name, Id = matchingWord2.Meanings.First().MeaningID });
            images[2].AddChoice(new MatchingAnswers() { IsAnswer = false, Text = matchingWord3.Name, Id = matchingWord3.Meanings.First().MeaningID });
            images[3].AddChoice(new MatchingAnswers() { IsAnswer = false, Text = matchingWord4.Name, Id = matchingWord4.Meanings.First().MeaningID });
            return images;
        }

        public Grade Grade(MatchingQuiz toBeGradedQuiz)
        {
            var persistedQuiz = GetQuiz();
            var grade = new Grade() { MatchingQuiz = persistedQuiz };

            foreach (var question in toBeGradedQuiz.Images)
            {
                var persistedQuestion = (from q in persistedQuiz.Images
                                         where q.Id == question.Id
                                         select q).FirstOrDefault();

                if (persistedQuestion != null)
                {
                    foreach (var choice in question.Choices)
                    {
                        var persistedChoice = (from c in persistedQuestion.Choices
                                               where c.Id == choice.Id
                                               select c).FirstOrDefault();

                        // sets the user choice in the actual exam fetched from database! 
                        persistedChoice.IsSelected = true;

                        if (persistedChoice.IsAnswer)
                        {
                            grade.Score += persistedQuestion.Point;
                        }
                    }
                }
            }

            return grade;
        }

    }
}