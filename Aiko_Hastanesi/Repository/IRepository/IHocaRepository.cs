using Aiko_Hastanesi.Models;
using System.Numerics;

namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface IHocaRepository : IRepository<Hoca>
    {
        void Update(Hoca obj);
    }
}
