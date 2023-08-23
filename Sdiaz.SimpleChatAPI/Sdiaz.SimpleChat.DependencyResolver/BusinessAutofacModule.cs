using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Sdiaz.SimpleChat.Business.ServiceQuery;
using Sdiaz.SimpleChat.Business;
using Sdiaz.SimpleChat.Core.Business_Interface.ServiceQuery;
using Sdiaz.SimpleChat.Core.Business_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sdiaz.SimpleChat.DependencyResolver
{
    public static class BusinessAutofacModule
    {
        public static ContainerBuilder CreateAutofacBusinessContainer(this IServiceCollection services, ContainerBuilder builder)
        {
            builder.RegisterType<IMessageService>().As<MessageService>();
            builder.RegisterType<IMessageServiceQuery>().As<MessageServiceQuery>();
            return builder;
        }
    }

    public class BusinessAutofacModule1 : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<MessageServiceQuery>().As<IMessageServiceQuery>();
        }
    }
}
