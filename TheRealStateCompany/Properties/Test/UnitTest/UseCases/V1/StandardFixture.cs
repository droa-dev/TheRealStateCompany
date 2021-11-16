using Properties.Application.Services;
using Properties.Infrastructure.DataAccess.DataProviders.SQLServer;
using Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories.Fakes;
using Properties.Infrastructure.DataAccess.Factories;

namespace UnitTest.UseCases.V1
{
    public sealed class StandardFixture
    {
        public StandardFixture()
        {
            this.Context = new RealStateDbContextFake();
            this.PropertyRepositoryFake = new PropertyRepositoryFake(this.Context);
            this.OwnerRepositoryFake = new OwnerRepositoryFake(this.Context);
            this.PropertyTraceRepositoryFake = new PropertyTraceRepositoryFake(this.Context);
            this.PropertyImageRepositoryFake = new PropertyImageRepositoryFake(this.Context);
            this.CountryStatesRepositoryFake = new CountryStatesRepositoryFake(this.Context);
            this.UnitOfWork = new UnitOfWorkFake();
            this.EntityFactory = new EntityFactory();
            this.Notification = new Notification();
        }

        public EntityFactory EntityFactory { get; }
        public RealStateDbContextFake Context { get; }
        public PropertyRepositoryFake PropertyRepositoryFake { get; }
        public OwnerRepositoryFake OwnerRepositoryFake { get; }
        public PropertyImageRepositoryFake PropertyImageRepositoryFake { get; }
        public PropertyTraceRepositoryFake PropertyTraceRepositoryFake { get; }
        public CountryStatesRepositoryFake CountryStatesRepositoryFake { get; }
        public UnitOfWorkFake UnitOfWork { get; }
        public Notification Notification { get; }
    }
}