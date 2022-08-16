using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class LacheRepository : ILancheRepository
    {
        private AppDbContext _context;

        public LacheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }
        IEnumerable<Lanche> ILancheRepository.Lanches => _context.Lanches.Include(c => c.Categoria);

        IEnumerable<Lanche> ILancheRepository.LanchesPreferidos =>_context.Lanches.
                            Where(l => l.IsLanchePreferido).Include(c => c.Categoria);

        Lanche ILancheRepository.GetLancheByld(int lancheId)
        {
           return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
