using System;
using System.IO;
using System.Security.Policy;
using AppDomainTest.Crm4.Interfaces;

namespace AppDomainTest.Application
{
    class Program
    {
        [LoaderOptimization(LoaderOptimization.MultiDomain)]
        static void Main(string[] args)
        {
            Console.WriteLine("Application Start");

            Console.WriteLine("Main AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);

            AppDomain.CurrentDomain.SetupInformation.PrivateBinPath = "Crm4";

            AppDomainSetup adSetup = new AppDomainSetup();
            
            Console.WriteLine();
            PrintAssemblies(AppDomain.CurrentDomain);

            adSetup.ApplicationBase = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Crm4");
            adSetup.PrivateBinPath = "Crm4";

            AppDomain crm4AppDomain = AppDomain.CreateDomain("Crm4Service", null, adSetup);

            Console.WriteLine();
            PrintAssemblies(crm4AppDomain);

            Console.WriteLine();
            IService crm4Service = (IService)crm4AppDomain.CreateInstanceAndUnwrap("AppDomainTest.Crm4.Service", "AppDomainTest.Crm4.Service.Service");
            crm4Service.Setup();

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        private static void PrintAssemblies(AppDomain appDomain)
        {
            Console.WriteLine();
            Console.WriteLine("{0} assemblies:", appDomain.FriendlyName);
            foreach (var assembly in appDomain.GetAssemblies())
            {
                Console.WriteLine("  {0}", assembly.FullName);
            }
        }
    }
}
