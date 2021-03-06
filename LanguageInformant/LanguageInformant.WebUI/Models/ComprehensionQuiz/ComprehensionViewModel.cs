﻿using LanguageInformant.Domain.Concrete;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageInformant.WebUI.Models
{
    public class ComprehensionViewModel
    {
        //This model is used to create and grade the comprehension quiz model
        //user logged in to track progress
        Member member = new Member();

        //create the vocab quiz
        public VocabQuiz GetQuiz()
        {
            var quiz = new VocabQuiz() 
            { 
                Id = 100, 
                Name = " Vocabulary Translation Lesson" 
            };
            quiz.AddQuestion(GetQuestions());

            return quiz;
        }

        //populate vocab quiz with questions an answers
        private IList<VocabQuestions> GetQuestions()
        {
            var questions = new List<VocabQuestions>()
                {
                    new VocabQuestions() 
                    { 
                        Text = "Perro", 
                        Point = 10, 
                        Id = 1, 
                        OrderNumber = 0
                    }, 
                    new VocabQuestions() 
                    { 
                        Text = "Camisa", 
                        Point = 5, 
                        Id =2, 
                        OrderNumber = 1
                    },
                    new VocabQuestions() 
                    { 
                        Text="Mesa", 
                        Point =5, 
                        Id=3, 
                        OrderNumber = 2
                    },
                    new VocabQuestions() 
                    { 
                        Text="Casa", 
                        Point =5, 
                        Id=4, 
                        OrderNumber = 3
                    },
                    new VocabQuestions() 
                    { 
                        Text="Gato", 
                        Point =5, 
                        Id=5, 
                        OrderNumber = 4
                    }
                };

            //question1 answers
            questions[0].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Buffalo", Id = 1 });
            questions[0].AddChoice(new VocabAnswers() { IsAnswer = true, Text = "Dog", Id = 2 });
            questions[0].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Person", Id = 3 });
            questions[0].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Lamp", Id = 4 });
            //question2 answers
            questions[1].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Lightbulb", Id = 5 });
            questions[1].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Sun", Id = 6 });
            questions[1].AddChoice(new VocabAnswers() { IsAnswer = true, Text = "Shirt", Id = 7 });
            questions[1].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Chair", Id = 8 });
            //question3 answers
            questions[2].AddChoice(new VocabAnswers() { IsAnswer = true, Text = "Table", Id = 9 });
            questions[2].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Bathroom", Id = 10 });
            questions[2].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Cold", Id = 11 });
            questions[2].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Bright", Id = 12 });
            //question4 answers
            questions[3].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Doctor", Id = 13 });
            questions[3].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Car", Id = 14 });
            questions[3].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Love", Id = 15 });
            questions[3].AddChoice(new VocabAnswers() { IsAnswer = true, Text = "House", Id = 16 });
            //question5 answers
            questions[4].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Bird", Id = 17 });
            questions[4].AddChoice(new VocabAnswers() { IsAnswer = true, Text = "Cat", Id = 18 });
            questions[4].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Hole", Id = 19 });
            questions[4].AddChoice(new VocabAnswers() { IsAnswer = false, Text = "Hello", Id = 20 });
           
            return questions;
        }

        //grade the quiz
        public Grade Grade(VocabQuiz toBeGradedQuiz)
        {
            //get created quiz
            var persistedQuiz = GetQuiz()
            //create grade to hold quiz
            var grade = new Grade() { Quiz = persistedQuiz };

            foreach (var question in toBeGradedQuiz.Questions)
            {
                //look up questions based off id of the question
                var persistedQuestion = (from q in persistedQuiz.Questions
                                         where q.Id == question.Id
                                         select q).FirstOrDefault();

                if (persistedQuestion != null)
                {
                    foreach (var choice in question.Choices)
                    {
                        //look up answers based of id of answers
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