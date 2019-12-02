using System.Collections.Generic;
using System.Linq;
using Benner.DigitalMicrowave.Core.Models;
using Benner.DigitalMicrowave.Models;

namespace Benner.DigitalMicrowave.Extensions
{
    public static class ProgramViewModelExtensions
    {
        public static ProgramViewModel ToViewModel(this MicrowaveProgram program)
        {
            return new ProgramViewModel
            {
                Time = program.Time,
                Power = program.Power,
                HeatingCharacter = program.HeatingCharacter,
                Name = program.Name,
                Instructions = program.Instructions
            };
        }

        public static IEnumerable<ProgramViewModel> ToViewModel(this IEnumerable<MicrowaveProgram> programs)
        {
            return programs.Select(ToViewModel).ToList();
        }
    }
}