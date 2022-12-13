using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaDeEncomendas.Models
{
    public class Encomendas
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição da encomenda é obrigatória")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Escolha a forma de pagamento")]
        [Display(Name = "Pagamento*")]
        public string FormaPagamento { get; set; }

        [Required(ErrorMessage = "Informe o status inicial do projeto")]
        [Display(Name = "Status*")]
        public string Status { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o valor do pedido")]
        //[Range(0, 9999.99)]
        [DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18,2)")]
        //[DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        public Clientes Clientes { get; set; }

        [Required(ErrorMessage = "É obrigatório associar a encomenda a um cliente")]
        [Display(Name = "Cliente*")]
        public int ClientesId { get; set; }
    }
}
