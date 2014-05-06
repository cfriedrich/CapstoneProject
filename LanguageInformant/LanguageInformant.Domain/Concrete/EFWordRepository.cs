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

        public IQueryable<Word> GetWords()
        {
            var db = new LanguageInformantDbContext();
            return (from w in db.Words select w);
        }

        public void AddWord(Word word)
        {
            var db = new LanguageInformantDbContext();
            db.Words.Add(word);
            db.SaveChanges();
        }

        public void SaveWord(Word word)
        {
            var db = new LanguageInformantDbContext();

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
            var db = new LanguageInformantDbContext();
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
            var db = new LanguageInformantDbContext();
            Word word = db.Words.Find(wordID);
            Meaning meaning = db.Meanings.Find(meaningID);
            word.Meanings.Add(meaning);
            db.SaveChanges();
        }

        public void DeleteMeaning(int wordID, int meaningID)
        {
            var db = new LanguageInformantDbContext();
            Word word = db.Words.Find(wordID);
            Meaning meaning = db.Meanings.Find(meaningID);
            word.Meanings.Remove(meaning);
            db.SaveChanges();
        }


        public Word GetWord(int wordID)
        {
            var db = new LanguageInformantDbContext();
            Word dbEntry = db.Words.Find(wordID);

            return dbEntry;
        }
    }
}
