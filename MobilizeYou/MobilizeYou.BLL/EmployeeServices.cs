using System.Collections.Generic;

namespace MobilizeYou.BLL
{
    using DTO;
    using DAL;
    public class EmployeeServices : IServices<Employee>
    {
        readonly EmployeeDao _employeeDao = new EmployeeDao();
        public List<Employee> GetAll()
        {
            return _employeeDao.GetAll();
        }

        public Employee GetById(int id)
        {
            return _employeeDao.GetById(id);
        }

        public void Add(Employee obj)
        {
            _employeeDao.Add(obj);
        }

        public void Delete(Employee obj)
        {
            _employeeDao.Delete(obj);
        }
    }
}
