using LanguageInformant.Domain.Abstract;
using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Concrete
{
    public class EFMeaningRepository : IMeaningRepository
    {
        private LanguageInformantDbContext context = new LanguageInformantDbContext();

        public IQueryable<Meaning> GetMeanings()
        {
            var db = new LanguageInformantDbContext();
            return (from m in db.Meanings select m);
        }

        public void AddMeaning(Meaning meaning)
        {
            var db = new LanguageInformantDbContext();
            db.Meanings.Add(meaning);
            db.SaveChanges();
        }

        public void SaveMeaning(Meaning meaning)
        {
           if (meaning.MeaningID == 0)
            {
                context.Meanings.Add(meaning);
                context.SaveChanges();
            }
            else
            {
                Meaning dbEntry = context.Meanings.Find(meaning.MeaningID);
                if (dbEntry != null)
                {
                    dbEntry.Name = meaning.Name;
                    dbEntry.Description = meaning.Description;
                    dbEntry.ImageData = meaning.ImageData;
                    dbEntry.ImageMimeType = meaning.ImageMimeType;
                    dbEntry.Words = meaning.Words;
                    context.SaveChanges();
                }
            }
        }

        public Meaning DeleteMeaning(int meaningID)
        {
            var db = new LanguageInformantDbContext();
            Meaning dbEntry = db.Meanings.Find(meaningID);
            if (dbEntry != null)
            {
                db.Meanings.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }



        public Meaning GetMeaning(int meaningID)
        {
            var db = new LanguageInformantDbContext();
            Meaning dbEntry = db.Meanings.Find(meaningID);

            return dbEntry;
        }
    }
}
