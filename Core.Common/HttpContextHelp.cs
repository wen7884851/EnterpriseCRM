//**********************************
//*ClassName:
//*Version:
//*Date:
//*Author:
//*Effect:
//**********************************

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;

namespace Core.Common
{
    public class HttpContextHelp
    {
        public CompositionContainer HttpContextGet(string ContextName)
        {
            CompositionContainer container = new CompositionContainer();
            
            if (ContextName == "")
            {
                return container;
            }
            container = HttpContext.Current.Application[ContextName] as CompositionContainer;
            if (container == null)
            {
                var catalog = new AggregateCatalog(new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath));
                container = new CompositionContainer(catalog);
                HttpContext.Current.Application[ContextName] = container;
            }
            return container;
        }

    }
}