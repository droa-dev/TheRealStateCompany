using NUnit.Framework;
using Properties.Application.BussinesCases.AddPropertyImage;
using System;
using System.Threading.Tasks;

namespace UnitTest.UseCases.V1.AddPropertyImage
{
    [TestFixture]
    public sealed class AddPropertyImageTest
    {
        [Test, TestCaseSource(typeof(DataSetup), "AddPropertyImageOk")]
        public async Task AddPropertyImageAsync_Returns_Ok(
            string fileName, byte[] file, Guid propertyGuid)
        {
            AddPropertyImagePresenter presenter = new();

            var _fixture = new StandardFixture();

            AddPropertyImageUseCase useCase = new(
                _fixture.PropertyImageRepositoryFake,
                _fixture.PropertyRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(fileName: fileName, file: file, propertyGuid: propertyGuid)
                .ConfigureAwait(false);

            Assert.NotNull(presenter.PropertyImage);
        }

        [Test, TestCaseSource(typeof(DataSetup), "AddPropertyImageBadRequest")]
        public async Task AddPropertyImageAsync_Returns_BadRequest(
            string fileName, byte[] file, Guid propertyGuid)
        {
            AddPropertyImagePresenter presenter = new();

            var _fixture = new StandardFixture();

            AddPropertyImageUseCase useCase = new(
                _fixture.PropertyImageRepositoryFake,
                _fixture.PropertyRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            AddPropertyImageValidationUseCase useCaseValidation = new(
                useCase,
                _fixture.Notification
                );

            await useCaseValidation
                .Execute(fileName: fileName, file: file, propertyGuid: propertyGuid)
                .ConfigureAwait(false);

            useCaseValidation.SetOutputPort(presenter);

            Assert.IsTrue(_fixture.Notification.IsInvalid);
        }

        [Test, TestCaseSource(typeof(DataSetup), "AddPropertyImageNotFound")]
        public async Task AddPropertyImageAsync_Returns_NotFound(
            string fileName, byte[] file, Guid propertyGuid)
        {
            AddPropertyImagePresenter presenter = new();

            var _fixture = new StandardFixture();

            AddPropertyImageUseCase useCase = new(
                _fixture.PropertyImageRepositoryFake,
                _fixture.PropertyRepositoryFake,
                _fixture.EntityFactory,
                _fixture.UnitOfWork
                );

            useCase.SetOutputPort(presenter);

            await useCase
                .Execute(fileName: fileName, file: file, propertyGuid: propertyGuid)
                .ConfigureAwait(false);

            Assert.IsTrue(presenter.IsNotFound);
        }
    }
}
