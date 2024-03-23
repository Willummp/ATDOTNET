using System.ComponentModel.DataAnnotations;

namespace CarGallery.Models
{
    public class Fabricante
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public String Nome { get; set; }

        [Required(ErrorMessage = "Campo descrição é obrigatório")]
        public String Descricao { get; set; }

        public List<Carro> Carros { get; set; } = new List<Carro>(); 
    }
}
