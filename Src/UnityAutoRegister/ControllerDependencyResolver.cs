using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using System.Web;

namespace UnityAutoRegister
{
    public class ControllerDependencyResolver : IDependencyResolver, IDisposable
    {
         

        private readonly IUnityContainer container;

        public ControllerDependencyResolver(IUnityContainer container)
        { 
            this.container = container; 
        }


        public object GetService(Type serviceType)
        {
            try
            {
                if (typeof(IController).IsAssignableFrom(serviceType))
                {
                    return container.Resolve(serviceType);
                }
            }
            catch
            {
                // ignored
            }
            
            return container.IsRegistered(serviceType) ? container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (container.IsRegistered(serviceType))
            { 
                yield return container.Resolve(serviceType);
            }
             
            foreach (object service in container.ResolveAll(serviceType))
            {
                yield return service;
            }

        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

  
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                container?.Dispose();
            }

            disposed = true;
        }

    }
}
