﻿using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Sdiaz.SimpleChat.Core.Repository_Interfaces;
using Sdiaz.SimpleChat.DataRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.DependencyResolver
{
    public static class RepositoryAutofacModule
    {
        public static ContainerBuilder CreateAutofacRepositoryContainer(this IServiceCollection services, ContainerBuilder builder)
        {
            //var databaseInitializer = new MigrateToLatestVersion(new SampleDataSeeder());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            return builder;
        }
    }

    public class RepositoryAutofacModule1 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //var databaseInitializer = new MigrateToLatestVersion(new SampleDataSeeder());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
