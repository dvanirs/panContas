using Microsoft.EntityFrameworkCore;
using panContas.Domain.Entities;
using panContas.Domain.Interfaces;
using panContas.Infrastructure.Data;

namespace panContas.Infrastructure.Repositories
{
    public class PessoaJuridicaRepository : IPessoaJuridicaRepository
    {
        private readonly AppDbContext _context;

        public PessoaJuridicaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PessoaJuridica?> GetByIdAsync(Guid id)
        {
            return await _context.PessoasJuridicas.Include(p => p.Endereco).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PessoaJuridica>> GetAllAsync()
        {
            return await _context.PessoasJuridicas.Include(p => p.Endereco).ToListAsync();
        }

        public async Task AddAsync(PessoaJuridica entity)
        {
            _context.PessoasJuridicas.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PessoaJuridica entity)
        {
            var existing = await _context.PessoasJuridicas
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(p => p.Id == entity.Id);

            if (existing != null)
            {
                // Atualiza os dados da Pessoa
                _context.Entry(existing).CurrentValues.SetValues(entity);
                // Bloqueia a alteração indevida da Foreign Key
                if (existing.Endereco != null)
                {
                    existing.EnderecoId = existing.Endereco.Id;
                }

                if (existing.Endereco != null && entity.Endereco != null)
                {
                    // Preserva o Id real do endereço do banco!
                    entity.Endereco.Id = existing.Endereco.Id;
                    // Atualiza os dados do Endereço sem inserir nova linha no banco
                    _context.Entry(existing.Endereco).CurrentValues.SetValues(entity.Endereco);
                }
                else if (entity.Endereco != null)
                {
                    existing.Endereco = entity.Endereco;
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.PessoasJuridicas.FindAsync(id);
            if (entity != null)
            {
                _context.PessoasJuridicas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
