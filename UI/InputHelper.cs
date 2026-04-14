using System;

namespace UniversitySystem.UI
{
    /// <summary>
    /// Hjelpeklasse for trygg innlesing av brukerinput i konsollapplikasjonen.
    /// Samler validering og standardisert dialog med brukeren.
    /// </summary>
    public static class InputHelper
    {
        /// <summary>
        /// Leser en påkrevd tekststreng fra brukeren.
        /// Tillater ikke tom eller ugyldig input.
        /// </summary>
        /// <param name="prompt">Teksten som vises før innlesing.</param>
        /// <returns>En trimmet, ikke-tom tekststreng.</returns>
        public static string ReadRequiredString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                Console.WriteLine("Feltet kan ikke være tomt. Prøv igjen.");
            }
        }

        /// <summary>
        /// Leser en valgfri tekststreng fra brukeren.
        /// Returnerer tom streng hvis brukeren ikke skriver noe.
        /// </summary>
        /// <param name="prompt">Teksten som vises før innlesing.</param>
        /// <returns>Innles teksten, trimmet. Kan være tom.</returns>
        public static string ReadOptionalString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine()?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Leser et heltall fra brukeren.
        /// </summary>
        /// <param name="prompt">Teksten som vises før innlesing.</param>
        /// <returns>Et gyldig heltall.</returns>
        public static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int value))
                {
                    return value;
                }

                Console.WriteLine("Du må skrive inn et gyldig heltall. Prøv igjen.");
            }
        }

        /// <summary>
        /// Leser et heltall som må være større enn eller lik en gitt minimumsverdi.
        /// </summary>
        /// <param name="prompt">Teksten som vises før innlesing.</param>
        /// <param name="minValue">Laveste tillatte verdi.</param>
        /// <returns>Et gyldig heltall innenfor grensen.</returns>
        public static int ReadInt(string prompt, int minValue)
        {
            while (true)
            {
                int value = ReadInt(prompt);

                if (value >= minValue)
                {
                    return value;
                }

                Console.WriteLine($"Tallet må være større enn eller lik {minValue}. Prøv igjen.");
            }
        }

        /// <summary>
        /// Leser et heltall innenfor et gitt intervall.
        /// Brukes typisk for menyvalg.
        /// </summary>
        /// <param name="minValue">Laveste tillatte verdi.</param>
        /// <param name="maxValue">Høyeste tillatte verdi.</param>
        /// <returns>Et gyldig menyvalg.</returns>
        public static int ReadMenuChoice(int minValue, int maxValue)
        {
            while (true)
            {
                Console.Write("Velg et alternativ: ");
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int choice) &&
                    choice >= minValue &&
                    choice <= maxValue)
                {
                    return choice;
                }

                Console.WriteLine($"Ugyldig valg. Velg et tall mellom {minValue} og {maxValue}.");
            }
        }

        /// <summary>
        /// Leser et ja/nei-svar fra brukeren.
        /// </summary>
        /// <param name="prompt">Spørsmål som vises til brukeren.</param>
        /// <returns>True ved ja, false ved nei.</returns>
        public static bool ReadYesNo(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (j/n): ");
                string? input = Console.ReadLine()?.Trim().ToLowerInvariant();

                if (input == "j" || input == "ja")
                {
                    return true;
                }

                if (input == "n" || input == "nei")
                {
                    return false;
                }

                Console.WriteLine("Ugyldig svar. Skriv 'j' for ja eller 'n' for nei.");
            }
        }

        /// <summary>
        /// Stopper flyten midlertidig slik at brukeren kan lese meldinger før programmet fortsetter.
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Trykk Enter for å fortsette...");
            Console.ReadLine();
        }
    }
}
