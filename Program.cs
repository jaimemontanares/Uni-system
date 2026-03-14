C#
using UniversitySystem.Data;
using UniversitySystem.UI;

namespace UniversitySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = SeedData.Initialize();
            var menu = new Menu(manager);
            menu.Run();
        }
    }
}