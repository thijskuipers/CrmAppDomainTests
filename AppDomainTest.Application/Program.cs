using System;
using System.IO;
using System.Security.Policy;
using AppDomainTest.Common.Interfaces;

namespace AppDomainTest.Application
{
    class Program
    {
        [LoaderOptimization(LoaderOptimization.MultiDomainHost)]
        static void Main(string[] args)
        {
            Console.WriteLine("Application Start");

            Console.WriteLine("Main AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);

            AppDomain crm4AppDomain = SetupCrmServiceAppDomain("Crm4", "AppDomainTest.Crm4.Service", "AppDomainTest.Crm4.Service.Service");
            AppDomain crm5AppDomain = SetupCrmServiceAppDomain("Crm5", "AppDomainTest.Crm5.Service", "AppDomainTest.Crm5.Service.Service");

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Creates a new AppDomain using a ApplicationBase relative to the main application's ApplicationBase,
        /// loading a 
        /// </summary>
        /// <param name="relativeAppBase"></param>
        /// <param name="assemblyName"></param>
        /// <param name="serviceTypeFullName"></param>
        /// <returns>The created AppDomain.</returns>
        private static AppDomain SetupCrmServiceAppDomain(string relativeAppBase, string assemblyName, string serviceTypeFullName)
        {
            AppDomainSetup adSetup = new AppDomainSetup();

            adSetup.ApplicationBase = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, relativeAppBase);
            adSetup.ConfigurationFile = Path.Combine(adSetup.ApplicationBase, string.Format("{0}.dll.config", assemblyName));

            AppDomain crmAppDomain = AppDomain.CreateDomain(string.Format("{0} AppDomain", assemblyName), null, adSetup);

            IService crmService = (IService)crmAppDomain.CreateInstanceAndUnwrap(assemblyName, serviceTypeFullName);
            crmService.Setup();

            return crmAppDomain;
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
