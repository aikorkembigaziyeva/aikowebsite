using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Aiko_Hastanesi.Repository
{
    public class HocaMusaitlikRepository : Repository<HocaMusaitlik>, IHocaMusaitlilkRepository
    {
        private ApplicationDbContext _db;
        public HocaMusaitlikRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(HocaMusaitlik obj)
        {
            var existingEntity = _db.HocaMusaitliks.Local.FirstOrDefault(e => e.Id == obj.Id);
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).State = EntityState.Detached;
            }

            _db.HocaMusaitliks.Update(obj);
        }
    }
}
