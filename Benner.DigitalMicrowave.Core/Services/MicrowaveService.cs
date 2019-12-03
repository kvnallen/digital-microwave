using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Benner.DigitalMicrowave.Core.Commands;
using Benner.DigitalMicrowave.Core.Events;
using Benner.DigitalMicrowave.Core.Models;

namespace Benner.DigitalMicrowave.Core.Services
{
    public class MicrowaveService
    {
        private readonly IMicrowaveRepository _repository;
        private readonly IProgramRepository _programRepository;
        private readonly IEnumerable<IMicrowaveNotifier> _notifiers;

        public MicrowaveService(
            IMicrowaveRepository repository,
            IProgramRepository programRepository,
            IEnumerable<IMicrowaveNotifier> notifiers)
        {
            _repository = repository;
            _programRepository = programRepository;
            _notifiers = notifiers;
        }

        public Task<string> Warm(WarmCommand command)
        {
            var (microwave, notifiers) = CreateMicrowave(command);
            return microwave.Warm(notifiers);
        }

        public Task<string> FastStart(WarmCommand command)
        {
            command.Time = 30;
            command.Power = 8;
            var (microwave, notifiers) = CreateMicrowave(command);
            return microwave.Warm(notifiers);
        }

        private (Microwave microwave, List<IMicrowaveNotifier> notifiers) CreateMicrowave(WarmCommand command)
        {
            var options = GetOptions(command);
            var notifiers = _notifiers.Where(x => x.IsSatisfied(options)).ToList();
            var microwave = new Microwave(options);
            var program = options.Program;

            if (program != null
               && !options.IsFile()
               && !program.IsCompatibleForFood(options.Text))
            {
                throw new InvalidOperationException(Errors.INCOMPATIBLE_FOOD);
            }

            _repository.Store(microwave);
            return (microwave, notifiers);
        }

        public void Cancel()
        {
            var current = _repository.GetCurrent();

            if (current is null)
                throw new InvalidOperationException(Errors.MICROWAVE_OFF);

            current.Cancel();

            _repository.ClearCurrent();
        }

        private MicrowaveOptions GetOptions(WarmCommand command)
        {
            if (string.IsNullOrEmpty(command.ProgramName))
            {
                return new MicrowaveOptions(command.GetTime(), command.Power, command.Text, ".");
            }

            var program = _programRepository.GetByName(command.ProgramName);

            return new MicrowaveOptions(
                time: program.Time,
                power: program.Power,
                text: command.Text,
                heatingCharacter: program.HeatingCharacter,
                program: program);
        }

        public void Pause()
        {
            var microwave = _repository.GetCurrent();
            microwave.Pause();
            _repository.ClearCurrent();
        }
    }
}