using System;
using AppDomainTest.Crm4.Interfaces;

namespace AppDomainTest.Crm4.Service
{
    public class Service : MarshalByRefObject, IService
    {
        #region IService Members

        public string Setup()
        {
            return string.Format("Crm4.Service AppDomain: {0}", AppDomain.CurrentDomain.FriendlyName);
        }

        #endregion
    }
}
