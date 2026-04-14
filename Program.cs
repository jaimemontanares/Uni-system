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
        var manager = new UniversityManager();
        SeedData.Initialize(manager);
        
        var startMenu = new StartMenu(manager);
        startMenu.Run();
        }
    }
}
