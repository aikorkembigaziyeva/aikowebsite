namespace Aiko_Hastanesi.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IHocaRepository Hoca {  get; }
        IAsistanRepository Asistan { get; }
        IBolumRepository Bolum { get; }
        IAcilRepository Acil { get; }
        INobetRepository Nobet { get; }
        IHocaMusaitlilkRepository HocaMusaitlik { get; }
        IRandevuRepository Randevu { get; }
        void Save();
    }
}
