using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Abstract
{
    public interface IVocabularyRepository
    {
        IQueryable<Vocabulary> GetVocabularies();

        Vocabulary GetVocabulary(int vocabularyID);
        void AddVocabulary(Vocabulary vocabulary);
        void SaveVocabulary(Vocabulary vocabulary);
        Vocabulary DeleteVocabulary(int vocabularyID);
        void AddWord(int vocabularyID, int wordID);
        void RemoveWord(int vocabularyID, int wordID);

    }
}
