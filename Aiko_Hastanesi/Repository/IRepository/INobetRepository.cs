using Aiko_Hastanesi.Models;
using System.Numerics;

namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface INobetRepository : IRepository<Nobet>
    {
        void Update(Nobet obj);
    }
}
