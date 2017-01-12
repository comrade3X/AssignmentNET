using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
                db.Customers.Attach(obj);
                db.Customers.Remove(obj);
                db.SaveChanges();
            }
        }

        public void Update(Customer obj)
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
    }
}
