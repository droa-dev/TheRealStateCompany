using NUnit.Framework;
using Properties.Application.BussinesCases.CreateOwner;
using System;
using System.Threading.Tasks;

namespace UnitTest.UseCases.V1.CreateOwner
{
    [TestFixture]
    public sealed class CreateOwnerTest
    {
        [Test, TestCaseSource(typeof(DataSetup), "CreatedOwnerOk")]
        public async Task CreateOwnerAsync_Returns_Created(
            decimal identificationNumber, string name, string address, byte[]? photo, DateTime? birthday)
        {
            CreateOwnerPresenter presenter = new();

            var _fixture = new StandardFixture();

            CreateOwnerUseCase useCase = new(
                _fixture.OwnerRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(identificationNumber, name, address, photo, birthday)
                .ConfigureAwait(false);

            Assert.NotNull(presenter.Owner);
        }

        [Test, TestCaseSource(typeof(DataSetup), "CreatedOwnerBadRequest")]
        public async Task CreateOwnerAsync_Returns_BadRequest(
            decimal identificationNumber, string name, string address, byte[]? photo, DateTime? birthday)
        {
            CreateOwnerPresenter presenter = new();

            var _fixture = new StandardFixture();

            CreateOwnerUseCase useCase = new(
                _fixture.OwnerRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            CreateOwnerValidationUseCase useCaseValidation = new(
                useCase,
                _fixture.Notification
                );

            await useCaseValidation
                .Execute(identificationNumber, name, address, photo, birthday)
                .ConfigureAwait(false);

            useCaseValidation.SetOutputPort(presenter);

            Assert.IsTrue(_fixture.Notification.IsInvalid);
        }
    }
}
