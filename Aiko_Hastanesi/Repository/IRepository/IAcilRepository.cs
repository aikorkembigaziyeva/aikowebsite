using Aiko_Hastanesi.Models;
using System.Numerics;

namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface IAcilRepository : IRepository<Acil>
    {
        void Update(Acil obj);
    }
}
