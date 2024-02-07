using FinancasPessoaisWeb.Data;
using FinancasPessoaisWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoaisWeb.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _context;
        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> VerificarCredenciais(string cpf, string senha)
        {
            return await _context.Account.AnyAsync(a => a.CPF == cpf && a.Senha == senha);
        }
        public async Task<AccountModel> BuscarPorCPF(string cpf)
        {
            var account = await _context.Account.FirstOrDefaultAsync(a => a.CPF == cpf);
            return account;
        }
        public async Task<AccountModel> BuscarPorId(string id)
        {
            var account = await _context.Account.FirstOrDefaultAsync(a => a.Id == int.Parse(id));
            return account;
        }
        public async Task<bool> CadastrarConta(AccountModel accountModel)
        {
            _context.Account.Add(accountModel);
            await _context.SaveChangesAsync();
            return accountModel.Id > 0;
        }

        public async Task<bool> AtualizarConta(AccountModel accountModel)
        {            
            _context.Account.Update(accountModel); 
            await _context.SaveChangesAsync();
            return accountModel.Id > 0; 
        }



    }
}
