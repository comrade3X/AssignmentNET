using System.Collections.Generic;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class OrderServices : IServices<Order>
    {
        readonly OrderDao _orderDao = new OrderDao();
        public List<Order> GetAll()
        {
            return _orderDao.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderDao.GetById(id);
        }

        public void Add(Order obj)
        {
            _orderDao.Add(obj);
        }

        public void Delete(Order obj)
        {
            _orderDao.Delete(obj);
        }
    }
}
