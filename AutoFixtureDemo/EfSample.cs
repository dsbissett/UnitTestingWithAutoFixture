using System.Collections.Generic;
using System.Linq;

namespace AutoFixtureDemo
{
    public class EfSample
    {
        private NORTHWNDEntities _context;

        public EfSample(NORTHWNDEntities context)
        {
            _context = context;
        }

        public IEnumerable<Employees> GetAllEmployees()
        {
            using (_context)
            {
                return _context.Employees.ToList();
            }
        }

        public Employees GetEmployeeById(int id)
        {
            using (_context)
            {
                return _context.Employees.Find(id);
            }
        }
    }
}