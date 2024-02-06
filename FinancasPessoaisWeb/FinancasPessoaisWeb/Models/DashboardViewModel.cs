namespace FinancasPessoaisWeb.Models
{
    public class DashboardViewModel
    {
        public AccountModel Account { get; set; }
        public decimal SaldoConta { get; set; }
        public List<DespesaModel> DespesasFuturas { get; set; }
    }
}
