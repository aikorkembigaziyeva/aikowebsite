using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using System.Numerics;

namespace Aiko_Hastanesi.Repository
{
    public class RandevuRepository : Repository<Randevu>, IRandevuRepository
    {
        private ApplicationDbContext _db;
        public RandevuRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Randevu obj)
        {
            _db.Randevus.Update(obj);
        }
    }
}
