using System.ComponentModel.DataAnnotations;

namespace SistemaDeEncomendas.Models
{
    public class Clientes
    {
        public int Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Nome é obrigatório")]
        public string Nome { get; set; }

        public string? Endereco { get; set; }

        [EmailAddress(ErrorMessage = "Favor insira um e-mail em formato válido")]
        public string? Email { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Celular é obrigatório e em formato válido (00) 00000-0000")]
        [Display(Name = "Celular")]
        public string Tel { get; set; }

        [Display(Name = "Celular 2")]
        public string? TelAdc { get; set; }

        public virtual ICollection<Encomendas> Encomendas { get; set; }
    }
}
