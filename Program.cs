using UniversitySystem.Data;
using UniversitySystem.Services;
using UniversitySystem.UI;

namespace UniversitySystem
{
    /// <summary>
    /// Startpunkt for konsollapplikasjonen.
    /// Oppretter systemets sentrale tjenester, fyller inn demo-data
    /// og overfører kontrollen til brukergrensesnittet.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Setter opp applikasjonen i riktig rekkefølge:
        /// 1. Oppretter service-laget
        /// 2. Laster inn demo-data
        /// 3. Starter hovedmenyen
        /// </summary>
        /// <param name="args">Eventuelle argumenter sendt ved oppstart.</param>
        static void Main(string[] args)
        {
            var manager = new UniversityManager();
            SeedData.Initialize(manager);

            var startMenu = new StartMenu(manager);
            startMenu.Run();
        }
    }
}
