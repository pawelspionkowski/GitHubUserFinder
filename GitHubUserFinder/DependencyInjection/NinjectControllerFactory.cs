using GitHubUserFinder.Service.GitHub.AbstractWebServiceRepository;
using GitHubUserFinder.Service.GitHub.WebServiceRepository;
using Ninject;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace GitHubUserFinder.DependencyInjection
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IRepository>().To<GitHub>().InSingletonScope()
            .WithConstructorArgument("_gitHubLoggin", ConfigurationManager.AppSettings["GitHubLoggin"])
            .WithConstructorArgument("_gitHubPassword", ConfigurationManager.AppSettings["GitHubPassword"]);
        }
    }
}