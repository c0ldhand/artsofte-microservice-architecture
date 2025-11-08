using Application.Sagas;
using Domain.Entities.Enum;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.EF
{
    public class SagaDbContext : MassTransit.EntityFrameworkCoreIntegration.SagaDbContext
    {
        public SagaDbContext(DbContextOptions<SagaDbContext> options, IEnumerable<ISagaClassMap> configurations) : base(options)
        {
            Configurations = configurations;
        }
        public DbSet<OrderState> OrderStates { get; set; } = null!;
        protected override IEnumerable<ISagaClassMap> Configurations { get; }
    }
}
