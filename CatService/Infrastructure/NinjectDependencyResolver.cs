using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace CatService.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            throw new NotImplementedException();
        }
        private void AddBindings()
        {
           // kernel.Bind<IValueCalculator>().To<LinqValueCalculator>();
        }
    }
}