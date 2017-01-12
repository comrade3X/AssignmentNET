using System.Collections.Generic;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class CustomersSvervices : IServices<Customer>
    {
        readonly CustomersDao _customerDao = new CustomersDao();
        public List<Customer> GetAll()
        {
            return _customerDao.GetAll();
        }

        public Customer GetById(int id)
        {
            return _customerDao.GetById(id);
        }

        public void Add(Customer obj)
        {
            _customerDao.Add(obj);
        }

        public void Delete(Customer obj)
        {
            _customerDao.Delete(obj);
        }

        public void Update(Customer obj)
        {
            _customerDao.Update(obj);
        }
    }
}
