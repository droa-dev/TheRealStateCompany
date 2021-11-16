using NUnit.Framework;
using Properties.Application.BussinesCases.CreateOwner;
using System;
using System.Threading.Tasks;

namespace UnitTest.UseCases.V1.CreateOwner
{
    [TestFixture(typeof(StandardFixture))]
    public sealed class CreateOwnerTest
    {
        private readonly StandardFixture _fixture;
        public CreateOwnerTest(StandardFixture fixture) => this._fixture = fixture;

        [Test]
        public async Task CreateOwnerAsync_Returns_Created(
            decimal identificationNumber, string name, string address, byte[]? photo, DateTime? birthday)
        {
            CreateOwnerPresenter presenter = new();

            CreateOwnerUseCase useCase = new(
                this._fixture.OwnerRepositoryFake,
                this._fixture.EntityFactory,
                this._fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase.Execute(identificationNumber, name, address, photo, birthday);

            Assert.NotNull(presenter.Owner);
        }
    }
}
