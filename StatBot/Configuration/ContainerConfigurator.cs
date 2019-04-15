using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using StatBot.Cuckoo;
using StatBot.Helpers;
using StatBot.Service.Core;
using StatBot.Settings;
using StatBot.Tracing;
using StatBot.UserIO;
using StatBot.Vk.Models;

namespace StatBot.Configuration
{
    public static class ContainerConfigurator
    {
        private static readonly Type[] excludeFromAutoRegistration =
        {
            typeof(ApplicationSettings),
            typeof(IService),
        };

        private static readonly Type[] simpleTransientTypes =
        {
            typeof(IMessagePrinter),
            typeof(ITraceIdProvider),
        };

        public static IWindsorContainer ConfigureContainer()
        {
            var assembly = Assembly.GetCallingAssembly();
            var container =
                new WindsorContainer().Install(
                    new AssemblyInstaller(
                        assembly,
                        new InstallerFactory())
                );

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            var classes = Classes.FromAssemblyInThisApplication();

            container.Register(
                classes
                    .Where(
                        t =>
                            !excludeFromAutoRegistration.Intersect(t.WithInterfaces()).Any() &&
                            !simpleTransientTypes.Intersect(t.WithInterfaces()).Any()
                    )
                    .WithServiceAllInterfaces()
            );

            container.Register(
                classes
                    .Where(
                        t =>
                            !excludeFromAutoRegistration.Intersect(t.WithInterfaces()).Any() &&
                            simpleTransientTypes.Intersect(t.WithInterfaces()).Any()
                    )
                    .WithServiceAllInterfaces()
                    .LifestyleTransient()
            );

            var userMessagePrinterImplType = UserMessagePrinterEmitter.EmitImplType();
            container.Register(
                Component.For<Func<string, IUserMessagePrinter>>()
                         .Instance(
                             prefix =>
                             {
                                 var messagePrinter = container.Resolve<IMessagePrinter>(
                                     Arguments.FromNamed(new[] {new KeyValuePair<string, object>("prefix", prefix)})
                                 );

                                 return (IUserMessagePrinter) Activator.CreateInstance(userMessagePrinterImplType,
                                                                                       messagePrinter);
                             }
                         )
            );

            var settings = container.Resolve<IApplicationSettingsLoader>().Load();
            container.Register(Component.For<ApplicationSettings>().Instance(settings));

            container.Register(
                Component.For<IExecutingRequestsCollection[]>()
                         .Instance(
                             new IExecutingRequestsCollection[]
                             {
                                 container.Resolve<IExecutingRequestsCollection<StatRequestParameters>>(),
                                 container.Resolve<IExecutingRequestsCollection<CuckooRequestParameters>>()
                             }
                         )
            );

            return container;
        }
    }
}