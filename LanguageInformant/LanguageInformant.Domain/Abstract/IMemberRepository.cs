using LanguageInformant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageInformant.Domain.Abstract
{
    public interface IMemberRepository
    {
        IQueryable<Member> GetMembers();

        Member GetMember(int memberID);

        Member GetMember(string Name);
        void AddMember(Member member);
        void SaveMember(Member member);
        Member DeleteMember(int memberID);
    }
}
