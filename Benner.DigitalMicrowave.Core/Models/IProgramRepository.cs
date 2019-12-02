using System.Collections.Generic;

namespace Benner.DigitalMicrowave.Core.Models
{
    public interface IProgramRepository
    {
        MicrowaveProgram GetByName(string programName);
        void Add(MicrowaveProgram program);
        IEnumerable<MicrowaveProgram> GetAll();
    }
}
