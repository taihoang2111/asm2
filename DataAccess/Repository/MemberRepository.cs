using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository: IMemberRepository
    {
        public MemberObject GetMemberByID(int memberID) => MemberDAO.Instance.GetMemberObjectByID(memberID);
        public MemberObject GetMemberByLogin(string email,string password) 
            => MemberDAO.Instance.GetMemberbyLogin(email,password);
        public IEnumerable<MemberObject> GetMemberObjects() => MemberDAO.Instance.GetMemberObjectsList();
        public void InsertMember(MemberObject member)=> MemberDAO.Instance.AddNew(member);
        public void DeleteMember(int MemberID) => MemberDAO.Instance.Delete(MemberID);
        public void UpdateMember(MemberObject member) => MemberDAO.Instance.Update(member);
        public List<MemberObject> Search(Predicate<MemberObject> predicate) => MemberDAO.Instance.Search(predicate);
    }
}
