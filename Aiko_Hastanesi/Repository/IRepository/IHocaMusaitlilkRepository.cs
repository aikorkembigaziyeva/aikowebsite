using Aiko_Hastanesi.Models;
using System.Numerics;

namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface IHocaMusaitlilkRepository : IRepository<HocaMusaitlik>
    {
        void Update(HocaMusaitlik obj);
    }
}
