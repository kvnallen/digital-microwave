using System;
using System.Collections.Generic;
using System.Linq;

namespace Benner.DigitalMicrowave.Core.Models
{
    public class MicrowaveProgram
    {
        public const string DEFAULT_HEATING_CHARACTER = ".";

        public MicrowaveProgram(string name, string instructions, MicrowaveTime time, MicrowavePower power, string heatingCharacter)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));
            Time = time ;
            Power = power;

            if (DEFAULT_HEATING_CHARACTER == heatingCharacter)
            {
                throw new ArgumentException(Errors.PROGRAM_WITH_DEFAULT_CHARACTER);
            }

            HeatingCharacter = heatingCharacter;
        }

        public string Name { get; }
        public string Instructions { get; }
        public MicrowaveTime Time { get; }
        public MicrowavePower Power { get; }
        public string HeatingCharacter { get; }

        public override bool Equals(object obj)
        {
            return obj is MicrowaveProgram program &&
                   Name == program.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public static bool operator ==(MicrowaveProgram left, MicrowaveProgram right)
        {
            return EqualityComparer<MicrowaveProgram>.Default.Equals(left, right);
        }

        public static bool operator !=(MicrowaveProgram left, MicrowaveProgram right)
        {
            return !(left == right);
        }

        public bool IsCompatibleForFood(string food)
        {
            if (string.IsNullOrEmpty(food))
                return false;

            return food
                .Split(" ")
                .Any(word => Name.Contains(word, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
