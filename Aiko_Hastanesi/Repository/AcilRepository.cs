using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using System.Numerics;

namespace Aiko_Hastanesi.Repository
{
    public class AcilRepository : Repository<Acil>, IAcilRepository
    {
        private ApplicationDbContext _db;
        public AcilRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Acil obj)
        {
            _db.Acils.Update(obj);
        }
    }
}
