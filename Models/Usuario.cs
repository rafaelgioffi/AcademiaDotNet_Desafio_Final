using System.ComponentModel.DataAnnotations;

namespace SistemaDeEncomendas.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O login é obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; }
    }
}
