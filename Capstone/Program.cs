using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    /// <summary>
    /// Main entry point for the program.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            MainMenuCLI mainMenu = new MainMenuCLI();
            mainMenu.DisplayCLI();
        }
    }
}
