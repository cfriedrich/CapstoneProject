using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Abstract
{
    public interface IWordRepository
    {
        IQueryable<Word> GetWords();

        Word GetWord(int wordID);
        void AddWord(Word word);
        void SaveWord(Word word);
        Word DeleteWord(int wordID);
    }
}
