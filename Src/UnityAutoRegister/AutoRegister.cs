using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace UnityAutoRegister
{
    public class AutoRegister
    {
        public static void AllInMvc(IUnityContainer container)
        {
            if(container == null) return;
            
            All(container);
            DependencyResolver.SetResolver(new ControllerDependencyResolver(container));
        }

       
        private static IUnityContainer Container { set; get; }

        public static void All(IUnityContainer c)
        {
            if (c == null) return;
            if (Container == null)
            {
                Container = c;
            }

            List<Assembly> list = AppDomain.CurrentDomain.GetAssemblies().ToList();

            foreach (Assembly assembly in list)
            {
                IEnumerable<Type> types = from type in GetLoadableTypes(assembly)
                                          where Attribute.IsDefined(type, typeof(ImplementAttribute))
                                          select type;

                foreach (Type to in types)
                {
                    ImplementAttribute custom = to.GetCustomAttributes<ImplementAttribute>().SingleOrDefault();
                    if (custom != null)
                    {
                        Container.RegisterType(custom.FromType, to,WithName.Default(to), WithLifetime.ContainerControlled(to));
                       
                    }

                }

            }
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }
    }
}
