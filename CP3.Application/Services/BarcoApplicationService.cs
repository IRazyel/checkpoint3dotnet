using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Application.Services
{
    public class BarcoApplicationService : IBarcoApplicationService
    {
        private readonly IBarcoRepository _repository;

        public BarcoApplicationService(IBarcoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<BarcoEntity> GetAllBoats()
        {
            return _repository.GetAll();
        }

        public BarcoEntity GetBoatById(int id)
        {
            return _repository.GetById(id);
        }

        public BarcoEntity AddBoat(IBarcoDto dto)
        {
            dto.Validate();

            var barcoEntity = new BarcoEntity
            {
                Nome = dto.Nome,
                Modelo = dto.Modelo,
                Ano = dto.Ano,
                Tamanho = dto.Tamanho
            };

            return _repository.Add(barcoEntity);
        }

        public BarcoEntity UpdateBoat(int id, IBarcoDto dto)
        {
            dto.Validate();

            var barcoEntity = _repository.GetById(id);
            if (barcoEntity == null)
                throw new Exception("Barco não encontrado");

            barcoEntity.Nome = dto.Nome;
            barcoEntity.Modelo = dto.Modelo;
            barcoEntity.Ano = dto.Ano;
            barcoEntity.Tamanho = dto.Tamanho;

            return _repository.Update(barcoEntity);
        }

        public BarcoEntity DeleteBoat(int id)
        {
            var barco = _repository.Delete(id);
            if (barco == null)
                throw new Exception("Barco não encontrado");

            return barco;
        }
    }
}
