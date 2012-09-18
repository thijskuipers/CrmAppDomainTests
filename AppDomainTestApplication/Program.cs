using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using AppDomainTest.Crm4.Interfaces;

namespace AppDomainTest.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Start");

            Console.WriteLine("Main AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);

            Evidence adEvidence = AppDomain.CurrentDomain.Evidence;
            AppDomainSetup adSetup = new AppDomainSetup();
            
            Console.WriteLine("ApplicationBase: {0}", adSetup.ApplicationBase);
            Console.WriteLine("PrivateBinPath: {0}", adSetup.PrivateBinPath);
            Console.WriteLine("PrivateBinPathProbe: {0}", adSetup.PrivateBinPathProbe);

            Console.WriteLine();
            PrintAssemblies(AppDomain.CurrentDomain);

            adSetup.ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

            AppDomain crm4AppDomain = AppDomain.CreateDomain("Crm4Service", null, adSetup);

            crm4AppDomain.Load("AppDomainTest.Crm4.Service");

            IService crm4Service = (IService)crm4AppDomain.CreateInstanceAndUnwrap("AppDomainTest.Crm4.Service", "AppDomainTest.Crm4.Service.Service");
            string setup = crm4Service.Setup();
            Console.WriteLine(setup);

            PrintAssemblies(crm4AppDomain);

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
