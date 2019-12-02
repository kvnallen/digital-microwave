using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Benner.DigitalMicrowave.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Benner.DigitalMicrowave.Models
{
    public class MicrowaveViewModel
    {
        public MicrowaveViewModel()
        {
            
        }

        public MicrowaveViewModel(IEnumerable<MicrowaveProgram> programs)
        {
            Programs = new SelectList(programs, nameof(MicrowaveProgram.Name), nameof(MicrowaveProgram.Name));
        }

        [Required(ErrorMessage = "O texto é obrigatório.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "O tempo é obrigatório.")]
        public int Time { get; set; }

        public string ProgramName { get; set; }

        public SelectList Programs { get; set; }
        public int Power { get; set; }
        public int? CurrentTime { get; set; }
    }
}