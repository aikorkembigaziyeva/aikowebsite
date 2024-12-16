using Aiko_Hastanesi.Models;
using Aiko_Hastanesi.Repository.IRepository;

namespace Aiko_Hastanesi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IHocaRepository Hoca { get; private set; }  
        public IAsistanRepository Asistan { get; private set; }
        public IBolumRepository Bolum { get; private set; }
        public IAcilRepository Acil { get; private set; }
        public INobetRepository Nobet { get; private set; }
        public IHocaMusaitlilkRepository HocaMusaitlik { get; private set; }
        public IRandevuRepository Randevu { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Hoca = new HocaRepository(_db);        
            Asistan = new AsistanRepository(_db);
            Bolum = new BolumRepository(_db);
            Acil = new AcilRepository(_db);
            Nobet = new NobetRepository(_db);
            HocaMusaitlik = new HocaMusaitlikRepository(_db);
            Randevu = new RandevuRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
