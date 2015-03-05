using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Http;

using Ninject;
using WeDecide.DAL.Abstract;
using WeDecide.DAL.Concrete;
using WeDecide.Models.Concrete;
using WeDecide.Controllers;

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
            // Just dependency inject the things you need.
            // No longer do we have to waste resources on "newing up" the context object.
            kernel.Bind<QuestionDbContext>().ToMethod<QuestionDbContext>(
                (context) => { return QuestionDbContext.Create(); }
            );
            kernel.Bind<ApplicationDbContext>().ToMethod<ApplicationDbContext>(
                context => { return ApplicationDbContext.Create(); }
            );

            kernel.Bind<IQuestionDAL>().To<TestDal>();
            //kernel.Bind<IMembershipDAL>().To<CustomMembershipDAL>();
        }

    }
}
