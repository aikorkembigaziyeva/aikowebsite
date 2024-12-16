using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using System.Numerics;

namespace Aiko_Hastanesi.Repository
{
    public class HocaRepository : Repository<Hoca>, IHocaRepository
    {
        private ApplicationDbContext _db;
        public HocaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Hoca obj)
        {
            _db.Hocas.Update(obj);
        }
    }
}
