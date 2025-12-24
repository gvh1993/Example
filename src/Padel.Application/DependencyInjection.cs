using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Padel.Application.Shared.Behaviours;
using Padel.Application.Shared.Messaging;

namespace Padel.Application;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplication()
        {
            services.Scan(x => x.FromAssembliesOf(typeof(DependencyInjection))
                .AddClasses(y => y.AssignableTo(typeof(IQueryHandler<,>)), false)
                .AsImplementedInterfaces()
                .AddClasses(y => y.AssignableTo(typeof(ICommandHandler<>)), false)
                .AsImplementedInterfaces()
                .AddClasses(y => y.AssignableTo(typeof(ICommandHandler<,>)), false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.TryDecorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(ValidationDecorator.CommandBaseHandler<>));

            services.TryDecorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
            services.TryDecorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));
            services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingDecorator.CommandBaseHandler<>));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
