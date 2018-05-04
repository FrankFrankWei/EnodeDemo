﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Google.NameServerService
{
    class Program
    {
        static void Main()
        {
            if (!Environment.UserInteractive)
            {
                ServiceBase.Run(new Service());
            }
            else
            {
                Bootstrap.Initialize();
                Bootstrap.Start();

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Initialize success...");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine("Press enter to exit...");
                var line = Console.ReadLine();
                while (line != "exit")
                {
                    switch (line)
                    {
                        case "cls":
                            Console.Clear();
                            break;
                        default:
                            return;
                    }
                    line = Console.ReadLine();
                }
            }
        }
    }
}