using UniversitySystem.Data;
using UniversitySystem.Services;
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
            // Oppretter systemets sentrale tjenester
            var manager = new UniversityManager();

            // Laster inn eksempeldata slik at systemet kan testes med en gang
            SeedData.Initialize(manager);

            // Starter menyen
            var menu = new Menu(manager);
            menu.Run();
        }
    }
}
