using Aiko_Hastanesi.Models;
using System.Numerics;

namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface IAsistanRepository : IRepository<Asistan>
    {
        void Update(Asistan obj);
    }
}
