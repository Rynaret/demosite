using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using Autofac;
using Common.Conventions;
using Common.Implementations;
using AutoMapper;
using Autofac.Util;
using Common.Implementations.Queries;
using Common.Conventions.Queries;
using Common.Autofac.Resolver;
using Common.Conventions.Commands;
using Common.Implementations.Commands;
using Common.Extensions;
using Common.Queries;
using Microsoft.AspNetCore.SignalR;

namespace Common.Autofac
{
    public static class AutofacConfig
    {
        public static void RegisterCQDependencies(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            var list = assemblies.ToList();
            list.Add(typeof(GetQuery<,>).Assembly);
            assemblies = list.ToArray();

            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IQuery<,>));
            builder.RegisterPartialGenerics(assemblies, typeof(IQuery<,>));
            builder.RegisterPartialGenerics(assemblies, typeof(IQuery<,,>));
            builder.RegisterGeneric(typeof(QueryFor<,>)).As(typeof(IQueryFor<,>));
            builder.RegisterGeneric(typeof(QueryFor<>)).As(typeof(IQueryFor<>));
            builder.RegisterType<QueryForResolver>().As<IQueryForResolver>();
            builder.RegisterType<QueryResolver>().As<IQueryResolver>();
            builder.RegisterType<QueryFactory>().As<IQueryFactory>();
            builder.RegisterType<QueryBuilder>().As<IQueryBuilder>();

            builder.RegisterPartialGenerics(assemblies, typeof(ICommand<,,>));
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(ICommand<>));
            builder.RegisterType<CommandResolver>().As<ICommandResolver>();
            builder.RegisterType<CommandFactory>().As<ICommandFactory>();
            builder.RegisterType<CommandBuilder>().As<ICommandBuilder>();

            builder.RegisterType<EfRepository>().As<IRepository>();
            builder.RegisterType<LinqProvider>().As<ILinqProvider>();
        }

        public static void RegisterSignalRHubs(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.IsSubclassOf(typeof(Hub)))
                .AsSelf()
                .SingleInstance();
        }

        public static void RegisterEventHandlers(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }

        public static void RegisterMapper(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            builder.RegisterAssemblyTypes(assemblies).AssignableTo<Profile>().As<Profile>();

            builder.Register(context => new MapperConfiguration(cfg => context.Resolve<IEnumerable<Profile>>().ForEach(x => cfg.AddProfile(x))))
                .AsSelf()
                .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .SingleInstance();
        }

        private static void RegisterPartialGenerics(this ContainerBuilder builder, Assembly[] assemblies, Type interfaceType)
        {
            var generics = assemblies
                .SelectMany(x => x.GetLoadableTypes()
                    .Where(y => y.GetInterfaces()
                        .Any(z => z.IsGenericType(interfaceType))
                    )
                )
                .Where(x => x.IsGenericType)
                .ToList();
            foreach (var item in generics)
            {
                builder.RegisterGeneric(item).As(interfaceType);
            }
        }

        private static bool IsGenericType(this Type type, Type genericType)
            => type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}