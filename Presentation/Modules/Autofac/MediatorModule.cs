using Application;
using Autofac;
using Domain.Abstracts.Mediator;
using MediatR;

namespace Presentation.Modules.Autofac
{
    /// <summary>
    ///     Mediator modules.
    /// </summary>
    public sealed class MediatorModule : Module
    {
        /// <summary>
        ///     Load services.
        /// </summary>
        /// <param name="builder">container builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Processor>().As<IProcessor>();
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly).AsImplementedInterfaces();
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.TryResolve(t, out var o) ? o : default;
            });
        }
    }
}