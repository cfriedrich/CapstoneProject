using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Concrete
{

    public class EFWordRepository : IWordRepository
    {
         LanguageInformantDbContext db = new LanguageInformantDbContext();

        public IQueryable<Word> GetWords()
        {
            return (from w in db.Words select w);
        }

        public void AddWord(Word word)
        {
            db.Words.Add(word);
            db.SaveChanges();
        }

        public void SaveWord(Word word)
        {

            if (word.WordID == 0)
            {
                db.Words.Add(word);
                db.SaveChanges();
            }
            else
            {
                Word dbEntry = db.Words.Find(word.WordID);
                if (dbEntry != null)
                {
                    dbEntry.Name = word.Name;
                    dbEntry.Description = word.Description;
                    dbEntry.Sound = word.Sound;
                    dbEntry.Meanings = word.Meanings;
                    dbEntry.Language = word.Language;
                    dbEntry.SoundVol = word.SoundVol;
                    db.SaveChanges();
                }
            }
        }

        public Word DeleteWord(int wordID)
        {
            Word dbEntry = db.Words.Find(wordID);
            if (dbEntry != null)
            {
                db.Words.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }

        public void AddMeaning(int wordID, int meaningID)
        {
            Word word = db.Words.Find(wordID);
            Meaning meaning = db.Meanings.Find(meaningID);
            word.Meanings.Add(meaning);
            db.SaveChanges();
        }

        public void RemoveMeaning(int wordID, int meaningID)
        {
            Word word = db.Words.Find(wordID);
            Meaning meaning = db.Meanings.Find(meaningID);
            word.Meanings.Remove(meaning);
            db.SaveChanges();
        }

        public void AddLanguage(int wordID, int languageID)
        {
            Word word = db.Words.Find(wordID);
            Language language = db.Languages.Find(languageID);
            word.Language = language;
            db.SaveChanges();
        }

        public Word GetWord(int wordID)
        {
            Word dbEntry = db.Words.Find(wordID);

            return dbEntry;
        }

        public Word GetWord(string word)
        {
            return (from w in db.Words
                    where w.Name == word
                    select w).FirstOrDefault(); 
        }

        public Language GetLanguage(int languageID)
        {
            return (from l in db.Languages
                    where l.LanguageID == languageID
                    select l).FirstOrDefault();
        }
    }
}
