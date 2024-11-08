using CP3.Domain.Entities;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Domain.Interfaces
{
    public interface IBarcoApplicationService
    {
        IEnumerable<BarcoEntity> GetAllBoats();
        BarcoEntity GetBoatById(int id);
        BarcoEntity AddBoat(IBarcoDto dto);
        BarcoEntity UpdateBoat(int id, IBarcoDto dto);
        BarcoEntity DeleteBoat(int id);
    }
}
