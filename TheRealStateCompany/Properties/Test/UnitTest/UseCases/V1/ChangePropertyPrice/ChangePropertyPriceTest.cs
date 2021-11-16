using NUnit.Framework;
using Properties.Application.BussinesCases.ChangePropertyPrice;
using System;
using System.Threading.Tasks;

namespace UnitTest.UseCases.V1.ChangePropertyPrice
{
    [TestFixture]
    public sealed class ChangePropertyPriceTest
    {
        [Test, TestCaseSource(typeof(DataSetup), "ChangePropertyPriceOk")]
        public async Task ChangePropertyPriceAsync_Returns_Ok(
            Guid propertyGuid, decimal price, decimal tax)
        {
            ChangePropertyPricePresenter presenter = new();

            var _fixture = new StandardFixture();

            ChangePropertyPriceUseCase useCase = new(
                _fixture.PropertyTraceRepositoryFake,
                _fixture.EntityFactory,
                _fixture.PropertyRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(propertyGuid, price, tax)
                .ConfigureAwait(false);

            Assert.NotNull(presenter.Property);
        }

        [Test, TestCaseSource(typeof(DataSetup), "ChangePropertyPriceBadRequest")]
        public async Task ChangePropertyPriceAsync_Returns_BadRequest(
            Guid propertyGuid, decimal price, decimal tax)
        {
            ChangePropertyPricePresenter presenter = new();

            var _fixture = new StandardFixture();

            ChangePropertyPriceUseCase useCase = new(
                _fixture.PropertyTraceRepositoryFake,
                _fixture.EntityFactory,
                _fixture.PropertyRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            ChangePropertyPriceValidationUseCase useCaseValidation = new(
                useCase,
                _fixture.Notification
                );

            await useCaseValidation
                .Execute(propertyGuid, price, tax)
                .ConfigureAwait(false);

            useCaseValidation.SetOutputPort(presenter);

            Assert.IsTrue(_fixture.Notification.IsInvalid);
        }

        [Test, TestCaseSource(typeof(DataSetup), "ChangePropertyPriceNotFound")]
        public async Task ChangePropertyPriceAsync_Returns_NotFound(
            Guid propertyGuid, decimal price, decimal tax)
        {
            ChangePropertyPricePresenter presenter = new();

            var _fixture = new StandardFixture();

            ChangePropertyPriceUseCase useCase = new(
                _fixture.PropertyTraceRepositoryFake,
                _fixture.EntityFactory,
                _fixture.PropertyRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(propertyGuid, price, tax)
                .ConfigureAwait(false);

            Assert.IsTrue(presenter.IsNotFound);
        }
    }
}
