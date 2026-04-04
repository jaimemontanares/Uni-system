using UniversitySystem.Data;
using UniversitySystem.UI;

namespace UniversitySystem
{
    /// <summary>
    /// Startpunkt for konsollapplikasjonen.
    /// Oppretter systemets tjenester, laster inn eksempeldata og starter menyen.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Hovedmetoden som starter programmet.
        /// </summary>
        /// <param name="args">Eventuelle argumenter sendt ved oppstart.</param>
        static void Main(string[] args)
        {
            // Laster inn eksempeldata slik at systemet kan testes med en gang
            var manager = SeedData.Initialize();

            // Starter menyen
            var menu = new Menu(manager);
            menu.Run();
        }
    }
}
