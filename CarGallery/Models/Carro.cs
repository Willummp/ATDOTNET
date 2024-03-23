using Microsoft.Build.Framework;

namespace CarGallery.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string DescricaoCurta { get; set; }
        public string DescricaoLonga { get; set; }
        public string Imagem { get; set; }
        public int FabricanteId { get; set; }
        public Fabricante Fabricante { get; set; }

    }
}
