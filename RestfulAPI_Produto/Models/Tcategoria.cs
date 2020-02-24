using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestfulAPI_Produto.Models
{
    public partial class Tcategoria
    {
        public Tcategoria()
        {
            Tproduto = new HashSet<Tproduto>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "O nome deve ser informado.")]
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve conter no máximo 80 caracteres.")]
        public string Nome { get; set; }

        public ICollection<Tproduto> Tproduto { get; set; }
    }
}
