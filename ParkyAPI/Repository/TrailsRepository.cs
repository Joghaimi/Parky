using ParkyAPI.Models;
using ParkyAPI.Repository.IRepository;

namespace ParkyAPI.Repository
{
    public class TrailsRepository:ITrailsRepository
    {
        private ApplicationDbContext _db;
        public TrailsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTrails(Trail Trail)
        {
            _db.Trail.Add(Trail);
            return save();
        }

        public bool DeleteTrails(Trail Trail)
        {
            _db.Trail.Remove(Trail);
            return save();
        }

        public ICollection<Trail> GetTrails()
        {
            return _db.Trail.OrderBy(a => a.Name).ToList();
        }

        public Trail GetTrails(int Trailid)
        {
            return _db.Trail.FirstOrDefault(a => a.id == Trailid);
        }

        public bool save()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }

        public bool TrailsExists(string name)
        {
            return _db.Trail.Any(a => a.Name.Trim().ToLower() == name.ToLower().Trim());
        }

        public bool TrailsExists(int Trailid)
        {
            return _db.Trail.Any(a => a.id == Trailid);
        }

        public bool UpdateTrails(Trail Trail)
        {
            _db.Trail.Update(Trail);
            return save();
        }
    }
}
