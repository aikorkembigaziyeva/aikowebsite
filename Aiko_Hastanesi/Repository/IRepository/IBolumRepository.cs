using Aiko_Hastanesi.Models;
using System.Numerics;

namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface IBolumRepository : IRepository<Bolum>
    {
        void Update(Bolum obj);
    }
}
