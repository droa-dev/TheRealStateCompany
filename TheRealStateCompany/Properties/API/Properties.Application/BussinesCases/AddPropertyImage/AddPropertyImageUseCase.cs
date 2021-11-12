using Properties.Application.Services;
using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Properties.Application.BussinesCases.AddPropertyImage
{
    /// <inheritdoc />
    public class AddPropertyImageUseCase : IAddPropertyImageUseCase
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IPropertyRepository _propertyRepository;       
        private readonly IPropertyFactory _propertyFactory;
        private readonly IUnitOfWork _unitOfWork;
        private IOutputPort _outputPort;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddPropertyImageUseCase" /> class.
        /// </summary>
        /// <param name="propertyImageRepository">Property Image Repository</param>
        /// <param name="propertyRepository">Property Repository</param>
        /// <param name="propertyFactory">Property Factory</param>
        /// <param name="unitOfWork"></param>
        public AddPropertyImageUseCase(
            IPropertyImageRepository propertyImageRepository,
            IPropertyRepository propertyRepository,            
            IPropertyFactory propertyFactory,
            IUnitOfWork unitOfWork)
        {
            _propertyImageRepository = propertyImageRepository;
            _propertyRepository = propertyRepository;            
            _propertyFactory = propertyFactory;
            _unitOfWork = unitOfWork;
            _outputPort = new AddPropertyImagePresenter();
        }

        /// <inheritdoc />
        public void SetOutputPort(IOutputPort outputPort) => this._outputPort = outputPort;

        /// <inheritdoc />
        public Task Execute(string fileName, byte[] file, Guid propertyGuid) =>
            this.AddPropertyImage(
                new Name(fileName), new File(file), new PropertyGuid(propertyGuid));

        private async Task AddPropertyImage(
            Name fileName, File file, PropertyGuid propertyGuid)
        {
            IProperty property = await _propertyRepository
                .GetProperty(propertyGuid)
                .ConfigureAwait(false);

            if (property is Property registeredProperty)
            {
                PropertyImage propertyImage = _propertyFactory
                    .NewPropertyImage(fileName, file, registeredProperty.PropertyGuid);

                await this.Create(propertyImage)
                    .ConfigureAwait(false);

                this._outputPort?.Ok(propertyImage);
            }

            this._outputPort?.NotFound();
        }

        private async Task Create(PropertyImage propertyImage)
        {
            await this._propertyImageRepository
                .Add(propertyImage)
                .ConfigureAwait(false);

            await this._unitOfWork
                .Save()
                .ConfigureAwait(false);
        }
    }
}
