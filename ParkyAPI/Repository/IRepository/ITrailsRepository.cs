using ParkyAPI.Models;

namespace ParkyAPI.Repository.IRepository
{
    public interface ITrailsRepository
    {
        ICollection<Trail> GetTrails();
        Trail GetTrails(int Trailid);
        bool TrailsExists(string name);
        bool TrailsExists(int Trailid);
        bool CreateTrails(Trail Trail);
        bool UpdateTrails(Trail Trail);
        bool DeleteTrails(Trail Trail);
        bool save();
    }
}
