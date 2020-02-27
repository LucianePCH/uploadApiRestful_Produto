using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestfulAPI_Produto.Models
{
    public partial class TProduto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome deve ser informado.")]
        [MinLength(3, ErrorMessage = "O nome deve conter no mínimo 3 caracteres.")]
        [MaxLength(100, ErrorMessage = "O nome deve conter no máximo 100 caracteres.")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "A código da categoria deve ser informado.")]        
        public int IdCategoria { get; set; }

        public TCategoria IdCategoriaNavigation { get; set; }
    }
       
}
