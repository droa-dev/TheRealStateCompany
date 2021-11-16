using NUnit.Framework;
using Properties.Application.BussinesCases.ListProperty;
using System.Threading.Tasks;

namespace UnitTest.UseCases.V1.ListProperty
{
    [TestFixture]
    public sealed class ListPropertyTest
    {
        [Test, TestCaseSource(typeof(DataSetup), "ListPropertyOk")]
        public async Task ListPropertyAsync_Returns_Ok(
            decimal? ownerIdentification, string? countryStateAbb, decimal? initialPrice,
            decimal? maxPrice, string? year, string? codeInternal)
        {
            ListPropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            ListPropertyUseCase useCase = new(
                _fixture.OwnerRepositoryFake,
                _fixture.PropertyRepositoryFake,
                _fixture.CountryStatesRepositoryFake,
                _fixture.EntityFactory
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(ownerIdentification, countryStateAbb, initialPrice, maxPrice, year, codeInternal)
                .ConfigureAwait(false);

            Assert.NotNull(presenter.Properties);
        }

        [Test, TestCaseSource(typeof(DataSetup), "ListPropertyBadRequest")]
        public async Task ListPropertyAsync_Returns_BadRequest(
            decimal? ownerIdentification, string? countryStateAbb, decimal? initialPrice,
            decimal? maxPrice, string? year, string? codeInternal)
        {
            ListPropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            ListPropertyUseCase useCase = new(
                _fixture.OwnerRepositoryFake,
                _fixture.PropertyRepositoryFake,
                _fixture.CountryStatesRepositoryFake,
                _fixture.EntityFactory
                );

            ListPropertyValidationUseCase useCaseValidation = new(
                useCase,
                _fixture.Notification
                );

            await useCaseValidation
                .Execute(
                ownerIdentification, countryStateAbb, initialPrice, maxPrice, year, codeInternal)
                .ConfigureAwait(false);

            useCaseValidation.SetOutputPort(presenter);

            Assert.IsTrue(_fixture.Notification.IsInvalid);
        }

        [Test, TestCaseSource(typeof(DataSetup), "ListPropertyNotFound")]
        public async Task ListPropertyAsync_Returns_NotFound(
            decimal? ownerIdentification, string? countryStateAbb, decimal? initialPrice,
            decimal? maxPrice, string? year, string? codeInternal)
        {
            ListPropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            ListPropertyUseCase useCase = new(
                _fixture.OwnerRepositoryFake,
                _fixture.PropertyRepositoryFake,
                _fixture.CountryStatesRepositoryFake,
                _fixture.EntityFactory
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(ownerIdentification, countryStateAbb, initialPrice, maxPrice, year, codeInternal)
                .ConfigureAwait(false);

            Assert.IsTrue(presenter.IsNotFound);
        }
    }
}
