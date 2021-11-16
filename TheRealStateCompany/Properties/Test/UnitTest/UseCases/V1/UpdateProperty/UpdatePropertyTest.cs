using NUnit.Framework;
using Properties.Application.BussinesCases.UpdateProperty;
using System;
using System.Threading.Tasks;

namespace UnitTest.UseCases.V1.UpdateProperty
{

    [TestFixture]
    public sealed class UpdatePropertyTest
    {
        [Test, TestCaseSource(typeof(DataSetup), "UpdatedPropertyOk")]
        public async Task UpdatePropertyAsync_Returns_Ok(
            Guid propertyGuid, string? name, string? address, decimal? price,
            decimal? tax, string? codeInternal, string? year, decimal? ownerIdentification, string? countryStateAbb)
        {
            UpdatePropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            UpdatePropertyUseCase useCase = new(
                _fixture.PropertyTraceRepositoryFake,
                _fixture.EntityFactory,
                _fixture.PropertyRepositoryFake,
                _fixture.CountryStatesRepositoryFake,
                _fixture.OwnerRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(propertyGuid: propertyGuid, name: name, address: address, price: price, tax: tax,
                codeInternal: codeInternal, year: year, ownerIdentification: ownerIdentification, countryStateAbb: countryStateAbb)
                .ConfigureAwait(false);

            Assert.NotNull(presenter.Property);
        }

        [Test, TestCaseSource(typeof(DataSetup), "UpdatedPropertyBadRequest")]
        public async Task UpdatePropertyAsync_Returns_BadRequest(
            Guid propertyGuid, string? name, string? address, decimal? price,
            decimal? tax, string? codeInternal, string? year, decimal? ownerIdentification, string? countryStateAbb)
        {
            UpdatePropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            UpdatePropertyUseCase useCase = new(
                _fixture.PropertyTraceRepositoryFake,
                _fixture.EntityFactory,
                _fixture.PropertyRepositoryFake,
                _fixture.CountryStatesRepositoryFake,
                _fixture.OwnerRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            UpdatePropertyValidationUseCase useCaseValidation = new(
                useCase,
                _fixture.Notification
                );

            await useCaseValidation
                .Execute(
                propertyGuid: propertyGuid, name: name, address: address, price: price, tax: tax,
                codeInternal: codeInternal, year: year, ownerIdentification: ownerIdentification, countryStateAbb: countryStateAbb)
                .ConfigureAwait(false);

            useCaseValidation.SetOutputPort(presenter);

            Assert.IsTrue(_fixture.Notification.IsInvalid);
        }

        [Test, TestCaseSource(typeof(DataSetup), "UpdatedPropertyNotFound")]
        public async Task UpdatePropertyAsync_Returns_NotFound(
            Guid propertyGuid, string? name, string? address, decimal? price,
            decimal? tax, string? codeInternal, string? year, decimal? ownerIdentification, string? countryStateAbb)
        {
            UpdatePropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            UpdatePropertyUseCase useCase = new(
                _fixture.PropertyTraceRepositoryFake,
                _fixture.EntityFactory,
                _fixture.PropertyRepositoryFake,
                _fixture.CountryStatesRepositoryFake,
                _fixture.OwnerRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(propertyGuid: propertyGuid, name: name, address: address, price: price, tax: tax,
                codeInternal: codeInternal, year: year, ownerIdentification: ownerIdentification, countryStateAbb: countryStateAbb)
                .ConfigureAwait(false);

            Assert.IsTrue(presenter.IsNotFound);
        }
    }
}
