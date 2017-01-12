using System;
using System.Collections.Generic;
using System.Linq;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class OrderDetailsServices : IServices<OrderDetail>
    {
        OrderDetailDetailsDao _orderDetailDao = new OrderDetailDetailsDao();
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

        public List<OrderDetail> GetOrderDetailsByDate(DateTime dateFrom, DateTime dateTo)
        {
            var res = _orderDetailDao.GetOrderDetailsByDate(dateFrom, dateTo);
            return res;
        }

        public List<OrderDetailsDto> Search(DateTime dateFrom, DateTime dateTo, string cat, string make, string name)
        {
            var productServices = new ProductServices();
            var listProduct = productServices.GetAll();
            IEnumerable<OrderDetailsDto> enumerable;

            if (listProduct == null)
            {
                return new List<OrderDetailsDto>();
            }

            var listOrderDetails = GetOrderDetailsByDate(dateFrom, dateTo);
            if (listOrderDetails == null)
            {
                var res1 = from s in listProduct
                           select new OrderDetailsDto()
                           {
                               Product = s,
                               Status = "Available"
                           };

                enumerable = res1;
            }
            else
            {
                var linq = from product in listProduct
                           join orderDetail in listOrderDetails on product.Id equals orderDetail.Product.Id
                           into ps
                           from p in ps.DefaultIfEmpty()
                           select new OrderDetailsDto
                           {
                               OrderDetailId = p == null ? 0 : p.Id,
                               Product = product,
                               Type = p == null ? "" : p.Product.Category.Name,
                               ProductName = p == null ? "" : p.Product.Name,
                               From = p == null ? "" : p.ValidFrom.ToString("dd MMM yyyy"),
                               To = p == null ? "" : p.ValidTo.ToString("dd MMM yyyy"),
                               Customer = p == null ? "" : p.Order.Customer.FullName,
                               OrderDate = p == null ? "" : p.Order.CreatedDate.ToString("dd MMM yyyy"),
                               Status = p == null ? "Available" : "Hired"
                           };

                enumerable = linq;
            }

            if (!string.IsNullOrEmpty(cat) && !"All".Equals(cat))
            {
                enumerable = enumerable.Where(x => cat.Equals(x.Product.Category.Name));
            }

            if (!string.IsNullOrEmpty(make) && !"All".Equals(make))
            {
                enumerable = enumerable.Where(x => make.Equals(x.Product.Make));
            }

            if (!string.IsNullOrEmpty(name))
            {
                enumerable = enumerable.Where(x => x.Product.Name.ToLower().Contains(name.ToLower()));
            }

            enumerable = enumerable.OrderBy(x => x.Status)
                    .ThenBy(x => x.Type)
                    .ThenBy(x => x.Product.Name).ToList();

            return enumerable.ToList();
        }
    }
}
