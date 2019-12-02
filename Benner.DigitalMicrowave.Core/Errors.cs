namespace Benner.DigitalMicrowave.Core
{
    public static class Errors
    {
        public const string PROGRAM_WITH_DEFAULT_CHARACTER = "O programa não pode ter o caractere padrão (.)";
        public const string INCOMPATIBLE_FOOD = "Alimento incompatível com o programa selecionado.";
        public const string MICROWAVE_OFF = "O micro-ondas não está ligado.";
        public const string POWER_OUT_OF_RANGE = "A potência deve estar entre 1 e 10.";
        public const string TIME_OUT_OF_RANGE = "O tempo deve estar entre 1 segundo e 2 minutos.";
        public const string PROGRAM_WITH_SAME_NAME = "O nome do programa já existe no sistema.";
    }
}