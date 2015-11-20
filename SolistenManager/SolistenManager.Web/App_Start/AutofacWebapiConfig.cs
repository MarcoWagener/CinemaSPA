﻿using Autofac;
using Autofac.Integration.WebApi;
using SolistenManager.Data;
using SolistenManager.Data.Infrastructure;
using SolistenManager.Services;
using SolistenManager.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace SolistenManager.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<SolistenManagerContext>()
                    .As<DbContext>()
                    .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                    .As<IDbFactory>()
                    .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                    .As<IUnitOfWork>()
                    .InstancePerRequest();

            builder.RegisterType<EncryptionService>()
                    .As<IEncryptionService>()
                    .InstancePerRequest();

            builder.RegisterType<MembershipService>()
                    .As<IMembershipService>()
                    .InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}