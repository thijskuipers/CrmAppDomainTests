﻿using System;
using AppDomainTest.Crm4.Interfaces;

namespace AppDomainTest.Crm4.Service
{
    public class Service : MarshalByRefObject, IService
    {
        private static void PrintAssemblies(AppDomain appDomain)
        {
            Console.WriteLine();
            Console.WriteLine("{0} assemblies:", appDomain.FriendlyName);
            foreach (var assembly in appDomain.GetAssemblies())
            {
                Console.WriteLine("  {0}", assembly.FullName);
            }
        }

        #region IService Members

        public void Setup()
        {
            Console.WriteLine("Crm4.Service AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
            PrintAssemblies(AppDomain.CurrentDomain);
        }

        #endregion
    }
}
