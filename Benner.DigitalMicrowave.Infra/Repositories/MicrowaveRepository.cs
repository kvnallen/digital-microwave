using Benner.DigitalMicrowave.Core.Models;

namespace Benner.DigitalMicrowave.Infra.Repositories
{
    public class MicrowaveRepository : IMicrowaveRepository
    {
        private static Microwave _current;

        public void Store(Microwave microwave){
            _current = microwave;
        }

        public void ClearCurrent()
        {
            _current = null;
        }

        public Microwave GetCurrent() => _current;
    }
}