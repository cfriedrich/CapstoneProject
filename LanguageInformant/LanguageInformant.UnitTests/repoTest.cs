using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LanguageInformant.Domain.Entities;


namespace LanguageInformant.UnitTests
{
    [TestClass]
    public class repoTest
    {
        Meaning wordmeaning = new Meaning();
        Word target = new Word();
        Word target2 = new Word();
        Language wordlang = new Language();
        Vocabulary vocab = new Vocabulary();
        Scene scene = new Scene();
        Lesson lesson = new Lesson();
        Level level = new Level();
        Language language = new Language();

        public void createWordTest()
        {
            const string NAME = "Test";
            const string DESCRIPTION = "Test";
            const int SOUNDVOL = 2;
            Language lang = new Language();

            target.Name = NAME;
            target.Description = DESCRIPTION;
            target.SoundVol = SOUNDVOL;
            target.Language = lang;
            target.Meanings.Add(wordmeaning);
            target2.Meanings.Add(wordmeaning);

            Assert.AreEqual(NAME, target.Name);
            Assert.AreEqual(DESCRIPTION, target.Description);
            Assert.AreEqual(SOUNDVOL, target.SoundVol);
            Assert.AreEqual(lang, target.Language);
            Assert.AreEqual(target.Meanings, target2.Meanings); 

        }

        public void createMeaningTest()
        {
            const string NAME = "Test";
            const string DESCRIPTION = "Test";
            List<Word> words = new List<Word>();

            wordmeaning.Name = NAME;
            wordmeaning.Description = DESCRIPTION;
            wordmeaning.Words.Add(target);
            wordmeaning.Words.Add(target2);

            Assert.AreEqual(NAME, wordmeaning.Name);
            Assert.AreEqual(DESCRIPTION, wordmeaning.Description);
            Assert.AreEqual(wordmeaning.Words, target.Meanings);
        }

        public void createLanguageTest()
        {
            const string NAME = "Test";
            const string DESCRIPTION = "Test";
            List<Level> levels = new List<Level>();

            language.Name = NAME;
            language.Description = DESCRIPTION;
            language.Levels.Add(level);

            Assert.AreEqual(NAME, language.Name);
            Assert.AreEqual(DESCRIPTION, language.Description);
            Assert.AreEqual(language.Levels, level);
        }

        public void createVocabularyTest()
        {
            const string NAME = "Test";
            const string DESCRIPTION = "Test";
            List<Word> words = new List<Word>();

            wordmeaning.Name = NAME;
            wordmeaning.Description = DESCRIPTION;
            wordmeaning.Words.Add(target);
            wordmeaning.Words.Add(target2);

            Assert.AreEqual(NAME, target.Name);
            Assert.AreEqual(DESCRIPTION, target.Description);
            Assert.AreEqual(wordmeaning.Words, target.Meanings);
        }
    }
}
