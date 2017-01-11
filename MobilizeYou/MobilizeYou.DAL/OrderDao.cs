using System.Collections.Generic;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class OrderDao : IDao<Order>
    {
        public List<Order> GetAll()
        {
            using (var db = new MobilizeYouEntities())
            {
                var query = from s in db.Orders.ToList()
                            select new Order
                            {
                                Id = s.Id,
                                Customer = s.Customer,
                                Seller = s.Seller,
                                TotalPrice = s.TotalPrice,
                                CreatedDate = s.CreatedDate
                            };
                return query.ToList();
            }
        }

        public Order GetById(int id)
        {
            using (var db = new MobilizeYouEntities())
            {
                var s = db.Orders.Find(id);
                var order = new Order
                {
                    Id = s.Id,
                    Customer = s.Customer,
                    Seller = s.Seller,
                    TotalPrice = s.TotalPrice,
                    CreatedDate = s.CreatedDate
                };
                return order;
            }
        }

        public void Add(Order obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Orders.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(Order obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Orders.Remove(obj);
                db.SaveChanges();
            }
        }

    }
}
