using CP3.Domain.Entities;

namespace CP3.Domain.Interfaces
{
    public interface IBarcoRepository
    {
        BarcoEntity? GetById(int id);
        IEnumerable<BarcoEntity>? GetAll();
        BarcoEntity? Add(BarcoEntity barco);
        BarcoEntity? Update(BarcoEntity barco);
        BarcoEntity? Delete(int id);
    }
}
