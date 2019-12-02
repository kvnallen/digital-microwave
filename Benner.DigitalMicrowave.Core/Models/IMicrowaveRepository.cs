namespace Benner.DigitalMicrowave.Core.Models
{
    public interface IMicrowaveRepository
    {
         void Store(Microwave microwave);
         void ClearCurrent();
         Microwave GetCurrent();
    }
}