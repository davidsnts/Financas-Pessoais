using System.ComponentModel.DataAnnotations;

namespace FinancasPessoaisWeb.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
