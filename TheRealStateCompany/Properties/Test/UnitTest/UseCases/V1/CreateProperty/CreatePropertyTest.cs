using NUnit.Framework;
using Properties.Application.BussinesCases.CreateProperty;
using System.Threading.Tasks;

namespace UnitTest.UseCases.V1.CreateProperty
{
    [TestFixture]
    public sealed class CreatePropertyTest
    {
        [Test, TestCaseSource(typeof(DataSetup), "CreatedPropertyOk")]
        public async Task CreatePropertyAsync_Returns_Created(
            string name, string address, decimal price, decimal tax,
            string codeInternal, string year, decimal ownerIdentification, string countryStateAbb)
        {
            CreatePropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            CreatePropertyUseCase useCase = new(
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
                .Execute(name, address, price, tax, codeInternal, year, ownerIdentification, countryStateAbb)
                .ConfigureAwait(false);

            Assert.NotNull(presenter.Property);
        }

        [Test, TestCaseSource(typeof(DataSetup), "CreatedPropertyBadRequest")]
        public async Task CreatePropertyAsync_Returns_BadRequest(
            string name, string address, decimal price, decimal tax,
            string codeInternal, string year, decimal ownerIdentification, string countryStateAbb)
        {
            CreatePropertyPresenter presenter = new();

            var _fixture = new StandardFixture();

            CreatePropertyUseCase useCase = new(
                _fixture.PropertyTraceRepositoryFake,
                _fixture.EntityFactory,
                _fixture.PropertyRepositoryFake,
                _fixture.CountryStatesRepositoryFake,
                _fixture.OwnerRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            CreatePropertyValidationUseCase useCaseValidation = new(
                useCase,
                _fixture.Notification
                );

            await useCaseValidation
                .Execute(name, address, price, tax, codeInternal, year, ownerIdentification, countryStateAbb)
                .ConfigureAwait(false);

            useCaseValidation.SetOutputPort(presenter);

            Assert.IsTrue(_fixture.Notification.IsInvalid);
        }
    }
}
