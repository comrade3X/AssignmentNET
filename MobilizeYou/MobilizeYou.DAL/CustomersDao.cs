using System.Collections.Generic;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class CustomersDao : IDao<Customer>
    {
        public List<Customer> GetAll()
        {
            using (var db = new MobilizeYouEntities())
            {
                var query = from s in db.Customers.ToList()
                            select new Customer()
                            {
                                Id = s.Id,
                                FullName = s.FullName,
                                IdentityCardNo = s.IdentityCardNo,
                                DriveLicenceNo = s.DriveLicenceNo,
                                PhoneNumber = s.PhoneNumber
                            };
                return query.ToList();
            }
        }

        public Customer GetById(int id)
        {
            using (var db = new MobilizeYouEntities())
            {
                var s = db.Customers.Find(id);
                var customer = new Customer
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    IdentityCardNo = s.IdentityCardNo,
                    DriveLicenceNo = s.DriveLicenceNo,
                    PhoneNumber = s.PhoneNumber
                };
                return customer;
            }
        }

        public void Add(Customer obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Customers.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(Customer obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Customers.Remove(obj);
                db.SaveChanges();
            }
        }

    }
}
