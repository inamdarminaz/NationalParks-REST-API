using ParkyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Repository.IRepository
{
    public interface INationalParkRepository
    {
        ICollection<NationalPark> GetNationalParks();
        NationalPark GetNationalPark(int id);
        bool NationalParkExists(int id);
        bool NationalParkExists(string name);
        bool CreateNationalParkExists(NationalPark nationalPark);
        bool UpdateNationalParkExists(NationalPark nationalPark);
        bool DeleteNationalParkExists(NationalPark nationalPark);
        bool Save();
    }
}
