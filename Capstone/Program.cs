using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {
			MainMenuCLI mainMenu = new MainMenuCLI();
			mainMenu.DisplayCLI();
        }
    }
}
