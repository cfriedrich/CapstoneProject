using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Abstract
{
    public interface IMeaningRepository
    {
        IQueryable<Meaning> GetMeanings();

        Meaning GetMeaning(int meaningID);
        void AddMeaning(Meaning meaning);
        void SaveMeaning(Meaning meaning);
        Meaning DeleteMeaning(int meaningID);
    }
}
