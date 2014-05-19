using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Concrete
{
    public class EFVocabularyRepository : IVocabularyRepository
    {
        public IQueryable<Vocabulary> GetVocabularies()
        {
            var db = new LanguageInformantDbContext();
            return (from l in db.Vocabularies select l);
        }

        public void AddVocabulary(Vocabulary vocabulary)
        {
            var db = new LanguageInformantDbContext();
            db.Vocabularies.Add(vocabulary);
            db.SaveChanges();
        }

        public void SaveVocabulary(Vocabulary vocabulary)
        {
            var db = new LanguageInformantDbContext();

            if (vocabulary.VocabularyID == 0)
            {
                db.Vocabularies.Add(vocabulary);
                db.SaveChanges();
            }
            else
            {
                Vocabulary dbEntry = db.Vocabularies.Find(vocabulary.VocabularyID);
                if (dbEntry != null)
                {
                    dbEntry.Name = vocabulary.Name;
                    dbEntry.Description = vocabulary.Description;
                    dbEntry.Lesson = vocabulary.Lesson;
                    dbEntry.Words = vocabulary.Words;

                    db.SaveChanges();
                }
            }
        }

        public Vocabulary DeleteVocabulary(int vocabularyID)
        {
            var db = new LanguageInformantDbContext();
            Vocabulary dbEntry = db.Vocabularies.Find(vocabularyID);
            if (dbEntry != null)
            {
                db.Vocabularies.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }



        public Vocabulary GetVocabulary(int vocabularyID)
        {
            var db = new LanguageInformantDbContext();
            Vocabulary dbEntry = db.Vocabularies.Find(vocabularyID);

            return dbEntry;
        }

        public void AddWord(int vocabularyID, int wordID)
        {
            var db = new LanguageInformantDbContext();
            Word word = db.Words.Find(wordID);
            Vocabulary vocabulary = db.Vocabularies.Find(vocabularyID);
            vocabulary.Words.Add(word);
            db.SaveChanges();
        }

        public void RemoveWord(int vocabularyID, int wordID)
        {
            var db = new LanguageInformantDbContext();
            Word word = db.Words.Find(wordID);
            Vocabulary vocabulary = db.Vocabularies.Find(vocabularyID);
            vocabulary.Words.Add(word);
            db.SaveChanges();
        }

    }
}
