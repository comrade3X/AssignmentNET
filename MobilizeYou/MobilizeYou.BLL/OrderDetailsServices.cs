using System.Collections.Generic;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class OrderDetailsServices : IServices<OrderDetail>
    {
        readonly OrderDetailDetailsDao _orderDetailDao = new OrderDetailDetailsDao();
        public List<OrderDetail> GetAll()
        {
            return _orderDetailDao.GetAll();
        }

        public OrderDetail GetById(int id)
        {
            return _orderDetailDao.GetById(id);
        }

        public void Add(OrderDetail obj)
        {
            _orderDetailDao.Add(obj);
        }

        public void Delete(OrderDetail obj)
        {
            _orderDetailDao.Delete(obj);
        }

        public void Update(OrderDetail obj)
        {
            _orderDetailDao.Update(obj);
        }
    }
}
