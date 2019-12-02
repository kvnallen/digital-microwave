using System.Collections.Generic;
using System.Linq;
using Benner.DigitalMicrowave.Core.Models;

namespace Benner.DigitalMicrowave.Infra.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private static readonly HashSet<MicrowaveProgram> Programs = new HashSet<MicrowaveProgram>
        {
            new MicrowaveProgram("Frango", "Insira o frango", 120, 10, "🐓"),
            new MicrowaveProgram("Pipoca", "Insira a pipoca com o pacote virado para cima", 30, 3, "🍿"),
            new MicrowaveProgram("Carne", "Insira o churrasco", 100, 5, "🍖"),
            new MicrowaveProgram("Ovo", "Insira um recipiente de vidro com água e o ovo", 90, 7, "🥚"),
            new MicrowaveProgram("Macarrão", "Insira o macarrão", 45, 9, "🍜")
        };

        public void Add(MicrowaveProgram program) 
            => Programs.Add(program);

        public IEnumerable<MicrowaveProgram> GetAll() 
            => Programs;

        public MicrowaveProgram GetByName(string programName)
        {
            return Programs.SingleOrDefault(x => x.Name.ToLower() == programName.ToLower().Trim());
        }
    }
}