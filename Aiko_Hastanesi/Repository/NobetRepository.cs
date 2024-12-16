using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using System.Numerics;

namespace Aiko_Hastanesi.Repository
{
    public class NobetRepository : Repository<Nobet>, INobetRepository
    {
        private ApplicationDbContext _db;
        public NobetRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Nobet obj)
        {
            _db.Nobets.Update(obj);
        }
    }
}
