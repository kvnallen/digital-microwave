using Benner.DigitalMicrowave.Core.Commands;
using Benner.DigitalMicrowave.Core.Models;
using System;
using System.Collections.Generic;

namespace Benner.DigitalMicrowave.Core.Services
{
    public class ProgramService
    {
        private readonly IProgramRepository _repository;

        public ProgramService(IProgramRepository repository)
        {
            _repository = repository;
        }

        public void Add(CreateProgramCommand command)
        {
            var program = new MicrowaveProgram(
                command.Name,
                command.Instructions,
                command.Time,
                command.Power,
                command.HeatingCharacter);

            var programFromDb = _repository.GetByName(command.Name);

            if (programFromDb != null)
            {
                throw new InvalidOperationException(Errors.PROGRAM_WITH_SAME_NAME);
            }

            _repository.Add(program);
        }

        public MicrowaveProgram GetByName(string name) 
            => _repository.GetByName(name);


        public IEnumerable<MicrowaveProgram> GetAll() 
            => _repository.GetAll();
    }
}
