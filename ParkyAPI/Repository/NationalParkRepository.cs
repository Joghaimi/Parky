using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private ApplicationDbContext _db;
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Add(nationalPark);
            return save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Remove(nationalPark);
            return save();
        }

        public NationalPark GetNationalPark(int nationalParkid)
        {
            return _db.NationalPark.FirstOrDefault(a => a.Id == nationalParkid);
        }


        public ICollection<NationalPark> GetNationalParks()
        {
            return _db.NationalPark.OrderBy(a => a.Name).ToList();
        }

        public bool NationalParkExists(string name)
        {
            return _db.NationalPark.Any(a => a.Name.Trim().ToLower() == name.ToLower().Trim());
        }

        public bool NationalParkExists(int nationalParkid)
        {
            return _db.NationalPark.Any(a=>a.Id == nationalParkid);
        }

        public bool save()
        {
            return _db.SaveChanges() > 0 ? true : false; 
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
            _db.NationalPark.Update(nationalPark);
            return save();
        }
    }
}
