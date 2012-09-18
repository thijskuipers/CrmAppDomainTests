using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace AppDomainTest.Crm4.Interfaces
{
    public class DomainProxy : MarshalByRefObject
    {
        public Assembly GetAssembly(string assemblyPath)
        {
            Console.WriteLine("Resolving assembly: {0}", assemblyPath);
            Console.WriteLine("AppDomain.Name: {0}", AppDomain.CurrentDomain.FriendlyName);

            try
            {
                return Assembly.LoadFrom(assemblyPath);
            }
            catch (Exception ex)
            {
                throw ex;
                //throw new RemotingException(string.Format("Exception occurred while trying to load assembly from path {0}", assemblyPath), ex);
            }
        }

        public static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Console.WriteLine("Hello from DomainProxy.ResolveAssembly");
            Console.WriteLine("  Resolving assembly: {0}", args.Name);
            Console.WriteLine("  AppDomain.Name: {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("  AppDomain.BaseDirectory: {0}", AppDomain.CurrentDomain.BaseDirectory);

            Assembly returnAssembly = null;

            string assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Crm4", args.Name + ".dll");
            if (File.Exists(assemblyPath))
            {
                Console.WriteLine("Assembly exists at {0}", assemblyPath);

                try
                {
                    returnAssembly = Assembly.LoadFrom(assemblyPath);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error occurred while loading assembly");
                }
            }
            else
            {
                Console.WriteLine("Assembly does not exists at {0}", assemblyPath);
            }

            return returnAssembly;
        }

        public static void AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Hello from DomainProxy.AssemblyLoad");
            Console.WriteLine("AppDomain.Name: {0}", AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine(args.LoadedAssembly.FullName);
        }
    }
}
