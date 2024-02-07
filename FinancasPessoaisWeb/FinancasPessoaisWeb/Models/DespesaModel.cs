using System.ComponentModel.DataAnnotations;

namespace FinancasPessoaisWeb.Models

{
    public class DespesaModel
    {
        public int Id { get; set; }

        public int IdAccount { get; set; }

        [Required(ErrorMessage = "A descrição da despesa é obrigatória.")]
        [StringLength(255, ErrorMessage = "A descrição da despesa deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O valor da despesa é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor da despesa deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        [DataType(DataType.DateTime, ErrorMessage = "Informe uma data e hora válida.")]
        [Display(Name = "Data de Vencimento")]
        public DateTime Vencimento { get; set; }

        [StringLength(45, ErrorMessage = "A situação da despesa deve ter no máximo {1} caracteres.")]
        public string Situacao { get; set; } = "A pagar";
    }
}