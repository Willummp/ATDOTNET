using System.ComponentModel.DataAnnotations;

namespace CarGallery.Models
{
    public class Usuario
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Campo email é obrigatório")]
        [EmailAddress(ErrorMessage = "Campo email não está em um formato correto")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Campo senha é obrigatório")]
        public string Senha { get; set; }
    }
}
