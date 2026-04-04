namespace UniversitySystem.UI
{
    /// <summary>
    /// Felles hjelpemetoder for robust lesing av input i konsollen.
    /// Holder validering ute av menyklassene.
    /// </summary>
    public static class InputHelper
    {
        public static string ReadRequiredString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }

                Console.WriteLine("Input kan ikke være tom. Prøv igjen.");
            }
        }

        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.WriteLine("Ugyldig tall. Prøv igjen.");
            }
        }

        public static int ReadMenuChoice(int min, int max)
        {
            while (true)
            {
                Console.Write("Velg alternativ: ");
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) && value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine("Ugyldig valg. Prøv igjen.");
            }
        }

        public static void Pause()
        {
            Console.WriteLine("\nTrykk en tast for å fortsette...");
            Console.ReadKey();
        }
    }
}
