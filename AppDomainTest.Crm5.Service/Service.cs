using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppDomainTest.Common.Interfaces;

namespace AppDomainTest.Crm5.Service
{
    public class Service : MarshalByRefObject, IService
    {
        #region IService Members

        public void Setup()
        {
            Console.WriteLine("Crm5.Service AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
        }

        #endregion
    }
}
