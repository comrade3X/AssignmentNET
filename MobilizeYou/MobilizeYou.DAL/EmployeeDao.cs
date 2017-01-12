using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;


namespace MobilizeYou.DAL
{
    using DTO;
    public class EmployeeDao : IDao<Employee>
    {
        public List<Employee> GetAll()
        {
            using (var db = new MobilizeYouEntities())
            {
                var query = from s in db.Employees.ToList()
                            select new Employee
                            {
                                Id = s.Id,
                                FullName = s.FullName,
                                Dob = s.Dob,
                                Sex = s.Sex,
                                Address = s.Address,
                                PhoneNumber = s.PhoneNumber,
                                JobType = s.JobType
                            };
                return query.ToList();
            }
        }

        public Employee GetById(int id)
        {
            using (var db = new MobilizeYouEntities())
            {
                var s = db.Employees.Find(id);
                var emp = new Employee
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Dob = s.Dob,
                    Sex = s.Sex,
                    Address = s.Address,
                    PhoneNumber = s.PhoneNumber,
                    JobType = s.JobType
                };
                return emp;
            }
        }

        public void Add(Employee obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Employees.Add(obj);
                db.SaveChanges();
            }
        }

        public void Delete(Employee obj)
        {
            using (var db = new MobilizeYouEntities())
            {
                db.Employees.Attach(obj);
                db.Employees.Remove(obj);
                db.SaveChanges();
            }
        }

        public void Update(Employee obj)
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
