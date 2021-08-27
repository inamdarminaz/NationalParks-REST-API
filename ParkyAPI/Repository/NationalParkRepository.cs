using ParkyAPI.Data;
using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _context;
        public NationalParkRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateNationalParkExists(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationalParkExists(NationalPark nationalPark)
        {
            _context.NationalParks.Remove(nationalPark);
            return Save();    
        }

        public NationalPark GetNationalPark(int id)
        {
            return _context.NationalParks.FirstOrDefault(n=>n.Id==id);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return _context.NationalParks.OrderBy(n=>n.Name).ToList();
        }

        public bool NationalParkExists(int id)
        {
            bool isExists = _context.NationalParks.Any(n=>n.Id==id);
            return isExists;
        }

        public bool NationalParkExists(string name)
        {
            bool isExists = _context.NationalParks.Any(n=>n.Name.ToLower().Trim()==name.ToLower().Trim());
            return isExists;
        }

        public bool Save()
        {
            int rowsAffected = _context.SaveChanges();
            return rowsAffected > 0 ? true : false;
        }

        public bool UpdateNationalParkExists(NationalPark nationalPark)
        {
            _context.NationalParks.Update(nationalPark);
            return Save();
        }
    }
}
