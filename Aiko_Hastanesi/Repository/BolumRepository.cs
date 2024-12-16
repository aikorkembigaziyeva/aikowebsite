using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using System.Numerics;

namespace Aiko_Hastanesi.Repository
{
    public class BolumRepository : Repository<Bolum>, IBolumRepository
    {
        private ApplicationDbContext _db;
        public BolumRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Bolum obj)
        {
            _db.Bolums.Update(obj);
        }
    }
}
