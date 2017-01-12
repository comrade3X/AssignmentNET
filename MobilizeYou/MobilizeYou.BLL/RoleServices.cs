using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobilizeYou.DAL;
using MobilizeYou.DTO;

namespace MobilizeYou.BLL
{
    public class RoleServices : IServices<Role>
    {
        private RoleDao _roleDao = new RoleDao();

        public List<Role> GetAll()
        {
            return _roleDao.GetAll();
        }

        public Role GetById(int id)
        {
            return _roleDao.GetById(id);
        }

        public void Add(Role obj)
        {
            _roleDao.Add(obj);
        }

        public void Delete(Role obj)
        {
            _roleDao.Delete(obj);
        }

        public void Update(Role obj)
        {
            _roleDao.Update(obj);
        }
    }
}
