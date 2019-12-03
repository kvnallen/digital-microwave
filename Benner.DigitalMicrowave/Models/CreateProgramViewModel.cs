using System;
using System.ComponentModel.DataAnnotations;

namespace Benner.DigitalMicrowave.Models
{
    public class CreateProgramViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "As instruções são obrigatórias.")]
        public string Instructions { get; set; }


        [Range(1, 120, ErrorMessage = "O tempo deve estar entre 1 ~ 120 segundos.")]
        public int Time { get; set; }

        [Range(1, 10, ErrorMessage = "A potência deve estar entre 1 e 10.")]
        public int Power { get; set; }

        [StringLength(1, ErrorMessage = "O caractér deve ter no máximo 1 dígito.")]
        [Required(ErrorMessage = "O caractér de aquecimento é obrigatório")]
        public string HeatingCharacter { get; set; }
    }
}
