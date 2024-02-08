using FinancasPessoaisWeb.Data;
using FinancasPessoaisWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoaisWeb.Services
{
    public class DespesaService
    {
        private readonly ApplicationDbContext _context;
        public DespesaService(ApplicationDbContext context)
        {
            _context = context;
        }
                
        public async Task<bool> CadastrarDespesa(DespesaModel despesa)
        {
            despesa.IdAccount = 1;
            _context.Despesa.Add(despesa);
            await _context.SaveChangesAsync();
            return despesa.Id > 0;
        }
        public async Task<DespesaModel> GetDespesaById(int Id)
        {            
            var despesa = await _context.Despesa.FirstOrDefaultAsync(a => a.Id == Id);            
            return despesa;
        }
        public async Task<bool> AtualizarDespesa(DespesaModel despesa)
        {
            _context.Despesa.Update(despesa);
            await _context.SaveChangesAsync();
            return despesa.Id > 0;
        }

        public async Task<bool> Apagar(int id)
        {
            DespesaModel despesa = await GetDespesaById(id);
            _context.Despesa.Remove(despesa);
            await _context.SaveChangesAsync();
            return despesa.Id > 0;
        }

        public async Task<List<DespesaModel>> BuscarPorIdAccount(string IdAccount)
        {
            List<DespesaModel> despesas = await _context.Despesa.Where(d => d.IdAccount == int.Parse(IdAccount))
        .Select(d => new DespesaModel
        {           
            Id = d.Id,
            Descricao = d.Descricao,
            Valor = d.Valor,
            Vencimento = d.Vencimento,
            Situacao = d.Situacao,
            IdAccount = d.IdAccount
        })
        .ToListAsync();
            return despesas;
        }
    }
}
