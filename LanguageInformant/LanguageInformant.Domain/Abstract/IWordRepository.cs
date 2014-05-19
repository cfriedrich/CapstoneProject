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

        Word GetWord(string word);
        Word GetWord(int wordID);
        
        Language GetLanguage(int languageID);
        void AddWord(Word word);
        void SaveWord(Word word);
        Word DeleteWord(int wordID);
        void AddMeaning(int wordID, int meaningID);
        void AddLanguage(int wordID, int languageID);
        void RemoveMeaning(int wordID, int meaningID);
    }
}
