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
