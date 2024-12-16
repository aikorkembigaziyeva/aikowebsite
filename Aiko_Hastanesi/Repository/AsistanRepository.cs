using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using System.Numerics;

namespace Aiko_Hastanesi.Repository
{
    public class AsistanRepository : Repository<Asistan>, IAsistanRepository
    {
        private ApplicationDbContext _db;
        public AsistanRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Asistan obj)
        {
            _db.Asistans.Update(obj);
        }
    }
}
