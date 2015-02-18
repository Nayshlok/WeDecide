using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Ninject;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;

namespace WeDecide.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            ApplyBindings();
        }

        #region IDependencyResolver Members

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        #endregion

        private void ApplyBindings()
        {
            // Do this waaaayyyy better
            kernel.Bind<IMembershipDAL>().To<CustomMembershipDAL>();
            //kernel.Bind<IQuestionDAL>().To<Questio
        }

    }
}
