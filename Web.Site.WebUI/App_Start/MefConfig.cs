using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Mvc;
using IDependencyResolver = System.Web.Http.Dependencies.IDependencyResolver;
using System.Web;
using System.Net;

namespace Web.Site.WebUI
{
    public static class MefConfig
    {
        public static void RegisterMef()
        {
            var container = ConfigureContainer();

            ControllerBuilder.Current.SetControllerFactory(new MefControllerFactory(container));
            
            // var dependencyResolver = System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver;
            System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new MefDependencyResolver(container);
        }

        public static CompositionContainer ConfigureContainer()
        {
            var catalog = new AggregateCatalog(new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath));
            var container = new CompositionContainer(catalog, CompositionOptions.DisableSilentRejection);
            HttpContext.Current.Application["Container"] = container;
            HttpContext.Current.DisposeOnPipelineCompleted(container);
            return container;
        }
    }

    public class MefDependencyResolver : IDependencyResolver
    {
        private readonly CompositionContainer _container;
        
        public MefDependencyResolver(CompositionContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            var export = _container.GetExports(serviceType, null, null).SingleOrDefault();

            return null != export ? export.Value : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var exports =_container.GetExports(serviceType, null, null);
            var createdObjects = new List<object>();
            if (exports.Any())
            {
                foreach (var export in exports)
                {
                    createdObjects.Add(export.Value);
                }
            }

            return createdObjects;
        }

        public void Dispose()
        {

        }
    }

    public class MefControllerFactory : DefaultControllerFactory
    {
        private readonly CompositionContainer _compositionContainer;

        public MefControllerFactory(CompositionContainer compositionContainer)
        {
            if ((compositionContainer == null) || compositionContainer.Providers.Count == 0)
            {
                _compositionContainer = MefConfig.ConfigureContainer();
            }
            else
            {
                _compositionContainer = compositionContainer;
            }
        }
         
       
        /// <summary>
        /// 创建控制器对象
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                var export = _compositionContainer.GetExports(controllerType, null, null).SingleOrDefault();
                
                IController result;

                if (null != export)
                {
                    result = export.Value as IController;
                }
                else
                {
                    result = base.GetControllerInstance(requestContext, controllerType);
                    _compositionContainer.ComposeParts(result);
                }

                return result;
            }
            return null;
        }
    }
}