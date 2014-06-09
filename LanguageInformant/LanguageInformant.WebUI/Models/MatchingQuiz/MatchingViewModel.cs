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
        //This model is used to create and grade the matching quiz model
        //repositories being used
        private IMeaningRepository meaningrepo = new EFMeaningRepository();
        private IWordRepository wordrepo = new EFWordRepository();

        //user logged in to track progress
        Member member = new Member();

        //create the matching quiz
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

        //Populate the quiz with images and answers
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
            
            //each image is given an answer
            images[0].AddChoice(new MatchingAnswers() { IsAnswer = false, Text = matchingWord1.Name, Id = matchingWord1.Meanings.First().MeaningID });
            images[1].AddChoice(new MatchingAnswers() { IsAnswer = true, Text = matchingWord2.Name, Id = matchingWord2.Meanings.First().MeaningID });
            images[2].AddChoice(new MatchingAnswers() { IsAnswer = false, Text = matchingWord3.Name, Id = matchingWord3.Meanings.First().MeaningID });
            images[3].AddChoice(new MatchingAnswers() { IsAnswer = false, Text = matchingWord4.Name, Id = matchingWord4.Meanings.First().MeaningID });
            return images;
        }

        //Quiz Grading (Post)
        public Grade Grade(MatchingQuiz toBeGradedQuiz)
        {
            //get quiz information
            var persistedQuiz = GetQuiz();
            //create a grade to store quiz in
            var grade = new Grade() { MatchingQuiz = persistedQuiz };

            foreach (var question in toBeGradedQuiz.Images)
            {
                //look for image based off id of the image 
                var persistedQuestion = (from q in persistedQuiz.Images
                                         where q.Id == question.Id
                                         select q).FirstOrDefault();

                if (persistedQuestion != null)
                {
                    foreach (var choice in question.Choices)
                    {
                        //look for the answer based off id of radio button
                        var persistedChoice = (from c in persistedQuestion.Choices
                                               where c.Id == choice.Id
                                               select c).FirstOrDefault();

                        // sets the user choice in the actual exam 
                        persistedChoice.IsSelected = true;

                        if (persistedChoice.IsAnswer)
                        {
                            //add points to score
                            grade.Score += persistedQuestion.Point;
                        }
                    }
                }
            }
            return grade;
        }
    }
}