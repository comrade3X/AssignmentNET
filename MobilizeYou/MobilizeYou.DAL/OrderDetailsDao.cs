using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class OrderDetailDetailsDao : IDao<OrderDetail>
    {
        private MobilizeYouEntities _db = new MobilizeYouEntities();
        public OrderDetailDetailsDao()
        {

        }
        public List<OrderDetail> GetAll()
        {
            var query = from s in _db.OrderDetails.ToList()
                        select new OrderDetail
                        {
                            Id = s.Id,
                            Order = s.Order,
                            Product = s.Product,
                            ValidFrom = s.ValidFrom,
                            ValidTo = s.ValidTo,
                            OrderId = s.OrderId,
                            ProductId = s.ProductId
                        };
            return query.ToList();
        }

        public OrderDetail GetById(int id)
        {
            var s = _db.OrderDetails.Find(id);
            var od = new OrderDetail
            {
                Id = s.Id,
                Order = s.Order,
                Product = s.Product,
                ValidFrom = s.ValidFrom,
                ValidTo = s.ValidTo,
                OrderId = s.OrderId,
                ProductId = s.ProductId
            };
            return od;
        }

        public void Add(OrderDetail obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.OrderDetails.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(OrderDetail obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.OrderDetails.Attach(obj);
                db.OrderDetails.Remove(obj);
                db.SaveChanges();
            }
        }

        public void Update(OrderDetail obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("");
            }

            using (var db = new MobilizeYouEntities())
            {
                db.Entry(obj).State = EntityState.Modified;
                // other changed properties
                db.SaveChanges();
            }
        }

        public List<OrderDetail> GetOrderDetailsByDate(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                // Logic: startA <= endB && startB <= endA
                var query = _db.OrderDetails.Where(x => x.ValidFrom <= dateTo && dateFrom <= x.ValidTo).ToList();

                return query.Count == 0 ? null : query;
            }
            catch
            {
                return null;
            }
        }
    }
}
