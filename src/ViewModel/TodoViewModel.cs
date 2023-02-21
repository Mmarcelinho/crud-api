using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.ViewModel
{
    public class TodoViewModel
    {
        [Required(ErrorMessage = "Campo Obrigátorio")]
        [MinLength(3, ErrorMessage = "Minimo de 3 caracteres")]
        public string Title { get; set; }
    }
}