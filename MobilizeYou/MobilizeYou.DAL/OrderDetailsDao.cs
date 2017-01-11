using System.Collections.Generic;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class OrderDetailDetailsDao : IDao<OrderDetail>
    {
        public List<OrderDetail> GetAll()
        {
            using (var db = new MobilizeYouEntities())
            {
                var query = from s in db.OrderDetails.ToList()
                            select new OrderDetail
                            {
                                Id = s.Id,
                                Order = s.Order,
                                Product = s.Product,
                                ValidFrom = s.ValidFrom,
                                ValidTo = s.ValidTo
                            };
                return query.ToList();
            }
        }

        public OrderDetail GetById(int id)
        {
            using (var db = new MobilizeYouEntities())
            {
                var s = db.OrderDetails.Find(id);
                var od = new OrderDetail
                {
                    Id = s.Id,
                    Order = s.Order,
                    Product = s.Product,
                    ValidFrom = s.ValidFrom,
                    ValidTo = s.ValidTo
                };
                return od;
            }
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
                db.OrderDetails.Remove(obj);
                db.SaveChanges();
            }
        }

    }
}
