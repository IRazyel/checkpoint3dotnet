using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly ApplicationContext _context;

        public BarcoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public BarcoEntity? GetById(int id)
        {
            return _context.Barcos.Find(id);
        }

        public IEnumerable<BarcoEntity>? GetAll()
        {
            return _context.Barcos.ToList();
        }

        public BarcoEntity? Add(BarcoEntity barco)
        {
            _context.Barcos.Add(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Update(BarcoEntity barco)
        {
            _context.Barcos.Update(barco);
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Delete(int id)
        {
            var barco = _context.Barcos.Find(id);
            if (barco != null)
            {
                _context.Barcos.Remove(barco);
                _context.SaveChanges();
            }
            return barco;
        }
    }
}
