using System.Collections.Generic;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class MembershipServices : IServices<Membership>
    {
        readonly MembershipDao _membershipDao = new MembershipDao();
        public List<Membership> GetAll()
        {
            return _membershipDao.GetAll();
        }

        public Membership GetById(int id)
        {
            return _membershipDao.GetById(id);
        }

        public void Add(Membership obj)
        {
            _membershipDao.Add(obj);
        }

        public void Delete(Membership obj)
        {
            _membershipDao.Delete(obj);
        }

        public void Update(Membership obj)
        {
            _membershipDao.Update(obj);
        }
    }
}
