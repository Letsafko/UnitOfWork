using Application.CreateNewOrder;
using Autofac;
using Presentation.Controllers.CreateNewOrder;

namespace Presentation.Modules.Autofac
{
    internal class WebApiModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateNewOrderPresenter>().InstancePerLifetimeScope();
            builder.Register<ICreateNewOrderOutput>(x => x.Resolve<CreateNewOrderPresenter>()).InstancePerLifetimeScope();
        }
        
    }
}