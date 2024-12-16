using Aiko_Hastanesi.Models;
using System.Numerics;

namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface IRandevuRepository : IRepository<Randevu>
    {
        void Update(Randevu obj);
    }
}
